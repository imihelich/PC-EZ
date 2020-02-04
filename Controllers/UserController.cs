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
    public class UserController : Controller
    {
        private readonly UserDbContext context;

        private User activeUser;

        public List<User> userList;

        public bool ValidAccount(string username, string password) //determines if an account with the same username and password exists
        {
            userList = context.Users.ToList();
            foreach (User user in userList)
            {
                if (user.Username.ToLower().Equals(username.ToLower()) && user.Password.Equals(password)) // password is case sensitive
                {
                    return true;
                }
            }
            return false;
        }

        public User FindByUsername(string username)
        {
            foreach (User user in context.Users.ToList())
            {
                if (username.ToLower().Equals(user.Username.ToLower()))
                {
                    return context.Users.Find(user.ID);
                }
            }
            return null;
        }

        public UserController(UserDbContext dbContext)
        {
            context = dbContext;
        }

        [HttpGet]
        public IActionResult Index(string username)
        {
            if (username != null)
            {
                ViewUserViewModel viewUser = new ViewUserViewModel
                {
                    User = FindByUsername(username)
                };
                return View("ViewUser",viewUser);
            }
            userList = context.Users.ToList();
            return View(userList);
        }

        public IActionResult Add()
        {
            AddUserViewModel addUserViewModel = new AddUserViewModel();
            return View(addUserViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddUserViewModel addUserViewModel)
        {
            if (ModelState.IsValid) // checks that form model is valid before proceeding
            {
                if (FindByUsername(addUserViewModel.Username) == null) //checks that username is not in use before adding new account
                {
                    User newUser = new User
                    {
                        Username = addUserViewModel.Username,
                        Password = addUserViewModel.Password,
                        SkillLevel = addUserViewModel.SkillLevel
                    };
                    context.Users.Add(newUser);
                    context.SaveChanges();
                    activeUser = context.Users.Find(newUser.ID);
                    return RedirectToAction("Index", "User", new { username = newUser.Username });
                }

                AddUserViewModel addError = new AddUserViewModel("An account with this username already exists - please try a different username.");
                return View(addError); // return with error message for existing account
            }

            return View(addUserViewModel); // form invalid return
        }

        public IActionResult Login() // render login form -> post form to Post login to be verified
        {
            LoginViewModel loginViewModel = new LoginViewModel();
            return View(loginViewModel);
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                if (ValidAccount(loginViewModel.Username, loginViewModel.Password) == true)
                {
                    activeUser = FindByUsername(loginViewModel.Username);
                    return RedirectToAction("Index","User", new {username = loginViewModel.Username});
                }
                if (FindByUsername(loginViewModel.Username) == null)
                {
                    LoginViewModel errorLogin = new LoginViewModel("Account does not exist.");
                    return View(errorLogin);
                }
                else
                {
                    LoginViewModel errorLogin = new LoginViewModel("Incorrect password");
                    return View(errorLogin);
                }
            }
            return View(loginViewModel); // invalid form return
        } 
        
    }
}
