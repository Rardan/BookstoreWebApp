using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookstoreWebApp.Data;
using BookstoreWebApp.Helpers;
using BookstoreWebApp.Models;
using BookstoreWebApp.Services;
using Microsoft.AspNetCore.Authorization;

namespace BookstoreWebApp.Controllers
{
    [Authorize(Roles = "Employee, Admin")]
    public class AuthorsController : Controller
    {
        private readonly IAuthorService _authorService;
        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        // GET: Authors
        public async Task<IActionResult> Index(string currentFilter, string? searchString, int? pageNumber)
        {
            if (searchString != null)
            {
                pageNumber = 1;
                return View(await PaginatedList<Author>.CreateAsync(_authorService.GetFiltered(searchString),
                    pageNumber ?? 1, 20));
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;
            int pageSize = 20;
            return View(await PaginatedList<Author>.CreateAsync(_authorService.GetAll(), pageNumber ?? 1, pageSize));
        }

        // GET: Authors/Details/5
        public IActionResult Details(int id)
        {

            var author =  _authorService.Get(id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Author author)
        {
            if (ModelState.IsValid)
            {
                await _authorService.Add(author);
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Authors/Edit/5
        public IActionResult Edit(int id)
        {
            var author = _authorService.Get(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Author author)
        {
            if (id != author.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _authorService.Update(author);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_authorService.Exists(author.Id))
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
            return View(author);
        }

        // GET: Authors/Delete/5
        public IActionResult Delete(int id)
        {
            var author = _authorService.Get(id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var author = _authorService.Get(id);
            await _authorService.Delete(author.Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
