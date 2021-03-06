﻿using System;
using System.Collections.Generic;
using System.Drawing;
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
        // showCoctails()
        // open()
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
            PopulateEmpty();
            return View();
        }

        private void PopulateEmpty()
        {
            var allProducts = _context.Product;
            var viewModel = new List<AssignedProductData>();
            foreach (var product in allProducts)
            {
                viewModel.Add(new AssignedProductData
                {
                    ProductId = product.Id,
                    Title = product.title,
                    Assigned = false
                });
            }
            ViewData["Products"] = viewModel;
        }

        // POST: Cocktails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,title,price,color,proof,category")] Cocktail cocktail, string[] selectedProducts, string create, string preview)
        {
            if (create != null)
            {
                if (ModelState.IsValid)
                {
                    CreateCocktailProducts(selectedProducts, cocktail);
                    _context.Add(cocktail);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            if(preview != null)
            {
                cocktail.color = ColorTranslator.ToHtml(CalculateColorFromProducts(selectedProducts));
                return View(cocktail);
            }
            return View(cocktail);
        }

        private void CreateCocktailProducts(string[] selectedProducts, Cocktail cocktailToUpdate)
        {
            cocktailToUpdate.cocktailProducts = new List<CocktailProduct>();
            if (selectedProducts == null)
            {
                return;
            }

            var selectedProductsHS = new HashSet<string>(selectedProducts);
            foreach (var product in _context.Product)
            {
                if (selectedProductsHS.Contains(product.Id.ToString()))
                {
                    cocktailToUpdate.cocktailProducts.Add(new CocktailProduct { cocktailId = cocktailToUpdate.Id, productId = product.Id });
                }
            }
        }

        private Color CalculateColorFromProducts(string[] selectedProducts)
        {
            if (selectedProducts == null)
            {
                return Color.Transparent;
            }

            string colors = "";
            var selectedProductsHS = new HashSet<string>(selectedProducts);
            foreach (var product in _context.Product)
            {
                if (selectedProductsHS.Contains(product.Id.ToString()))
                {
                    colors += !string.IsNullOrEmpty(product.color) ? product.color : ColorTranslator.ToHtml(Color.Transparent);
                }
            }

            string[] colorArray = colors.Split('#',StringSplitOptions.RemoveEmptyEntries);
            if (colorArray.Length > 1)
            {
                Color current = ColorTranslator.FromHtml("#"+colorArray[0]);
                for (int i = 1; i < colorArray.Length; i++)
                {
                    current = Blend(current, ColorTranslator.FromHtml("#" + colorArray[i]), 0.5);
                }
                return current;
            }
            else if(colorArray.Length == 1)
            {
                return ColorTranslator.FromHtml("#"+colorArray[0]);
            }
            else
            {
                return Color.Transparent;
            }
        }

        private Color Blend(Color color, Color backColor, double amount)
        {
            byte r = (byte)((color.R * amount) + backColor.R * (1 - amount));
            byte g = (byte)((color.G * amount) + backColor.G * (1 - amount));
            byte b = (byte)((color.B * amount) + backColor.B * (1 - amount));
            return Color.FromArgb(r, g, b);
        }

        public async Task<IActionResult> Order(int? id)
        {
            if (ModelState.IsValid)
            {
                var cocktail = await _context.Cocktail.FindAsync(id);
                var cartItem = new CartItem()
                {
                    cartId = 1,
                    cocktailId = (int)id,
                    count = 1,
                    price = cocktail.price,
                };
                _context.Add(cartItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Cocktails/Edit/5
        // editCocktail()
        // open()
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
        // getProducts()
        private void Populate(Cocktail cocktail)
        {
            var allProducts = _context.Product;
            var cocktailProducts = new HashSet<int>(cocktail.cocktailProducts.Select(c => c.productId));
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
        // submitForm()
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,title,price,color,proof,category")] Cocktail cocktail, string[] selectedProducts)
        {
            if (id != cocktail.Id)
            {
                return NotFound();
            }
            // validate
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
            var cocktailProducts = new HashSet<int>
                (cocktailToUpdate.cocktailProducts.Select(c => c.product.Id));
            foreach (var product in _context.Product)
            {
                if (selectedProductsHS.Contains(product.Id.ToString()))
                {
                    if (!cocktailProducts.Contains(product.Id))
                    {
                        cocktailToUpdate.cocktailProducts.Add(new CocktailProduct { cocktailId = cocktailToUpdate.Id, productId = product.Id });
                    }
                }
                else
                {

                    if (cocktailProducts.Contains(product.Id))
                    {
                        CocktailProduct productToRemove = cocktailToUpdate.cocktailProducts.FirstOrDefault(i => i.productId == product.Id);
                        _context.Remove(productToRemove);
                    }
                }
            }
        }

        // GET: Cocktails/Delete/5
        // removeCocktail()
        public async Task<IActionResult> Delete(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var cocktail = await _context.Cocktail
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (cocktail == null)
            //{
            //    return NotFound();
            //}

            //return View(cocktail);

            if (id == null)
            {
                return NotFound();
            }

            var cocktail = await _context.Cocktail.FindAsync(id);
            _context.Cocktail.Remove(cocktail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
