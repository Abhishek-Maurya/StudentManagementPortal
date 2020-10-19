using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using StudentManagementAuthorization.Controllers;
using StudentManagementAuthorization.Models;
using System.Collections.Generic;
using System.Linq;

namespace StudentMamagementAuthoriztionTest
{
    public class Tests
    {
        StudentManagementContext db;
        [SetUp]
        public void Setup()
        {
            var logindetails = new List<LoginCredentials>
            {
                new LoginCredentials{UserName="dummyadmin@gmail.com",Password="123456789",Role="Admin" },
                new LoginCredentials{UserName="dummyteacher@gmail.com",Password="123456789",Role="Teacher"},
                new LoginCredentials{UserName="dummystudent@gmail.com",Password="123456789",Role="Student" }
            };

            var logindata = logindetails.AsQueryable();
            var mockSetLogin = new Mock<DbSet<LoginCredentials>>();
            mockSetLogin.As<IQueryable<LoginCredentials>>().Setup(m => m.Provider).Returns(logindata.Provider);
            mockSetLogin.As<IQueryable<LoginCredentials>>().Setup(m => m.Expression).Returns(logindata.Expression);
            mockSetLogin.As<IQueryable<LoginCredentials>>().Setup(m => m.ElementType).Returns(logindata.ElementType);
            mockSetLogin.As<IQueryable<LoginCredentials>>().Setup(m => m.GetEnumerator()).Returns(logindata.GetEnumerator());
            var mockContext = new Mock<StudentManagementContext>();
            mockContext.Setup(c => c.LoginCredentials).Returns(mockSetLogin.Object);
            db = mockContext.Object;
        }

        [Test]
        public void Authorised_Student_Login()
        {
            LoginController obj = new LoginController(db);
            LoginCredentials nuser = new LoginCredentials()
            {
                UserName = "dummystudent@gmail.com",
                Password = "123456789",
                Role = "Student"
            };
            var data = obj.userLogin(nuser);
            var okResult = data as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
        }
        [Test]
        public void Authorised_Admin_Login()
        {
            LoginController obj = new LoginController(db);
            LoginCredentials nuser = new LoginCredentials()
            {
                UserName = "dummyadmin@gmail.com",
                Password = "123456789",
                Role = "Admin"
            };
            var data = obj.userLogin(nuser);
            var okResult = data as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
        }
        [Test]
        public void Authorised_Teacher_Login()
        {
            LoginController obj = new LoginController(db);
            LoginCredentials nuser = new LoginCredentials()
            {
                UserName = "dummyteacher@gmail.com",
                Password = "123456789",
                Role = "Teacher"
            };
            var data = obj.userLogin(nuser);
            var okResult = data as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
        }
        [Test]
        public void Unauthorised_Login()
        {
            LoginController obj = new LoginController(db);
            LoginCredentials nuser = new LoginCredentials()
            {
                UserName = "wrong",
                Password = "wrong",
                Role = "wrong"
            };
            var data = obj.userLogin(nuser);
            var result = data as UnauthorizedResult;
            Assert.AreEqual(401, result.StatusCode);
        }
    }
}