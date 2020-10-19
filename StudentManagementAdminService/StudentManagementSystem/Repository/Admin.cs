using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementSystem.Repository
{
    public class Admin : IAdmin
    {
        StudentManagementContext db;
        public Admin(StudentManagementContext _db)
        {
            db = _db;
        }
        public List<BasicDetails> GetDetails()
        {
            {
                if (db != null)
                {
                    return db.BasicDetails.ToList();
                }
                return null;
            }
        }

        public BasicDetails GetDetail(int? Id)
        {
            if (db != null)
            {
                return (db.BasicDetails.Where(x => x.Id == Id)).FirstOrDefault();
            }
            return null;
        }

        public int AddDetail(BasicDetails data)
        {
            if (db != null)
            {
                
                db.BasicDetails.Add(data);
                db.SaveChanges();
                MarksDetails marks = new MarksDetails()
                   { Id = data.Id, CurrentYear = null, Sem1 = null, Sem2 = null, Sem3 = null, Sem4 = null,
                    Sem5 = null, Sem6 = null, Sem7 = null, Sem8 = null, TotalMaxMarks = null, TotalObtainedMarks = null, Percentage = null };
                LoginCredentials newuser = new LoginCredentials()
                {
                    UserName = data.Email,
                    Password = data.Phone,
                    Role = "Student"
                };
                   db.MarksDetails.Add(marks);
                   db.LoginCredentials.Add(newuser);
                   db.SaveChanges();
                   return data.Id;
            }

            return 0;
        }

        public int DeleteDetail(int? Id)
        {
            int result = 0;

            if (db != null)
            {

                var data = db.BasicDetails.FirstOrDefault(x => x.Id == Id);
                var data2= db.MarksDetails.FirstOrDefault(x => x.Id == Id);
                var data3 = db.LoginCredentials.FirstOrDefault(x => x.UserName == data.Email);

                if (data != null)
                {

                    db.BasicDetails.Remove(data);
                    db.MarksDetails.Remove(data2);
                    db.LoginCredentials.Remove(data3);

                    result = db.SaveChanges();
                }
                return result;
            }

            return result;
        }
        public int UpdateDetail([FromBody] BasicDetails model)
        {
                if (db != null)
                   {
                     db.BasicDetails.Update(model);
                      db.SaveChanges();
                      return 1;
                 }
                return 0;
        }
    }
}