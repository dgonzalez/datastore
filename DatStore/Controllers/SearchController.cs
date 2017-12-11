using System;
using System.Collections;
using DatStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace DatStore.Controllers
{
    [Route("api/user/search")]
    public class SearchController: Controller
    {
        private readonly IUserService _userService;

        public SearchController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ArrayList Get()
        {
            string surname = HttpContext.Request.Query["surname"];
            string name = HttpContext.Request.Query["name"];

            return _userService.FindUsersByNameAndSurname(0, 1000, name, surname);
        }
    }
}
