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
            var pass = "qweqwe";
            var enc = c.EncryptPassword("a");
            Assert.IsTrue(c.PasswordIsValid(pass, enc));

        }

        [TestMethod]
        public void T()
        {
            SignUpInController c = new SignUpInController();
            var pass = "qweqwe";
            var enc = c.EncryptPassword("1234567890");
            Assert.AreEqual(enc, "asd");
        }

        [TestMethod]
        public void CreateTans()
        {
            AbsRepo<Transaction> repo = new TransactionRepo();
            long today = DateTime.Today.Ticks;

            Transaction t = new Transaction
            {
                User = new User { Email = "gk@mail.ru" },
                Date = today
            };
            repo.Create(t);
        }
    }
}
// APQNWAGYRnjTwZ1C2X2blknqfKbbxSWQkM1JuuCySPVNbS1Cgtgmw641/nIGz1sFFw==
