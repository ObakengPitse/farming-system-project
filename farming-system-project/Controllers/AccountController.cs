using farming_system_project.Data;
using Microsoft.AspNetCore.Mvc;
using farming_system_project.Models;
using System.Diagnostics;

namespace farming_system_project.Controllers
{
    public class AccountController: Controller
    {
        private readonly Data.AppDbContext _context;

        public AccountController(Data.AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            Console.WriteLine("Login method called");

            // Check in Farmer table
            var farmer = _context.Farmer.FirstOrDefault(f => f.Email == model.Email && f.Password == model.Password);
            if (farmer != null)
            {
                Console.WriteLine("Login method Inside");
                HttpContext.Session.SetString("UserRole", "Farmer");
                HttpContext.Session.SetInt32("UserId", farmer.Id);
                return RedirectToAction("Dashboard", "Farmer");
            }

            // Check in Employee table
            var employee = _context.Employee.FirstOrDefault(e => e.Email == model.Email && e.Password == model.Password);
            if (employee != null)
            {
                HttpContext.Session.SetString("UserRole", "Employee");
                HttpContext.Session.SetInt32("UserId", employee.Id);
                return RedirectToAction("Dashboard", "Employee");
            }

            ModelState.AddModelError("", "Invalid email or password");
            return View(model);
        }
        public IActionResult Logout()
        {
            // Perform logout logic here
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
  
}
