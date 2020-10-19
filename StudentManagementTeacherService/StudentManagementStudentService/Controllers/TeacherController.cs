using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementStudentService.Models;
using StudentManagementStudentService.Repository;

namespace StudentManagementTeacherService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
           ITeacher adb;
          readonly log4net.ILog _log4net;
        public TeacherController(ITeacher _adb)
            {
                adb = _adb;
               _log4net = log4net.LogManager.GetLogger(typeof(TeacherController));
        }

            [HttpGet]
            [Route("GetList")]
            public IActionResult GetList()
            {
               _log4net.Info(" Http GET request Started");
              try
                {
                    var details = adb.GetMarksList();
                    if (details == null)
                    {
                        return NotFound();
                    }
                _log4net.Info(" Http GET request Completed");
                return Ok(details);
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
                _log4net.Info(" Http GET By ID request Started");
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
        public IActionResult UpdateDetail([FromBody] MarksDetails model)
        {
            _log4net.Info(" Http PUT request Started");
            try
            {
                int val = adb.UpdateMarks(model);
                if (val == 1)
                {
                    _log4net.Info(" Http PUT request Completed");
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