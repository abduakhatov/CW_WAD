using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bmbox.DAL.Repos;
using Bmbox.DAL.Entities;
using bmbox_main.Controllers;
using bmbox_main.Models;

namespace bmbox_main.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            SignInUpController c = new SignInUpController();
            var obj = new RegistrationViewModel();
            //obj.Name = "Fn", "LN", "EM","Add", "qweqwe", "qweqwe"
            //Assert.AreEqual(true, conotroller.UserExists(new LoginViewModel()));

        }
    }
}
