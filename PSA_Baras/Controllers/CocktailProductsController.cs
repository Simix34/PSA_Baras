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
    public class CocktailProductsController : Controller
    {
        private readonly BarasDBContext _context;

        public CocktailProductsController(BarasDBContext context)
        {
            _context = context;
        }

        // GET: CocktailProducts
        public async Task<IActionResult> Index()
        {
            var barasDBContext = _context.CocktailProduct.Include(c => c.cocktail).Include(c => c.product);
            return View(await barasDBContext.ToListAsync());
        }

        // GET: CocktailProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cocktailProduct = await _context.CocktailProduct
                .Include(c => c.cocktail)
                .Include(c => c.product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cocktailProduct == null)
            {
                return NotFound();
            }

            return View(cocktailProduct);
        }

        // GET: CocktailProducts/Create
        public IActionResult Create()
        {
            ViewData["cocktailId"] = new SelectList(_context.Cocktail, "Id", "Id");
            ViewData["productId"] = new SelectList(_context.Product, "Id", "Id");
            return View();
        }

        // POST: CocktailProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,unit,quantity,cocktailId,productId")] CocktailProduct cocktailProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cocktailProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["cocktailId"] = new SelectList(_context.Cocktail, "Id", "Id", cocktailProduct.cocktailId);
            ViewData["productId"] = new SelectList(_context.Product, "Id", "Id", cocktailProduct.productId);
            return View(cocktailProduct);
        }

        // GET: CocktailProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cocktailProduct = await _context.CocktailProduct.FindAsync(id);
            if (cocktailProduct == null)
            {
                return NotFound();
            }
            ViewData["cocktailId"] = new SelectList(_context.Cocktail, "Id", "Id", cocktailProduct.cocktailId);
            ViewData["productId"] = new SelectList(_context.Product, "Id", "Id", cocktailProduct.productId);
            return View(cocktailProduct);
        }

        // POST: CocktailProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,unit,quantity,cocktailId,productId")] CocktailProduct cocktailProduct)
        {
            if (id != cocktailProduct.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cocktailProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CocktailProductExists(cocktailProduct.Id))
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
            ViewData["cocktailId"] = new SelectList(_context.Cocktail, "Id", "Id", cocktailProduct.cocktailId);
            ViewData["productId"] = new SelectList(_context.Product, "Id", "Id", cocktailProduct.productId);
            return View(cocktailProduct);
        }

        // GET: CocktailProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cocktailProduct = await _context.CocktailProduct
                .Include(c => c.cocktail)
                .Include(c => c.product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cocktailProduct == null)
            {
                return NotFound();
            }

            return View(cocktailProduct);
        }

        // POST: CocktailProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cocktailProduct = await _context.CocktailProduct.FindAsync(id);
            _context.CocktailProduct.Remove(cocktailProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CocktailProductExists(int id)
        {
            return _context.CocktailProduct.Any(e => e.Id == id);
        }
    }
}
