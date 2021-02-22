    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieDB.Models.ViewModels;
using MovieDb;
using FileUploadControl;
using Microsoft.AspNetCore.Http;
using MovieDB.Models;
using System.Runtime.Intrinsics.X86;
using MovieDb.Models;

namespace MovieDb.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UploadInterface _upload;

        public AdminController(ApplicationDbContext context, UploadInterface upload)
        {
            _context = context;
            _upload = upload;
        }

        // GET: Admin
        public async Task<IActionResult> Index()
        {
            return View(await _context.MovieDetailViewmodel.ToListAsync());
        }

        // GET: Admin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieDetailViewmodel = await _context.MovieDetailViewmodel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieDetailViewmodel == null)
            {
                return NotFound();
            }

            return View(movieDetailViewmodel);
        }

        // GET: Admin/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        public IActionResult Create(IList<IFormFile> files, MovieDetailViewmodel vmodel, MovieDetails movie)
        {
            movie.Movie_Name = vmodel.Name;
            movie.Movie_description = vmodel.Description;
            movie.DateAndTime = vmodel.DateOfMovie;
            foreach (var item in files)
            {
                movie.MoviePicture = "~/uploads/" + item.FileName.Trim();

            }
            _upload.uploadfilemultiple(files);
            _context.MovieDetails.Add(movie);
            _context.SaveChanges();
            TempData["Success"] = "Movie Saved";

            return RedirectToAction("Create", "Admin");
        }

        [HttpGet]
        public IActionResult CheckBookSeat()
        {
            var getBookingTable = _context.BookingTables.ToList().OrderByDescending(a => a.DateToPresent);
            return View(getBookingTable);
        }

        //[HttpGet]
        //public IActionResult GetUserDetails()
        //{
        //    var getUserTable = _context.Users.ToList();
        //    return View(getUserTable);
        //}


        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Name,Description,DateOfMovie,MoviePicture")] MovieDetailViewmodel movieDetailViewmodel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(movieDetailViewmodel);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(movieDetailViewmodel);
        //}

        // GET: Admin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieDetailViewmodel = await _context.MovieDetailViewmodel.FindAsync(id);
            if (movieDetailViewmodel == null)
            {
                return NotFound();
            }
            return View(movieDetailViewmodel);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,DateOfMovie,MoviePicture")] MovieDetailViewmodel movieDetailViewmodel)
        {
            if (id != movieDetailViewmodel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movieDetailViewmodel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieDetailViewmodelExists(movieDetailViewmodel.Id))
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
            return View(movieDetailViewmodel);
        }

        // GET: Admin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieDetailViewmodel = await _context.MovieDetailViewmodel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieDetailViewmodel == null)
            {
                return NotFound();
            }

            return View(movieDetailViewmodel);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movieDetailViewmodel = await _context.MovieDetailViewmodel.FindAsync(id);
            _context.MovieDetailViewmodel.Remove(movieDetailViewmodel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieDetailViewmodelExists(int id)
        {
            return _context.MovieDetailViewmodel.Any(e => e.Id == id);
        }
    }
}
