using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Portal_Project.Data;
using Portal_Project.Models.Portal;
using Portal_Project.Models.Portal.DMC;

namespace Portal_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
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

        // GET: Admin/Product
        public async Task<IActionResult> Index()
        {
            List<Product> model = await _context.Products
                                                .ToListAsync();

            return View(model);
        }

        // GET: Admin/Product/Create
        public IActionResult Create()
        {
            string username = HttpContext.User.Identity.Name;
            string userId = _userManager.FindByNameAsync(username).Result.Id;

            ViewData["UserID"] = userId;

            return View();
        }

        // POST: Admin/Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductID,UserID,Name,BasePrice")] Product model)
        {
            if (ModelState.IsValid)
            {
                model.Status = Product_Status.OnSale;

                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Admin/Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product model = await _context.Products
                                          .FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            bool isEditable = model.Status != Product_Status.Saled ? true : false;
            ViewData["IsEditable"] = isEditable;

            return View(model);
        }

        // POST: Admin/Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductID,UserID,Name,BasePrice,Status")] Product model)
        {
            if (id != model.ProductID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            bool isEditable = model.Status == Product_Status.Canceled ? true : false;
            ViewData["IsEditable"] = isEditable;

            return View(model);
        }

        // GET: Admin/Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product model = await _context.Products
                                          .FirstOrDefaultAsync(m => m.ProductID == id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // GET: Admin/Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product model = await _context.Products
                                          .FirstOrDefaultAsync(m => m.ProductID == id);

            if (model == null)
            {
                return NotFound();
            }

            bool isDeletable = await _context.Bids
                                             .AnyAsync(b => b.ProductID == model.ProductID);

            ViewData["IsDeletable"] = isDeletable;

            return View(model);
        }

        // POST: Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Product model = await _context.Products
                                          .FindAsync(id);

            _context.Products
                    .Remove(model);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Bids(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product product = await _context.Products
                                            .FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            List<Bid> model = await _context.Bids
                                            .Include(b => b.ApplicationUser)
                                            .Where(b => b.ProductID == id)
                                            .OrderByDescending(b => b.Price)
                                            .ToListAsync();

            string winner = "- - -";
            
            if (product.Status == Product_Status.Saled)
            {
                Bid winBid = await _context.Bids
                                           .Where(b => b.ProductID == id)
                                           .OrderByDescending(b => b.Price)
                                           .FirstOrDefaultAsync();

                ApplicationUser user = _userManager.FindByIdAsync(winBid.UserID).Result;

                winner = user.FirstName + " " + user.LastName;
            }

            ViewData["Product"] = product;
            ViewData["Winner"] = winner;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int? id)
        {
            Product model = await _context.Products
                                          .FindAsync(id);

            model.Status = Product_Status.Canceled;
            
            _context.Update(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Product", new { area = "Admin" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Close(int? id)
        {
            Product model = await _context.Products
                                          .FindAsync(id);

            model.Status = Product_Status.Saled;

            _context.Update(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Product", new { area = "Admin" });
        }
    }
}