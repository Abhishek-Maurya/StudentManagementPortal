using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using StudentManagementStudentService.Models;
using StudentManagementStudentService.Repository;
using StudentManagementTeacherService.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NUnitTestProject1
{
    public class Tests
    {
        StudentManagementContext db;
        [SetUp]
        public void Setup()
        {
           var marksdetails = new List<MarksDetails>
            {
                new MarksDetails{Id =1, CurrentYear = 1, Sem1 = 20, Sem2 = 20, Sem3 = 20, Sem4 = 20,Sem5 = 20 ,Sem6 = 20, Sem7 = 20, Sem8 = 20, TotalMaxMarks = 400, TotalObtainedMarks = 160, Percentage = 40 },
                new MarksDetails{Id =2, CurrentYear = 2, Sem1 = 30, Sem2 = 30, Sem3 = 30, Sem4 = 30,Sem5 = 30 ,Sem6 = 30, Sem7 = 30, Sem8 = 30, TotalMaxMarks = 400, TotalObtainedMarks = 240, Percentage = 48 },
                new MarksDetails{Id =3, CurrentYear = 3, Sem1 = 40, Sem2 = 40, Sem3 = 40, Sem4 = 40,Sem5 = 40 ,Sem6 = 40, Sem7 = 40, Sem8 = 40, TotalMaxMarks = 400, TotalObtainedMarks = 320, Percentage = 64 },
                new MarksDetails{Id =4, CurrentYear = 4, Sem1 = 50, Sem2 = 50, Sem3 = 50, Sem4 = 50,Sem5 = 50 ,Sem6 = 50, Sem7 = 50, Sem8 = 50, TotalMaxMarks = 400, TotalObtainedMarks = 400, Percentage = 100 }
            };                    

            var marksdata = marksdetails.AsQueryable();
            var mockSetMarks = new Mock<DbSet<MarksDetails>>();
            mockSetMarks.As<IQueryable<MarksDetails>>().Setup(m => m.Provider).Returns(marksdata.Provider);
            mockSetMarks.As<IQueryable<MarksDetails>>().Setup(m => m.Expression).Returns(marksdata.Expression);
            mockSetMarks.As<IQueryable<MarksDetails>>().Setup(m => m.ElementType).Returns(marksdata.ElementType);
            mockSetMarks.As<IQueryable<MarksDetails>>().Setup(m => m.GetEnumerator()).Returns(marksdata.GetEnumerator());
            var mockContext = new Mock<StudentManagementContext>();
            mockContext.Setup(c => c.MarksDetails).Returns(mockSetMarks.Object);

            db = mockContext.Object;
        }

        [Test]
        public void get_Valid_Detail()
        {
            Teacher teacherndata = new Teacher(db);
            TeacherController obj = new TeacherController(teacherndata);
            var data = obj.GetList();
            var okResult = data as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void get_Valid_Detail_By_ID()
        {
            Teacher teacherndata = new Teacher(db);
            TeacherController obj = new TeacherController(teacherndata);
            var data = obj.GetMarks(1);
            var okResult = data as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
        }
        [Test]
        public void update_Valid_Detail()
        {
            Teacher teacherndata = new Teacher(db);
            TeacherController obj = new TeacherController(teacherndata);
            MarksDetails dummymarks = new MarksDetails()
            {
                Id = 1,
                CurrentYear = 1,
                Sem1 = 10,
                Sem2 = 10,
                Sem3 = 10,
                Sem4 = 10,
                Sem5 = 10,
                Sem6 = 10,
                Sem7 = 10,
                Sem8 = 10,
                TotalMaxMarks = 400,
                TotalObtainedMarks = 100,
                Percentage = 25
            };
            var data = obj.UpdateDetail(dummymarks);
            var okResult = data as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
        }
    }
}
