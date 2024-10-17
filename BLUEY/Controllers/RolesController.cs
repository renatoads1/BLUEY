﻿using BLUEY.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BLUEY.Controllers
{
    [Authorize(Policy = "AdminPolicy")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAspnetUserRolesRepository _aspnetUserRolesRepository;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, IAspnetUserRolesRepository aspnetUserRolesRepository)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _aspnetUserRolesRepository = aspnetUserRolesRepository;
        }

        // Método para listar todos os roles
        
        public async Task<IActionResult> Index()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        // Método para criar um novo role
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult AddRegraUser() 
        {
            var usuarios = _userManager.Users; // Obter a lista de usuários
            ViewBag.Usuarios = usuarios; // Passa a lista de usuários usando ViewBag
            var regras = _roleManager.Roles.ToList();
            return View(regras);
        }

        [HttpPost]
        public IActionResult UpdateRoleUser(string userId, string roleId)
        {
            var user = userId;
            var role = roleId;
            var r = _aspnetUserRolesRepository.Set(user,role);

            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Create(string roleName)
        {
            if (!string.IsNullOrEmpty(roleName))
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View();
        }

        // Método para excluir um role
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                await _roleManager.DeleteAsync(role);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
