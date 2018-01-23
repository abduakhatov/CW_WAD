using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bmbox.DAL.Repos;
using Bmbox.DAL.Entities;
using bmbox_main.Controllers;

namespace bmbox_main.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            SignUpInController c = new SignUpInController();
            var pass = "hello";
            var enc = c.EncryptPassword(pass);
            Assert.IsTrue(c.UserExists(pass, enc));

        }
    }
}
