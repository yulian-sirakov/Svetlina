using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Svetlina.Data.Common;
using Svetlina.Data.Models;
using Svetlina.Services;


namespace Svetlina.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly ProjectContext projectContext;
        private readonly CustomerContext customerContext;
        private readonly ScheduleContext scheduleContext;
        private readonly IdentityContext identityContext;

        public ProjectsController(ProjectContext projectContext,CustomerContext customerContext, ScheduleContext scheduleContext,IdentityContext identityContext)
        {
            this.projectContext = projectContext;
            this.customerContext = customerContext;
            this.scheduleContext = scheduleContext;
            this.identityContext = identityContext;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
           
            return View(await projectContext.ReadAllAsync());
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await projectContext.ReadAsync((int)id, true);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Projects/Create
        public async Task< IActionResult> Create()
        {
            await LoadNavigationalEntities();
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection formCollection)
        {
            await LoadNavigationalEntities();

            Customer user = await identityContext.ReadAsync(User.FindFirstValue(ClaimTypes.NameIdentifier), true);
            


                Project project = new Project(new Schedule(), formCollection["ProjectName"], user, formCollection["Description"], DateTime.Parse(formCollection["StartDate"]), DateTime.Parse(formCollection["EndDate"]));

           

            if (ModelState.IsValid)
            {
                await projectContext.CreateAsync(project);
                return RedirectToAction(nameof(Index));
            }

            return View(project);
        }
        

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await projectContext.ReadAsync((int)id, true);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormCollection formCollection)
        {
            await LoadNavigationalEntities();

            Customer user = await identityContext.ReadAsync(User.FindFirstValue(ClaimTypes.NameIdentifier), true);



            Project project = new Project(new Schedule(), formCollection["ProjectName"], user, formCollection["Description"], DateTime.Parse(formCollection["StartDate"]), DateTime.Parse(formCollection["EndDate"]));

            project.ProjectId = id;

            if (ModelState.IsValid)
            {
                await projectContext.CreateAsync(project);
                return RedirectToAction(nameof(Index));
            }

            return View(project);
        }


        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await projectContext.ReadAsync((int)id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           

            await projectContext.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ProjectExists(int id)
        {
            return await projectContext.ReadAsync(id)!=null;
        }
        private async Task LoadNavigationalEntities()
        {
            ViewData["Customers"] = new SelectList(await customerContext.ReadAllAsync(), "CustomerId", "CustomerName");
            ViewData["Schedules"] = new SelectList(await scheduleContext.ReadAllAsync(), "ScheduleId", "ScheduleName");
        }

    }
}
