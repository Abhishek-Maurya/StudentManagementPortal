using Microsoft.AspNetCore.Mvc;
using StudentManagementStudentService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementStudentService.Repository
{
    public interface ITeacher
    {
        public List<MarksDetails> GetMarksList();
        public MarksDetails GetMarks(int? Id);
        public int UpdateMarks([FromBody] MarksDetails model);
    }
}
