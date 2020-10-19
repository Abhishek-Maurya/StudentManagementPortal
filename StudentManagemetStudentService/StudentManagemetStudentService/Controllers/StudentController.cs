using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagemetStudentService.Models;
using StudentManagemetStudentService.Repository;

namespace StudentManagemetStudentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        IStudent adb;
        readonly log4net.ILog _log4net;
        public StudentController(IStudent _adb)
        {
            adb = _adb;
            _log4net = log4net.LogManager.GetLogger(typeof(StudentController));
        }
        [HttpGet]
        [Route("GetDetail/{Id}")]
        public IActionResult GetDetail(int? Id)
        {
            _log4net.Info(" Http GET by ID request Started");
            if (Id == null)
            {
                return BadRequest();
            }
            try
            {
                var data = adb.GetDetail(Id);
                if (data == null)
                {
                    return NotFound();
                }
                _log4net.Info(" Http GET By ID request Completed");
                return Ok(data);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("GetMarks/{Id}")]
        public IActionResult GetMarks(int? Id)
        {
            _log4net.Info(" Http GET By IDrequest Started");
            if (Id == null)
            {
                return BadRequest();
            }
            try
            {
                var data = adb.GetMarks(Id);
                if (data == null)
                {
                    return NotFound();
                }
                _log4net.Info(" Http GET By ID request Completed");
                return Ok(data);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("UpdateDetail")]
        public IActionResult UpdateDetail([FromBody] BasicDetails model)
        {
            _log4net.Info(" Http PUT request Started");
            try
            {
                int val = adb.UpdateDetail(model);
                if (val == 1)
                {
                    _log4net.Info(" Http PUT request completed");
                    return Ok(val);                    
                }
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                if (ex.GetType().FullName ==
                         "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                {
                    return NotFound();
                }

                return BadRequest();
            }
        }
    }
}
