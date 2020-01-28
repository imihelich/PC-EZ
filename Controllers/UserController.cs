using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LCCapstone.Models;
using LCCapstone.Data;
using LCCapstone.ViewModels;

namespace LCCapstone.Controllers
{
    public class UserController :  Controller
    {
        private readonly UserDbContext context;

        public UserController(UserDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<User> userList = context.Users.ToList();
            return View(userList);
        }

        public IActionResult Add()
        {
            // Create & render new user form
            AddUserViewModel addUserViewModel = new AddUserViewModel();
            return View(addUserViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddUserViewModel addUserViewModel)
        {
            if (ModelState.IsValid)
            {

                User newUser = new User
                {
                    Username = addUserViewModel.Username,
                    Password = addUserViewModel.Password
                    // TODO: modify password to save with hashing
                };

                context.Users.Add(newUser);
                context.SaveChanges();
                return Redirect("/User");
            }

            return View(addUserViewModel); // If form invalid, render form again w/ error messages
        }
    }
}
