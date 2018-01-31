using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bmbox.DAL.Repos;
using Bmbox.DAL.Entities;
using bmbox_main.Controllers;
using System.Web.Mvc;
using bmbox_main.Models;
using System.Collections.Generic;
using System.Linq;

namespace bmbox_main.Tests
{
    [TestClass]
    public class RepoTest
    {
        private ProductController controller;
        private ViewResult viewResult;

        AbsRepo<Product, int> repo;

        [TestInitialize]
        public void Initialize()
        {
            controller = new ProductController();
            viewResult = controller.Index("name_desc", "P5", "type_desc", 1, "name_desc", "type_desc") as ViewResult;

            repo = new ProductRepo();
        }

        // After product is added without exception, the controller should return to "Index" ctrller
        [TestMethod]
        public void ProductCreate()
        {
            var product = viewResult.ViewData.Model;
            var result = (RedirectToRouteResult) controller.Create(new ProductViewModel
            {
                Name ="Name",
                Brand = "Brand",
                QuantityLeft = 1, 
                Type = "Type",
                Cost = 100
            });

            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        // the same as above but checks create product in DAL for exception
        [TestMethod]
        public void ProductCreateDAL()
        {
            try {
                repo.Create(new Product
                {
                    Name = "Name",
                    Brand = "Brand",
                    QuantityLeft = 1,
                    Type = "Type",
                    Cost = 100,
                });
            }  catch (Exception)
            {
                Assert.Fail();
            }
            Assert.IsTrue(true);
        }


        [TestMethod]
        public void AssertProductUpdate()
        { 
            var product = viewResult.ViewData.Model;
            var result = (RedirectToRouteResult)controller.Create(new ProductViewModel
            {
                Name = "Name Updated",
                Brand = "Brand Updated",
                QuantityLeft = 0,
                Type = "Type Updated",
                Cost = 0
            });
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void AsserProductSorting()
        {
            AbsRepo<Product, int> repository = new ProductRepo();
         
            var product = repository.GetAll();

            var sortedProductDesc = controller.Sort("type_desc", product).ToList();
            var sortedProductAsc = controller.Sort("Type", product).ToList();

            Assert.AreSame(sortedProductAsc.First(), sortedProductDesc.Last());
            Assert.AreSame(sortedProductAsc.Last(), sortedProductDesc.First());

            sortedProductDesc = controller.Sort("price_desc", product).ToList();
            sortedProductAsc = controller.Sort("Price", product).ToList();

            Assert.AreSame(sortedProductAsc.First(), sortedProductDesc.Last());
            Assert.AreSame(sortedProductAsc.Last(), sortedProductDesc.First());

        }

        [TestMethod]
        public void AsserProductSearching()
        {
            var products = repo.GetAll();
            var search = "Galaxy";
            var searchResult = controller.SearchResult(search, null, products).ToList();
            var expectedResult = products.Where(s => s.Name.Contains(search)).ToList();
            CollectionAssert.AreEqual(searchResult, expectedResult);

            search = "Smartphone";
            searchResult = controller.SearchResult(null, search, products).ToList();
            expectedResult = products.Where(s => s.Type.Equals(search)).ToList();
            CollectionAssert.AreEqual(searchResult, expectedResult);
        }

        // APQNWAGYRnjTwZ1C2X2blknqfKbbxSWQkM1JuuCySPVNbS1Cgtgmw641/nIGz1sFFw==
        [TestMethod]
        public void AuthenticateUser()
        {
            SignUpInController c = new SignUpInController();
            var pass = "1234567890";
            var enc = c.EncryptPassword(pass);
            Assert.IsTrue(c.PasswordIsValid(pass, enc));

        }


        [TestMethod]
        public void AssertNewTransaction()
        {
            AbsRepo<Transactions, string> repo = new TransactionRepo();
            long today = DateTime.Today.Ticks;

            try
            {
                repo.Create(new Transactions
                {
                    UserEmail = "gk@mail.ru",
                    Date = today
                });
            }
            catch (Exception)
            {
                Assert.Fail();
            }
            Assert.IsTrue(true);
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

    }
}
