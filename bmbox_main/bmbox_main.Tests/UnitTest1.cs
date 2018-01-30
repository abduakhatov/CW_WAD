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
            AbsRepo<User, int> repo = new UserRepo();
            SignUpInController c = new SignUpInController();

            var enc = c.EncryptPassword("1234567890");
            Assert.AreEqual(enc, "1");
        }
        /*
        Test Name:	T
Test FullName:	bmbox_main.Tests.UnitTest1.T
Test Source:	C:\Users\Farruh\Desktop\WAD\solution\CW_WAD\bmbox_main\bmbox_main.Tests\UnitTest1.cs : line 24
Test Outcome:	Failed
Test Duration:	0:00:00.3949374

Result StackTrace:	at bmbox_main.Tests.UnitTest1.T() in C:\Users\Farruh\Desktop\WAD\solution\CW_WAD\bmbox_main\bmbox_main.Tests\UnitTest1.cs:line 29
Result Message:	Assert.AreEqual failed. Expected:<APypPOKDhcm3dYwY/EW9YkIHkraQAAhk/sdmZ/zPbZkghEXKhlNv7BuRwPCKwQpgPQ==>. Actual:<1>.



    */

        [TestMethod]
        public void CreateTans()
        {
            AbsRepo<Transactions, string> repo = new TransactionRepo();
            long today = DateTime.Today.Ticks;

            Transactions t = new Transactions
            {
                UserEmail = "gk@mail.ru",
                Date = today
            };
            repo.Create(t);
        }
    }
}
// APQNWAGYRnjTwZ1C2X2blknqfKbbxSWQkM1JuuCySPVNbS1Cgtgmw641/nIGz1sFFw==
