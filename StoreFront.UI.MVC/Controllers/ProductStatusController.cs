﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreFront.DATA.EF.Models;

namespace StoreFront.UI.MVC.Controllers
{
    public class ProductStatusController : Controller
    {
        private readonly StoreFrontContext _context;

        public ProductStatusController(StoreFrontContext context)
        {
            _context = context;
        }

        // GET: ProductStatus
        public async Task<IActionResult> Index()
        {
            var storeFrontContext = _context.ProductStatuses.Include(p => p.Product);
            return View(await storeFrontContext.ToListAsync());
        }

        // GET: ProductStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductStatuses == null)
            {
                return NotFound();
            }

            var productStatus = await _context.ProductStatuses
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.ProductStatusId == id);
            if (productStatus == null)
            {
                return NotFound();
            }

            return View(productStatus);
        }

        // GET: ProductStatus/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId");
            return View();
        }

        // POST: ProductStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductStatusId,ProductId,InStock,OnBackOrder,Discontinued")] ProductStatus productStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", productStatus.ProductId);
            return View(productStatus);
        }

        // GET: ProductStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductStatuses == null)
            {
                return NotFound();
            }

            var productStatus = await _context.ProductStatuses.FindAsync(id);
            if (productStatus == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", productStatus.ProductId);
            return View(productStatus);
        }

        // POST: ProductStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductStatusId,ProductId,InStock,OnBackOrder,Discontinued")] ProductStatus productStatus)
        {
            if (id != productStatus.ProductStatusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductStatusExists(productStatus.ProductStatusId))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", productStatus.ProductId);
            return View(productStatus);
        }

        // GET: ProductStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductStatuses == null)
            {
                return NotFound();
            }

            var productStatus = await _context.ProductStatuses
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.ProductStatusId == id);
            if (productStatus == null)
            {
                return NotFound();
            }

            return View(productStatus);
        }

        // POST: ProductStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductStatuses == null)
            {
                return Problem("Entity set 'StoreFrontContext.ProductStatuses'  is null.");
            }
            var productStatus = await _context.ProductStatuses.FindAsync(id);
            if (productStatus != null)
            {
                _context.ProductStatuses.Remove(productStatus);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductStatusExists(int id)
        {
          return _context.ProductStatuses.Any(e => e.ProductStatusId == id);
        }
    }
}
