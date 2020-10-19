using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementSystem.Repository
{
    public interface IAdmin
    {
        public int AddDetail(BasicDetails data);

        public int DeleteDetail(int? Id);

        public BasicDetails GetDetail(int? Id);

        public List<BasicDetails> GetDetails();

        public int UpdateDetail(BasicDetails model);
    }
}
