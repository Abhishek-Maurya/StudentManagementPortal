using Microsoft.AspNetCore.Mvc;
using StudentManagementStudentService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementStudentService.Repository
{
    public class Teacher : ITeacher
    {
        StudentManagementContext db;
        public Teacher(StudentManagementContext _db)
        {
            db = _db;
        }
        public List<MarksDetails> GetMarksList()
        {
                if (db != null)
                {
                    return db.MarksDetails.ToList();
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

        public int UpdateMarks([FromBody] MarksDetails model)
        {
               if (db != null)
                {
                    db.MarksDetails.Update(model);
                    db.SaveChanges();
                    return 1;
                }
                return 0;
        }
    }
}
