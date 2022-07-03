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
    [Route("app-umh-x/api/user/v1.0/university-management/")]
    [ApiController]
    public class UniversityController : ControllerBase
    {

        private readonly IBlUniversity _BlUniversity;

        public UniversityController(IBlUniversity blUniversity)
        {
            _BlUniversity = blUniversity;
        }

        // GET: 
        [HttpGet("initial-catalog/Retrieve/Authorization/{Authorization}")]
        public object Login(string Authorization)
        {

            var result = _BlUniversity.InitialData(Authorization);

            if (!string.IsNullOrEmpty(result.Error))
            {
                // Add table log
                return BadRequest(result);
            }
            return Ok(result);

        }

    }
}
