using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MovieDb.Models;
using MovieDb.Models.ViewModels;

namespace MovieDb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        private UserManager<ApplicationUser> _userManager;

        int count = 1;
        bool flag = true;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db,UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _db = db;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var getMovieList = _db.MovieDetails.ToList();
            return View(getMovieList);
        }

        public IActionResult BookNow(int Id)
        {
            BookNowViewModel vm = new BookNowViewModel();
            var item = _db.MovieDetails.Where(a => a.Id == Id).FirstOrDefault();
            vm.Movie_Name = item.Movie_Name;
            vm.Movie_Date = item.DateAndTime;
            vm.MovieId = Id;
            return View(vm);
        }

        public IActionResult BookNow(BookNowViewModel vm)
        {
            
            
            List<BookingTable> bookings = new List<BookingTable>();
            List<Cart> carts = new List<Cart>();
            string seatno = vm.SeatNo.ToString();
            int movieId = vm.MovieId;

            string[] seatnoArray = seatno.Split(',');
            count = seatnoArray.Length;
            if(checkseat(seatno,movieId)==false)
            {
                foreach(var item in seatnoArray)
                {
                    carts.Add(new Cart { Amount = 150, MovieId = vm.MovieId, UserId = _userManager.GetUserId(HttpContext.User), Date = vm.Movie_Date, SeatNo = item });
                }
                foreach(var item in carts)
                {
                    _db.Cart.Add(item);
                    _db.SaveChanges();
                }

                TempData["Success"] = "Seat no Booked, Check Your Cart";
            }
            else
            {
                TempData["Success"] = "Change Your Seat Number";
            }
            return RedirectToAction("BookNow");
        }

        private bool checkseat(string seatno, int movieId)
        {
            string seats = seatno;
            string[] seatreserve = seats.Split(',');

            var seatNoList = _db.BookingTables.Where(a => a.MovieDetailId == movieId).ToList();
            foreach(var item in seatNoList)
            {
                string alreadyBooked = item.SeatNo;
                foreach(var item1 in seatreserve)
                {
                    if(item1== alreadyBooked)
                    {
                        flag = false;
                        break;
                    }
                }
            }

            if (flag == false)
                return true;
            else
                return false;
        }

        public IActionResult About()
        {
            ViewData["message"] = "Your application description page";
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["message"] = "Your contact page";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
