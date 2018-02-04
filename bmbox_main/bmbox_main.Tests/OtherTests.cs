using bmbox_main.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace bmbox_main.Tests
{
    [TestClass]
    class OtherTests
    {
        [TestMethod]
        public void AssertProductCreate()
        {
            ProductController x = new ProductController();
            x.Create(new Models.ProductViewModel
            {
                Brand = "Brand",
                Name = "Name",
                Cost = 0,
                QuantityLeft = 0,
                Type = "Type",
                Image = null
            });
            Assert.Fail();      

        }
    }
}
