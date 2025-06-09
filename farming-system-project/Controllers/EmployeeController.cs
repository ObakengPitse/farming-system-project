using farming_system_project.Data;
using farming_system_project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace farming_system_project.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly Data.AppDbContext _context;

        public EmployeeController(Data.AppDbContext context)
        {
            _context = context;
        }

        // GET: /Employee/Dashboard
        public IActionResult Dashboard()
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Employee")
                return RedirectToAction("Login", "Account");

            var farmers = _context.Farmer
                .OrderBy(f => f.Name)
                .ToList();

            return View(farmers);
        }

        // GET: /Employee/AddFarmer
        public IActionResult AddFarmer()
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Employee")
                return RedirectToAction("Login", "Account");

            return View();
        }

        // POST: /Employee/AddFarmer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddFarmer(Farmer model)
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Employee")
                return RedirectToAction("Login", "Account");

            _context.Farmer.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Dashboard");
        }

        // GET: /Employee/ViewFarmerProducts
        public IActionResult ViewFarmerProducts(int farmerId, DateTime? start, DateTime? end, string category)
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Employee")
                return RedirectToAction("Login", "Account");

            var query = _context.Product
                .Include(p => p.Farmer)
                .Where(p => p.FarmerId == farmerId);

            if (start.HasValue)
                query = query.Where(p => p.ProductionDate >= start.Value);

            if (end.HasValue)
                query = query.Where(p => p.ProductionDate <= end.Value);

            if (!string.IsNullOrWhiteSpace(category))
                query = query.Where(p => p.Category == category);

            var products = query
                .OrderByDescending(p => p.ProductionDate)
                .ToList();

            ViewBag.Farmer = _context.Farmer.FirstOrDefault(f => f.Id == farmerId);
            ViewBag.Start = start;
            ViewBag.End = end;
            ViewBag.Category = category;

            return View(products);
        }

        public IActionResult Logout()
        {
            // Perform logout logic here
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}
