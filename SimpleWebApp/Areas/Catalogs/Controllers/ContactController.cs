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
    public class ContactController : Controller
    {
        private readonly SqlSeverDbContext _context;

        public ContactController(SqlSeverDbContext context)
        {
            _context = context;
        }

        // GET: Catalogs/Contact
        public async Task<IActionResult> Index()
        {
            var list = await _context.Contacts.Where(x => x.IsDeleted == false).ToListAsync();
            return View(list);
        }

        // GET: Catalogs/Contact/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactModel = await _context.Contacts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactModel == null)
            {
                return NotFound();
            }

            return View(contactModel);
        }

        // GET: Catalogs/Contact/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Catalogs/Contact/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,NickName,Email,Phone,IsDeleted,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt")] ContactModel contactModel)
        {
            if (ModelState.IsValid)
            {
                contactModel.Id = Guid.NewGuid();
                _context.Add(contactModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contactModel);
        }

        // GET: Catalogs/Contact/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactModel = await _context.Contacts.FindAsync(id);
            if (contactModel == null)
            {
                return NotFound();
            }
            return View(contactModel);
        }

        // POST: Catalogs/Contact/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,NickName,Email,Phone,IsDeleted,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt")] ContactModel contactModel)
        {
            if (id != contactModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactModelExists(contactModel.Id))
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
            return View(contactModel);
        }

        // GET: Catalogs/Contact/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactModel = await _context.Contacts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactModel == null)
            {
                return NotFound();
            }

            return View(contactModel);
        }

        // POST: Catalogs/Contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var contactModel = await _context.Contacts.FindAsync(id);
            contactModel.IsDeleted = true;
            //_context.Contacts.Remove(contactModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactModelExists(Guid id)
        {
            return _context.Contacts.Any(e => e.Id == id);
        }
    }
}
