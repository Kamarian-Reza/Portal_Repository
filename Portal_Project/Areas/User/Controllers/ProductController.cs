using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Portal_Project.Data;
using Portal_Project.Models.Portal;
using Portal_Project.Models.Portal.DMC;

namespace Portal_Project.Areas.User.Controllers
{
    [Area("User")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductController(ApplicationDbContext context,
                                 UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: User/Product
        public async Task<IActionResult> Index()
        {
            List<Product> model = await _context.Products
                                                .Include(b => b.Bids)
                                                .ToListAsync();

            string username = HttpContext.User.Identity.Name;
            string userId = _userManager.FindByNameAsync(username).Result.Id;

            ViewData["UserID"] = userId;

            return View(model);
        }

        // GET: User/Product/Create
        public async Task<IActionResult> Bid(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product product = await _context.Products
                                            .Where(p => p.ProductID == id)
                                            .FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound();
            }

            string username = HttpContext.User.Identity.Name;
            string userId = _userManager.FindByNameAsync(username).Result.Id;

            ViewData["UserID"] = userId;
            ViewData["Product"] = product;

            return View();
        }

        // POST: User/Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Bid([Bind("BidID,ProductID,UserID,Price")] Bid model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            Product product = await _context.Products
                                            .Where(p => p.ProductID == model.ProductID)
                                            .FirstOrDefaultAsync();

            string username = HttpContext.User.Identity.Name;
            string userId = _userManager.FindByNameAsync(username).Result.Id;

            ViewData["UserID"] = userId;
            ViewData["Product"] = product;

            return View(product);
        }
    }
}