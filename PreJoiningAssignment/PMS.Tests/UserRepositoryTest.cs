using Microsoft.VisualStudio.TestTools.UnitTesting;
using PMS.BAL.Interface;
using PMS.DAL.Repository;
using PMS.Model;
using PMS.WebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using System.Web.Http.Results;

namespace PMS.Tests
{
    [TestClass]
    public class UserRepositoryTest
    {
        [TestMethod]
        public void TestPostAuthenticateUserFound()
        {
            var repo = new UserRepository();
            UserData user = new UserData();
            user.Email = "admin@gmail.com";
            user.Password = "Admin@123";
            var result = repo.AuthenticateUser(user);
            Assert.AreEqual("1", result[1]);
        }
        
        [TestMethod]
        public void TestPostAuthenticateUserNotFound()
        {
            var repo = new UserRepository();
            UserData user = new UserData();
            user.Email = "abc@gmail.com";
            user.Password = "admin@123";
            var result = repo.AuthenticateUser(user);
            Assert.AreEqual("Not Found", result[0]);
        }

        [TestMethod]
        public void TestPostCreateUserAdd()
        {
            var repo = new UserRepository();
            UserData user = new UserData
            {
                Name = "Test",
                Email = "test@gmail.com",
                Password = "test@123",
                Address = "Test Address",
                Contact = 1234567890,
            };
            
            var result = repo.CreateUser(user);
            Assert.AreEqual("Test", result[0]);
        }
    }
}
