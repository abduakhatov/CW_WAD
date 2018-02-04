using bmbox_main.Models;
using System.Web.Mvc;
using Bmbox.DAL.Entities;
using Bmbox.DAL.Repos;
using System.Linq;
using bmbox_main.Utils;
using System;
using System.Collections.Generic;
using PagedList;
using System.Xml.Linq;
using System.IO;
using System.Xml.Schema;
using bmbox_main.Helpers;
using bmbox_main.Models.Utils;
using System.Web;
using System.Drawing;

namespace bmbox_main.Controllers
{
    [Authorize]
    public class ProductController : ParentController
    {
        private AbsRepo<Product, int> repo = new ProductRepo();
        private Log log = new Log()
        {
            Controller = "Parent"
        };

        // GET: Product
        public ActionResult Index(string sortOrder, string nameSearch, string typeSearch
            , int? page, string currentNameFilter, string currentTypeFilter)
        {
            var products = repo.GetAll();
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.TypeSortParam = sortOrder == "Type" ? "type_desc" : "Type";
            ViewBag.PriceSortParam = sortOrder == "Price" ? "price_desc" : "Price";

            if (nameSearch != null)
            {
                page = 1;
            }
            else
            {
                nameSearch = currentNameFilter;
            }

            if (typeSearch != null)
            {
                page = 1;
            }
            else
            {
                typeSearch = currentTypeFilter;
            }

            ViewBag.CurrentNameFilter = nameSearch;
            ViewBag.CurrentTypeFilter = typeSearch;



            var category = products.Select(u => u.Type).Distinct().ToList();

            List<string> res = new List<string>();
            res.Add("All");
            res.AddRange(category);

            ViewBag.Categories = res;

            try
            {
                log.Action = "Index";
                log.IPAddress = Request.UserHostAddress.ToString();
                log.Method = Constants.LOG_METHOD_GET;
                log.User = User.Identity.Name;

                products = SearchResult(nameSearch, typeSearch, products);
                products = Sort(sortOrder, products);

                LogHelper.Info(log);
            }
            catch (Exception e)
            {
                log.Action = log.Action + "\n" + e.ToString();
                LogHelper.Error(log);
            }

            int pageSize = 10;
            var pageIndex = (page ?? 1);

            return View(new PagedList<ProductViewModel>(products.Select(MapToModel), pageIndex, pageSize));
        }

        public IQueryable<Product> Sort(string sortOrder, IQueryable<Product> products)
        {
            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(p => p.Name);
                    break;
                case "Type":
                    products = products.OrderBy(p => p.Type);
                    break;
                case "type_desc":
                    products = products.OrderByDescending(p => p.Type);
                    break;
                case "Price":
                    products = products.OrderBy(p => p.Cost);
                    break;
                case "price_desc":
                    products = products.OrderByDescending(p => p.Cost);
                    break;
                default:
                    products = products.OrderBy(p => p.Name);
                    break;
            }
            return products;
        }

