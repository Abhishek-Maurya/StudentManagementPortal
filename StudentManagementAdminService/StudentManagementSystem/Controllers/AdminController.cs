using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Models;
using StudentManagementSystem.Repository;

namespace StudentManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        IAdmin adb;
        readonly log4net.ILog _log4net;
        public AdminController(IAdmin _adb)
         {
            adb = _adb;
            _log4net = log4net.LogManager.GetLogger(typeof(AdminController));
        }

        [HttpGet]
        [Route("GetDetails")]
        public IActionResult GetDetails()
        {
            _log4net.Info(" Http GET request Started");
            try
            {
                var details = adb.GetDetails();
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
                _log4net.Info(" Http GET by ID request Completed");
                return Ok(data);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("AddDetail")]
        public IActionResult AddDetail([FromBody] BasicDetails model)
        {
            _log4net.Info(" Http POST request Started");
            if (ModelState.IsValid)
            {
                try
                {
                    var Id = adb.AddDetail(model);
                    if (Id > 0)
                    {
                        _log4net.Info(" Http POST request Completed");
                        return Ok(Id);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {

                    return BadRequest();
                }
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("DeleteDetail/{Id}")]
        public IActionResult DeleteDetail(int Id)
        {
            
            _log4net.Info(" Http DELETE request Started");

            if (Id == null)
            {
                return BadRequest();
            }

            try
            {
                var result = adb.DeleteDetail(Id);
                if (result == 0)
                {
                    return NotFound();
                }
                _log4net.Info(" Http DELETE request Completed");
                return Ok(result);
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
                    int val=adb.UpdateDetail(model);
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
