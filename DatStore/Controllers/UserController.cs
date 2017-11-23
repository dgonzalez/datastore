using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatStore.Model;
using DatStore.Services;
using Google.Cloud.Datastore.V1;
using Microsoft.AspNetCore.Mvc;

namespace DatStore.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ArrayList Get()
        {
            // Assume is not null just for simplicity.
            int offset = Int32.Parse(HttpContext.Request.Query["offset"]);
            int limit = Int32.Parse(HttpContext.Request.Query["limit"]);
            string surname = HttpContext.Request.Query["surname"];
            if (surname == null) 
            {
                return _userService.FindUsers(offset, limit);
            }
            else
            {
                return _userService.FindUsersBySurname(offset, limit, surname);    
            }

        }

        [HttpPost]
        public Key Post([FromBody] User user)
        {
            return _userService.AddUser(user);
        }
    }
}
