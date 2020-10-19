using Microsoft.AspNetCore.Mvc;
using StudentManagemetStudentService.Models;
using System.Linq;

namespace StudentManagemetStudentService.Repository
{
    public class Student : IStudent
    {
        StudentManagementContext db;
        public Student(StudentManagementContext _db)
        {
            db = _db;
        }
        public BasicDetails GetDetail(int? Id)
        {
            if (db != null)
            {
                return (db.BasicDetails.Where(x => x.Id == Id)).FirstOrDefault();
            }
            return null;
        }
        public MarksDetails GetMarks(int? Id)
        {
            if (db != null)
            {
                return (db.MarksDetails.Where(x => x.Id == Id)).FirstOrDefault();
            }
            return null;
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