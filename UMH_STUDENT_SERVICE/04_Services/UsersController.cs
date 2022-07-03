using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UMH_STUDENT_SERVICE._03_BussinesLayer;
using UMH_STUDENT_SERVICE.Context;
using UMH_STUDENT_SERVICE.Models;
using UMH_STUDENT_SERVICE.Utils;

namespace UMH_STUDENT_SERVICE.Controllers
{
    [Route("app-umh-x/api/user/v1.0/student-management/")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        
        private readonly IBlUser _BlUsers;

        public UsersController(IBlUser blUsers)
        {
            _BlUsers = blUsers;
        }

        // GET: 
        [HttpPost("login/Retrieve")]
        public object Login()
        {
            try
            {
                var result = _BlUsers.LoginUser(Request.Headers["Authorization"]);

                if (!string.IsNullOrEmpty(result.Error))
                {
                    // Add table log
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPut("update-password/process")]
        public object PutPasswordUser([FromBody] string newPassword)
        {
            try
            {
                var result = _BlUsers.UpdatePassword(Request.Headers["Authorization"], newPassword);

                if (!string.IsNullOrEmpty(result.Error))
                {
                    // Add table log
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            /**
             
            

  }
            }
             */

            return NoContent();
        }

        // POST: Crear usuarios
        [HttpPost("register-student/create")]
        public async Task<object> PostUser(User user)
        {
            
            var result = _BlUsers.RegisterUser(Request.Headers["Authorization"],user);

            if (!string.IsNullOrEmpty(result.Error))
            {
                // Add table log
                return BadRequest(result);
            }
            return Ok(result);
        }

        private bool UserExists(string id)
        {
            //return (_context.Users?.Any(e => e.NumberAcount == id)).GetValueOrDefault();
            return true;
        }
    }
}
