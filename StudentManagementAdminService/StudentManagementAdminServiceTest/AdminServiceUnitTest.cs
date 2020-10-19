using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using StudentManagementSystem.Controllers;
using StudentManagementSystem.Models;
using StudentManagementSystem.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentManagementAdminServiceTest
{
    public class Tests
    {
        StudentManagementContext db;
        [SetUp]
        public void Setup()
        {
            var details = new List<BasicDetails>
            {
                new BasicDetails{Id = 1, Name="Dummy1",Gender="Male",Address="Dummy Address 1",DateOfBirth=DateTime.Parse("11/23/2010"),Phone="1122334455",Email="dummy1@gmail.com",Course="course1",CourseDuration=4,FeeStatus="Paid"},
                new BasicDetails{Id = 2, Name="Dummy2",Gender="Female",Address="Dummy Address 2",DateOfBirth=DateTime.Parse("12/28/2010"),Phone="2233445566",Email="dummy1@gmail.com",Course="course2",CourseDuration=3,FeeStatus="Due"},
                new BasicDetails{Id = 3, Name="Dummy3",Gender="Male",Address="Dummy Address 3",DateOfBirth=DateTime.Parse("05/14/2010"),Phone="3344556677",Email="dummy1@gmail.com",Course="course1",CourseDuration=4,FeeStatus="Paid"},
                new BasicDetails{Id = 4, Name="Dummy4",Gender="Female",Address="Dummy Address 4",DateOfBirth=DateTime.Parse("08/10/2010"),Phone="4455667788",Email="dummy1@gmail.com",Course="course2",CourseDuration=3,FeeStatus="Due"}
            };

            var marksdetails = new List<MarksDetails>
            {
                new MarksDetails{Id =1, CurrentYear = 1, Sem1 = 20, Sem2 = 20, Sem3 = 20, Sem4 = 20,Sem5 = 20 ,Sem6 = 20, Sem7 = 20, Sem8 = 20, TotalMaxMarks = 400, TotalObtainedMarks = 160, Percentage = 40 },
                new MarksDetails{Id =2, CurrentYear = 2, Sem1 = 30, Sem2 = 30, Sem3 = 30, Sem4 = 30,Sem5 = 30 ,Sem6 = 30, Sem7 = 30, Sem8 = 30, TotalMaxMarks = 400, TotalObtainedMarks = 240, Percentage = 48 },
                new MarksDetails{Id =3, CurrentYear = 3, Sem1 = 40, Sem2 = 40, Sem3 = 40, Sem4 = 40,Sem5 = 40 ,Sem6 = 40, Sem7 = 40, Sem8 = 40, TotalMaxMarks = 400, TotalObtainedMarks = 320, Percentage = 64 },
                new MarksDetails{Id =4, CurrentYear = 4, Sem1 = 50, Sem2 = 50, Sem3 = 50, Sem4 = 50,Sem5 = 50 ,Sem6 = 50, Sem7 = 50, Sem8 = 50, TotalMaxMarks = 400, TotalObtainedMarks = 400, Percentage = 100 }
            };

            var logindetails = new List<LoginCredentials>
            {
                new LoginCredentials{UserName="dummy1@gmail.com",Password="1122334455",Role="Student"},
                new LoginCredentials{UserName="dummy2@gmail.com",Password="2233445566",Role="Student"},
                new LoginCredentials{UserName="dummy3@gmail.com",Password="3344556677",Role="Student"},
                new LoginCredentials{UserName="dummy4@gmail.com",Password="4455667788",Role="Student"},
            };

            var detaildata = details.AsQueryable();
            var mockSet = new Mock<DbSet<BasicDetails>>();
            mockSet.As<IQueryable<BasicDetails>>().Setup(m => m.Provider).Returns(detaildata.Provider);
            mockSet.As<IQueryable<BasicDetails>>().Setup(m => m.Expression).Returns(detaildata.Expression);
            mockSet.As<IQueryable<BasicDetails>>().Setup(m => m.ElementType).Returns(detaildata.ElementType);
            mockSet.As<IQueryable<BasicDetails>>().Setup(m => m.GetEnumerator()).Returns(detaildata.GetEnumerator());
            var mockContext = new Mock<StudentManagementContext>();
            mockContext.Setup(c => c.BasicDetails).Returns(mockSet.Object);

            var marksdata = marksdetails.AsQueryable();
            var mockSetMarks = new Mock<DbSet<MarksDetails>>();
            mockSetMarks.As<IQueryable<MarksDetails>>().Setup(m => m.Provider).Returns(marksdata.Provider);
            mockSetMarks.As<IQueryable<MarksDetails>>().Setup(m => m.Expression).Returns(marksdata.Expression);
            mockSetMarks.As<IQueryable<MarksDetails>>().Setup(m => m.ElementType).Returns(marksdata.ElementType);
            mockSetMarks.As<IQueryable<MarksDetails>>().Setup(m => m.GetEnumerator()).Returns(marksdata.GetEnumerator());
            mockContext.Setup(c => c.MarksDetails).Returns(mockSetMarks.Object);

            var logindata = logindetails.AsQueryable();
            var mockSetLogin = new Mock<DbSet<LoginCredentials>>();
            mockSetLogin.As<IQueryable<LoginCredentials>>().Setup(m => m.Provider).Returns(logindata.Provider);
            mockSetLogin.As<IQueryable<LoginCredentials>>().Setup(m => m.Expression).Returns(logindata.Expression);
            mockSetLogin.As<IQueryable<LoginCredentials>>().Setup(m => m.ElementType).Returns(logindata.ElementType);
            mockSetLogin.As<IQueryable<LoginCredentials>>().Setup(m => m.GetEnumerator()).Returns(logindata.GetEnumerator());
            mockContext.Setup(c => c.LoginCredentials).Returns(mockSetLogin.Object);

            db = mockContext.Object;
        }


        [Test]
        public void get_Valid_Detail()
        {
            Admin admindata = new Admin(db);
            AdminController obj = new AdminController(admindata);
            var data = obj.GetDetail(2);
            var okResult = data as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void get_all_Valid_Detail()
        {
            Admin admindata = new Admin(db);
            AdminController obj = new AdminController(admindata);
            var data = obj.GetDetails();
            var okResult = data as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void Delete_Valid_Detail()
        {
            Admin admindata = new Admin(db);
            AdminController obj = new AdminController(admindata);
            var data = obj.DeleteDetail(1);
            var okResult = data as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void Add_Valid_Detail()
        {
            Admin admindata = new Admin(db);
            AdminController obj = new AdminController(admindata);
            BasicDetails dummydata = new BasicDetails()
            {
                Name = "Dummy5",
                Gender = "Female",
                Address = "Dummy Address 5",
                DateOfBirth = DateTime.Parse("11/23/2010"),
                Phone = "1122334400",
                Email = "dummy5@gmail.com",
                Course = "course1",
                CourseDuration = 4,
                FeeStatus = "Paid"
            };
            var data = obj.AddDetail(dummydata);
            var okResult = data as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
        }
        [Test]
        public void update_Valid_Detail()
        {
            Admin admindata = new Admin(db);
            AdminController obj = new AdminController(admindata);
            BasicDetails dummydata = new BasicDetails()
            {
                Id = 1,
                Name = "Dummy1",
                Gender = "Male",
                Address = "Dummy Address 1",
                DateOfBirth = DateTime.Parse("11/23/2010"),
                Phone = "1122334455",
                Email = "dummy1@gmail.com",
                Course = "course1",
                CourseDuration = 4,
                FeeStatus = "due"
            };
            var data = obj.UpdateDetail(dummydata);
            var okResult = data as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
        }
    }
}
       