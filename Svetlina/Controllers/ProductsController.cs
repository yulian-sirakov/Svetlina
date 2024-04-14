using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Svetlina.Data.Common;
using Svetlina.Data.Models;
using Svetlina.Services;

namespace Svetlina.Controllers
{

    public class ProductsController : Controller
    {
        private readonly ProductContext productContext;
        private readonly CustomerContext CustomerContext;

        public ProductsController(ProductContext productContext, CustomerContext customerContext)
        {
            this.productContext = productContext;
            this.CustomerContext = customerContext;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await productContext.ReadAllAsync(true));
        }

        // GET: Products/Details/5

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await productContext.ReadAsync((int)id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        [Authorize(Roles = "Administrator")]

        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="Administrator")]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,Price,ProductImage")] Product product)
        {
            if (ModelState.IsValid)
            {
                await productContext.CreateAsync(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "Administrator")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await productContext.ReadAsync((int)id,true);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]


        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,Price,ProductImage")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   
                    await productContext.UpdateAsync(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ProductExists(product.ProductId))
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
            return View(product);
        }

        // GET: Products/Delete/5
        [Authorize(Roles = "Administrator")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await productContext.ReadAsync((int)id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            

            await productContext.DeleteAsync((int)id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Addtocart(int id)
        {
            Customer user = await CustomerContext.ReadAsync(User.FindFirstValue(ClaimTypes.NameIdentifier), true);
            var product = await productContext.ReadAsync(id,true);
            user.cart.Products.Add(product);
            await CustomerContext.UpdateAsync(user,true);
            return RedirectToAction(nameof(Index));
            

        }

        private async Task<bool> ProductExists(int id)
        {
            return await productContext.ReadAsync(id) != null;
        }
    }
}
