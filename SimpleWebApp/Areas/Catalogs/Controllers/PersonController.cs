using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SimpleWebApp.Areas.Catalogs.Models;
using SimpleWebApp.Data;

namespace SimpleWebApp.Areas.Catalogs.Controllers
{
    [Area("Catalogs")]
    public class PersonController : Controller
    {
        private readonly SqlSeverDbContext _context;

        public PersonController(SqlSeverDbContext context)
        {
            _context = context;
        }

        // GET: Catalogs/Person
        public async Task<IActionResult> Index()
        {
            var list = await _context.Persons.Where(x => x.IsDeleted == false).ToListAsync();

            return View(list);
        }

        // GET: Catalogs/Person/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personModel = await _context.Persons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personModel == null)
            {
                return NotFound();
            }

            return View(personModel);
        }

        // GET: Catalogs/Person/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Catalogs/Person/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,LastName,Email,Phone,BirthDate,Gender")] PersonModel personModel)
        {
            if (ModelState.IsValid)
            {
                personModel.Id = Guid.NewGuid();
                _context.Add(personModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(personModel);
        }

        // GET: Catalogs/Person/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personModel = await _context.Persons.FindAsync(id);
            if (personModel == null)
            {
                return NotFound();
            }
            return View(personModel);
        }

        // POST: Catalogs/Person/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,LastName,Email,Phone,BirthDate,Gender")] PersonModel personModel)
        {
            if (id != personModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonModelExists(personModel.Id))
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
            return View(personModel);
        }

        // GET: Catalogs/Person/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personModel = await _context.Persons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personModel == null)
            {
                return NotFound();
            }

            return View(personModel);
        }

        // POST: Catalogs/Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var personModel = await _context.Persons.FindAsync(id);
            personModel.IsDeleted = true;
            //_context.Persons.Remove(personModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonModelExists(Guid id)
        {
            return _context.Persons.Any(e => e.Id == id);
        }
    }
}
