using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PagedList.Core;
using SecondApp.Data;
using SecondApp.Models;
using static System.Net.Mime.MediaTypeNames;

namespace SecondApp.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel")]
    [Authorize]
    public class EventsController : Controller
    {
        private readonly DataContext _context;

        public EventsController(DataContext context)
        {
            _context = context;
        }

        // GET: ControlPanel/Events
        public IActionResult Index(string q, int page = 1)
        {
            var events = _context.Events.AsQueryable();
            if (!q.IsNullOrEmpty())
            {
                events = events.Where(x => x.Text!.Contains(q) || x.SubText!.Contains(q));
            }
            var pagedEvents = events.ToPagedList(page, 2);

            if (User.IsInRole("Admin"))
            {
                return View("AdminView", pagedEvents);
            }

            return View(pagedEvents);
        }

        // GET: ControlPanel/Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: ControlPanel/Events/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ControlPanel/Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Text,SubText,Date,Imageurl,ImageText,ImageSubText")] Event @event)
        {
            if (ModelState.IsValid)
            {
                var file = Request.Form.Files[0];
                string ext = Path.GetExtension(file.FileName);
                string name = Guid.NewGuid().ToString() + ext;
                var path = Path.Combine("wwwroot", "Uploads", name);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                if (@event.SubText == @event.Text)
                {
                    ModelState.AddModelError("SubText", "لايمكن أن ستاوي النص الاساسي مع النص الفرعي");
                }
                @event.Imageurl = name;
                _context.Add(@event);
                await _context.SaveChangesAsync();
                TempData["Message"] = "تمت عميلة اللللللللل";
                return RedirectToAction(nameof(Create));
            }
            return View(@event);
        }

        // GET: ControlPanel/Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            return View(@event);
        }

        // POST: ControlPanel/Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Text,SubText,Date,Imageurl,ImageText,ImageSubText")] Event @event)
        {
            if (id != @event.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.Id))
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
            return View(@event);
        }

        // GET: ControlPanel/Events/Delete/5
        public async Task<IActionResult> Delete(int? id, int employeeId)
        {
            if (id == null)
            {
                var empId = HttpContext.Request.Query["EmployeeId"];
                return NotFound();
            }

            var @event = await _context.Events
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: ControlPanel/Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            if (@event != null)
            {
                _context.Events.Remove(@event);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }

        public ActionResult SetCookie(string email)
        {
            Response.Cookies.Append("Email", email);
            return RedirectToAction(nameof(Index));
        }
    }
}
