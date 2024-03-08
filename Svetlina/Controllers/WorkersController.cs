using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Svetlina.Data.Common;
using Svetlina.Data.Models;
using Svetlina.Services;

namespace Svetlina.Controllers
{
    public class WorkersController : Controller
    {
        private readonly WorkerContext workerContext;
        private readonly ProjectContext projectContext;

        public WorkersController(WorkerContext workerContext,ProjectContext projectContext)
        {
           this.workerContext = workerContext;  
            this.projectContext = projectContext;
        }

        //GET: Workers
        public async Task<IActionResult> Index()
        {

            return View(await workerContext.ReadAllAsync());
        }

        // GET: Workers/Details/5
        public async Task<IActionResult> Details(int? id)
        {   

            if (id == null)
            {
                return NotFound();
            }

            var worker = await workerContext.ReadAsync((int)id, true, true);

            if (worker == null)
            {
                return NotFound();
            }
            await LoadNav();
            return View(worker);
        }

        // GET: Workers/Create
        public async Task<IActionResult> Create()
        {
            await LoadNav();

            return View();
        }

        // POST: Workers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorkerId,WorkerName,PhoneNumber,ProjectId,SpecialisationType")] Worker worker)
        {
            if (ModelState.IsValid)
            {
                
                await workerContext.CreateAsync(worker);
                return RedirectToAction(nameof(Index));
            }
            return View(worker);
        }

        // GET: Workers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var worker = await workerContext.ReadAsync((int)id, true, false);
            if (worker == null)
            {
                return NotFound();
            }
            await LoadNav();

            return View(worker);
        }

        // POST: Workers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WorkerId,WorkerName,PhoneNumber,ProjectId,SpecialisationType")] Worker worker)
        {
            if (id != worker.WorkerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await WorkerExists(worker.WorkerId))
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
            return View(worker);
        }

        // GET: Workers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var worker = await workerContext.ReadAsync((int)id);
            if (worker == null)
            {
                return NotFound();
            }

            return View(worker);
        }

        // POST: Workers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            await workerContext.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> WorkerExists(int id)
        {
            return await workerContext.ReadAsync(id) != null; 
        }

        private async Task LoadNav()
        {
            ViewData["Projects"] = new SelectList(await projectContext.ReadAllAsync(), "ProjectId", "ProjectName");
            ViewData["SpecType"] = new SelectList(Enum.GetValues(typeof(SpecialisationType)).Cast<SpecialisationType>().ToList());
        }
    }
}