        public IQueryable<Product> SearchResult(string nameSearch, string typeSearch, IQueryable<Product> products)
        {
            if (!String.IsNullOrEmpty(nameSearch))
            {
                products = products.Where(s => s.Name.Contains(nameSearch));
            }
            if (!String.IsNullOrEmpty(typeSearch) && !typeSearch.Equals("All"))
            {
                products = products.Where(s => s.Type.Equals(typeSearch));
            }
            return products;
        }

        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                log.Action = "Create";
                log.IPAddress = Request.UserHostAddress.ToString();
                log.Method = Constants.LOG_METHOD_GET;
                log.User = User.Identity.Name;
                LogHelper.Info(log);
            }
            catch (Exception e)
            {
                log.Action = log.Action + "\n" + e.ToString();
                LogHelper.Error(log);
            }
            return View(new ProductViewModel());
        }

        [HttpPost]
        public ActionResult Create(ProductViewModel m)
        {
            log.Action = "Create";
            log.IPAddress = Request.UserHostAddress.ToString();
            log.Method = Constants.LOG_METHOD_POST;
            log.User = User.Identity.Name;

            try
            {
                HttpPostedFileBase file = Request.Files["imageToImport"];
                var binaryImage = ConvertToBytes(file);
                var res = new byte[0];
                if (binaryImage == null || binaryImage == new byte[0])
                {
                    var imgPath = Server.MapPath("~/Images/no_image.png").Replace(@"\\", @"\");
                    Image img = Image.FromFile(imgPath);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        binaryImage = ms.ToArray();
                    };
                }

                m.Image = binaryImage;
                var p = MapFromModel(m);
                repo.Create(p);
                LogHelper.Info(log);
                return RedirectToAction("Index");
            }
            catch (System.Exception e)
            {
                log.Action = log.Action + "\n" + e.ToString();
                LogHelper.Error(log);
                throw;
            }
        }

        [HttpGet]
        public ActionResult CreateFromFile()
        {
            try
            {
                log.Action = "CreateFromFile";
                log.IPAddress = Request.UserHostAddress.ToString();
                log.Method = Constants.LOG_METHOD_GET;
                log.User = User.Identity.Name;
                LogHelper.Info(log);
            }
            catch (Exception e)
            {
                log.Action = log.Action + "\n" + e.ToString();
                LogHelper.Error(log);
            }
            return View();
        }

        [HttpPost]
        public ActionResult CreateFromFile(FormCollection formCollection)
        {
            log.Action = "CreateFromFile";
            log.IPAddress = Request.UserHostAddress.ToString();
            log.Method = Constants.LOG_METHOD_POST;
            log.User = User.Identity.Name;

            var file = Request.Files["fileToImport"];
            if (file == null)
            {
                ViewBag.Result = "File is missing";
                log.Action = log.Action + "\n File is missing";
                LogHelper.Error(log);
            }

            var products = new List<ProductViewModel>();
            using (var reader = new StreamReader(file.InputStream))
            {
                string line;
                try
                {

                    while ((line = reader.ReadLine()) != null){
                        
                        var product = new ProductViewModel();

                        try
                        {
                            var tokens = line.Split('|');
                            product.Name = tokens[0];
                            product.Brand = tokens[1];
                            product.Type = tokens[2];
                            product.Cost = decimal.Parse(tokens[3]);
                            product.QuantityLeft = short.Parse(tokens[4]);

                            products.Add(product);
                            var p = MapFromModel(product);
                            repo.Create(p);
                        }
                        catch (Exception e)
                        {
                            ViewBag.Result = "Incorrect file content format in line:\n" + line;
                            return View();
                        }
                    }
                }
                catch (Exception e)
                {
                    log.Action = log.Action + "\n" + e.ToString();
                    LogHelper.Error(log);
                }
            }

            ViewBag.Products = products;
            LogHelper.Info(log);
            return View();
        }


        [HttpGet]
        public ActionResult Details(int id)
        {
            log.Action = "Details";
            log.IPAddress = Request.UserHostAddress.ToString();
            log.Method = Constants.LOG_METHOD_GET;
            log.User = User.Identity.Name;
            Product res = null;
            try
            {
                LogHelper.Info(log);
                res = repo.GetById(id);
                
            }
            catch (System.Exception e)
            {
                log.Action = log.Action + "\n" + e.ToString();
                LogHelper.Error(log);
                throw;
            }
            return View(MapToModel(res));
        }


        [HttpGet]
        public ActionResult Update(int id)
        {
            log.Action = "Update";
            log.IPAddress = Request.UserHostAddress.ToString();
            log.Method = Constants.LOG_METHOD_GET;
            log.User = User.Identity.Name;
            Product res = null;

            try
            {
                LogHelper.Info(log);
                res = repo.GetById(id);

            }
            catch (System.Exception e)
            {
                log.Action = log.Action + "\n" + e.ToString();
                LogHelper.Error(log);
                throw;
            }
            return View(MapToModel(res));
        }

        [HttpPost]
        public ActionResult Update(ProductViewModel p)
        {
            log.Action = "Update";
            log.IPAddress = Request.UserHostAddress.ToString();
            log.Method = Constants.LOG_METHOD_POST;
            log.User = User.Identity.Name;
            if (ModelState.IsValid)
            {
                try
                {

                    HttpPostedFileBase file = Request.Files["imageToImport"];
                    var binaryImage = ConvertToBytes(file);
                    if (binaryImage == null || binaryImage == new byte[0])
                    {
                        var imgPath = Server.MapPath("~/Images/no_image.png").Replace(@"\\", @"\");
                        Image img = Image.FromFile(imgPath);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            binaryImage = ms.ToArray();
                        };
                    }

                    p.Image = binaryImage;

                    repo.Update(MapFromModel(p));
                    LogHelper.Info(log);
                    return RedirectToAction("Index");
                }
                catch (System.Exception e)
                {
                    log.Action = log.Action + "\n" + e.ToString();
                    LogHelper.Error(log);
                    throw;
                }
            }
            log.Action = log.Action + "\nInvalid Model State";
            LogHelper.Error(log);
            return View();
        }

        public ActionResult Delete(int id)
        {
            log.Action = "Delete";
            log.IPAddress = Request.UserHostAddress.ToString();
            log.Method = Constants.LOG_METHOD_GET;
            log.User = User.Identity.Name;

            try
            {
                repo.Remove(id);
                LogHelper.Info(log);
                return RedirectToAction("Index");
            }
            catch (System.Exception e)
            {
                log.Action = log.Action + "\n" + e.ToString();
                LogHelper.Error(log);
                return View();
            }
        }

        public ActionResult AddToBasket(int pId, string email)
        {
            log.Action = "AddToBasket";
            log.IPAddress = Request.UserHostAddress.ToString();
            log.Method = Constants.LOG_METHOD_GET;
            log.User = User.Identity.Name;
            try
            {
                TransactionController tc = new TransactionController();
                tc.Create(pId, email);
                LogHelper.Info(log);
                return RedirectToAction("Index");
            }
            catch (System.Exception e)
            {
                log.Action = log.Action + "\n" + e.ToString();
                LogHelper.Error(log);
                return View();
            }
        }

        public ActionResult ConvertToXml(string format)
        {
            log.Action = "AddToBasket";
            log.IPAddress = Request.UserHostAddress.ToString();
            log.Method = Constants.LOG_METHOD_GET;
            log.User = User.Identity.Name;

            var products = repo.GetAll().Select(MapToModel);

            var xDoc = new XDocument();
            xDoc.Declaration = new XDeclaration("1.0", "utf-8", "no");

            switch (format)
            {
                case "csv":
                    xDoc.Add(new XProcessingInstruction("xml-stylesheet", "type='text/xsl' href='/xml/ProductToCSV.xslt'"));
                    break;
            }

            xDoc.Add(new XElement("Products",
                                    products.Select(p =>
                                        new XElement("Product", new XAttribute("Id", p.Id),
                                            new XElement("Name", p.Name),
                                            new XElement("Brand", p.Brand),
                                            new XElement("Type", p.Type),
                                            new XElement("Image", p.Image),
                                            new XElement("Cost", p.Cost),
                                            new XElement("QuantityLeft", p.QuantityLeft)))));

            var schemas = new XmlSchemaSet();
            schemas.Add("", "http://" + System.Web.HttpContext.Current.Request.Url.Authority + "/xml/ProductSchema.xsd");

            var isValid = true;
            var errorMessage = "";

            xDoc.Validate(schemas, (o, e) =>
            {
                isValid = false;
                errorMessage = e.Message;
            }, true);

            if (!isValid)
            {
                xDoc = new XDocument();
                xDoc.Declaration = new XDeclaration("1.0", "utf-8", "no");
                xDoc.Add(new XElement("Error", errorMessage));
            }

            var sw = new StringWriter();
            xDoc.Save(sw);

            LogHelper.Info(log);
            return Content(sw.ToString(), "text/xml");
        }


        private Product MapFromModel(ProductViewModel m)
        {
            return new Product
            {
                Id = m.Id,
                Name = m.Name,
                Brand = m.Brand,
                Type = m.Type,
                Image = m.Image,
                Cost = m.Cost,
                QuantityLeft = m.QuantityLeft
            };
        }
        private ProductViewModel MapToModel(Product p)
        {
            return new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Brand = p.Brand,
                Type = p.Type,
                Image = p.Image,
                Cost = p.Cost,
                QuantityLeft = p.QuantityLeft
            };
        }

        private byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;

            if (image.ContentLength == 0 && string.IsNullOrEmpty(image.FileName)) return null;

            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }

        //private byte[] ConvertFromBytes(byte[] bytes)
        //{
        //    HttpPostedFileBase image = null;
        //    BinaryWriter reader = new BinaryWriter(bytes);
        //    image = reader.Write(bytes);
        //    return imageBytes;
        //}
    }
}