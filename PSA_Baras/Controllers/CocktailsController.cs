using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PSA_Baras.Data;
using PSA_Baras.Models;

namespace PSA_Baras.Controllers
{
    public class CocktailsController : Controller
    {
        private readonly BarasDBContext _context;

        public CocktailsController(BarasDBContext context)
        {
            _context = context;
        }

        // GET: Cocktails
        public async Task<IActionResult> Index()
        {
            var cocktails = await _context.Cocktail
                .Include(o => o.cocktailProducts)
                .ThenInclude(o => o.product)
                .ToListAsync();
            return View(cocktails);
        }

        // GET: Cocktails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cocktail = await _context.Cocktail
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cocktail == null)
            {
                return NotFound();
            }

            return View(cocktail);
        }

        // GET: Cocktails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cocktails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,title,price,color,proof,category")] Cocktail cocktail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cocktail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cocktail);
        }

        // GET: Cocktails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cocktail = await _context.Cocktail
                .Include(o => o.cocktailProducts)
                .ThenInclude(o => o.product)
                .FirstOrDefaultAsync(o => o.Id == id);
            if (cocktail == null)
            {
                return NotFound();
            }
            Populate(cocktail);
            return View(cocktail);
        }

        private void Populate(Cocktail cocktail)
        {
            var allProducts = _context.Product;
            var cocktailProducts = new HashSet<int>(cocktail.cocktailProducts.Select(c => c.Id));
            var viewModel = new List<AssignedProductData>();
            foreach (var product in allProducts)
            {
                viewModel.Add(new AssignedProductData
                {
                    ProductId = product.Id,
                    Title = product.title,
                    Assigned = cocktailProducts.Contains(product.Id)
                });
            }
            ViewData["Products"] = viewModel;
        }

        // POST: Cocktails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,title,price,color,proof,category")] Cocktail cocktail, string[] selectedProducts)
        {
            if (id != cocktail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var cocktailOld = await _context.Cocktail
                        .Include(o => o.cocktailProducts)
                        .ThenInclude(o => o.product)
                        .FirstOrDefaultAsync(o => o.Id == id);
                    cocktailOld.title = cocktail.title; cocktailOld.price = cocktail.price; cocktailOld.proof = cocktail.proof;
                    cocktailOld.color = cocktail.color; cocktailOld.category = cocktail.category;
                    UpdateCocktailProducts(selectedProducts, cocktailOld);
                    _context.Update(cocktailOld);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CocktailExists(cocktail.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cocktail);
        }

        private void UpdateCocktailProducts(string[] selectedProducts, Cocktail cocktailToUpdate)
        {
            if (selectedProducts == null)
            {
                cocktailToUpdate.cocktailProducts = new List<CocktailProduct>();
                return;
            }

            var selectedProductsHS = new HashSet<string>(selectedProducts);
            var instructorCourses = new HashSet<int>
                (cocktailToUpdate.cocktailProducts.Select(c => c.product.Id));
            foreach (var product in _context.Product)
            {
                if (selectedProductsHS.Contains(product.Id.ToString()))
                {
                    if (!instructorCourses.Contains(product.Id))
                    {
                        cocktailToUpdate.cocktailProducts.Add(new CocktailProduct { cocktailId = cocktailToUpdate.Id, productId = product.Id });
                    }
                }
                else
                {

                    if (instructorCourses.Contains(product.Id))
                    {
                        CocktailProduct productToRemove = cocktailToUpdate.cocktailProducts.FirstOrDefault(i => i.productId == product.Id);
                        _context.Remove(productToRemove);
                    }
                }
            }
        }

        // GET: Cocktails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cocktail = await _context.Cocktail
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cocktail == null)
            {
                return NotFound();
            }

            return View(cocktail);
        }

        // POST: Cocktails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cocktail = await _context.Cocktail.FindAsync(id);
            _context.Cocktail.Remove(cocktail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CocktailExists(int id)
        {
            return _context.Cocktail.Any(e => e.Id == id);
        }
    }
}
