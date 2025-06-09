using Microsoft.AspNetCore.Mvc;
using farming_system_project.Data;
using farming_system_project.Models;
using Microsoft.EntityFrameworkCore;


namespace farming_system_project.Controllers
{
    public class FarmerController : Controller
    {
        private readonly Data.AppDbContext _context;

        public FarmerController(Data.AppDbContext context)
        {
            _context = context;
        }

        // GET: /Farmer/Dashboard
        public IActionResult Dashboard()
        {
            var role = HttpContext.Session.GetString("UserRole");
            var farmerId = HttpContext.Session.GetInt32("UserId");

            if (role != "Farmer" || farmerId == null)
                return RedirectToAction("Login", "Account");

            var products = _context.Product
                .Include(p => p.Farmer)
                .Where(p => p.FarmerId == farmerId)
                .OrderByDescending(p => p.ProductionDate)
                .ToList();

            return View(products);
        }

        // GET: /Farmer/AddProduct
        public IActionResult AddProduct()
        {
            var role = HttpContext.Session.GetString("UserRole");
            
            if (role != "Farmer")
                return RedirectToAction("Login", "Account");
            
            return View();
        }

        // POST: /Farmer/AddProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProduct(Product model)
        {
            var role = HttpContext.Session.GetString("UserRole");
            var farmerId = HttpContext.Session.GetInt32("UserId");

            if (role != "Farmer" || farmerId == null)
                return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid)
                return View(model);

            model.FarmerId = farmerId.Value;

            _context.Product.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Dashboard");
        }

        public IActionResult Logout()
        {
            // Perform logout logic here
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}
