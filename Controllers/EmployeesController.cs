using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using ManageIO.Models;

namespace ManageIO.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeesContext _context;
        private readonly RolesContext _rcontext;

        public EmployeesController(EmployeesContext context, RolesContext rcontext)
        {
            _context = context;
            _rcontext = rcontext;
        }


        // GET: Employees
        public async Task<IActionResult> Index()
        {
            return View(await _context.employees.ToListAsync());
        }

        public async Task<IActionResult> Create()
        {
            List<string> roles = await _rcontext.roles
                .Select(r => r.RoleName)
                .ToListAsync();
            Console.WriteLine(roles.Count);
            ViewBag.Roles = roles;
            return View();
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = await _context.employees
                .FirstOrDefaultAsync(m => m.EmployeeID == id);
            if (employees == null)
            {
                return NotFound();
            }

            return View(employees);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string? s)
        {
            List<Employees> e = await _context.employees.ToListAsync();
            List<Employees> r = new();

            foreach (var i in e)
            {
                if(s != null)
                {
                    s = s.ToLower();
                    if (i.FullName.ToLower().Contains(s) || i.EmployeeID.ToString().Equals(s) || i.RoleName.ToString().ToLower().Contains(s))
                    {
                        r.Add(i);
                    }
                }
                else
                {
                    return View(e);
                }
            }

            return View(r);
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoleName,FullName,Email,Address,Mobile,Salary")] Employees employees)
        {
            if (ModelState.IsValid)
            {
                if(employees.RoleName.Equals("Select role"))
                {
                    List<string> roles = await _rcontext.roles
                    .Select(r => r.RoleName)
                    .ToListAsync();

                    ViewBag.Roles = roles;

                    ModelState.AddModelError("RoleNotSelected", "You must select a role.");
                    return View(employees);
                }

                if(employees.FullName.Length == 0)
                {
                    ModelState.AddModelError(employees.FullName, "Please enter a name.");
                    return View(employees);
                }

                _context.Add(employees);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employees);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            List<string> roles = await _rcontext.roles
                .Select(r => r.RoleName)
                .ToListAsync();

            ViewBag.Roles = roles;

            if (id == null)
            {
                return NotFound();
            }

            var employees = await _context.employees.FindAsync(id);

            if (employees == null)
            {
                return NotFound();
            }

            return View(employees);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeID,RoleName,FullName,Email,Address,Mobile,Salary")] Employees employees)
        {
            if (id != employees.EmployeeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if(employees.RoleName.Equals("Select role"))
                {
                    List<string> roles = await _rcontext.roles
                    .Select(r => r.RoleName)
                    .ToListAsync();

                    ViewBag.Roles = roles;

                    ModelState.AddModelError("RoleNotSelected", "You must select a role.");
                    return View(employees);
                }
                try
                {
                    _context.Update(employees);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeesExists(employees.EmployeeID))
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
            return View(employees);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = await _context.employees
                .FirstOrDefaultAsync(m => m.EmployeeID == id);
            if (employees == null)
            {
                return NotFound();
            }

            return View(employees);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employees = await _context.employees.FindAsync(id);
            if (employees != null)
            {
                _context.employees.Remove(employees);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeesExists(int id)
        {
            return _context.employees.Any(e => e.EmployeeID == id);
        }
    }
}
