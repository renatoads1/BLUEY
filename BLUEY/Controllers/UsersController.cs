using BLUEY.Models;
using BLUEY.Models.Repositories;
using BLUEY.Models.ViewModels;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Security;

namespace BLUEY.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "AdminPolicy")]
    public class UsersController : Controller
    {
        private readonly IAspNetUsersRepository _usersRepository;

        public UsersController(IAspNetUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public IActionResult Index()
        {
            var users = _usersRepository.GetUserRoles();
            return View(users);
        }

        [HttpGet("details/{id}")]
        public IActionResult Details(string id)
        {
            var user = _usersRepository.Get(id);
            //var user = _usersRepository.GetUserRoles();
            List<Users> Usuarios = new List<Users>();
            Usuarios.Add(user);
            return View("Index", Usuarios);
        }

        //[HttpGet("new")]
        //public IActionResult New()
        //{
        //    Users user = new Users();

        //    user.Id = Guid.NewGuid().ToString();
        //    user.UserName = "Renato lindo maravilhoso";
        //    user.Email = "renatorealsis@gmail.com";
        //    user.EmailConfirmed = false;
        //    user.NormalizedUserName         = "renatorealsis";
        //    user.NormalizedEmail            = "renatorealsis@gmail.com";
        //    user.EmailConfirmed             = false;
        //    user.PasswordHash               = "r3n4t0321";
        //    user.SecurityStamp              = "r3n4t0321";
        //    user.ConcurrencyStamp           = "";
        //    user.PhoneNumber                = "";
        //    user.PhoneNumberConfirmed       = false;
        //    user.TwoFactorEnabled           = false;
        //    user.LockoutEnd                 = new DateTime().AtNoon();
        //    user.LockoutEnabled             = false;
        //    user.AccessFailedCount = 0;

        //    var _user = _usersRepository.Set(user);
        //    List<Users> Usuarios = new List<Users>();
        //    Usuarios.Add(_user);
        //    return View("Index", Usuarios);
        //}

    }
}
