using Microsoft.AspNetCore.Mvc;
using StudentManagemetStudentService.Models;

namespace StudentManagemetStudentService.Repository
{
    public interface IStudent
    {
        public BasicDetails GetDetail(int? Id);
        public MarksDetails GetMarks(int? Id);
        public int UpdateDetail([FromBody] BasicDetails model);
    }
}
