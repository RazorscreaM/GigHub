using GigHub.Models;
using GigHub.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }
        [Authorize]
        public ActionResult Index()
        {
            var upcomingGigs = _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now)
                .OrderByDescending(g => g.ArtistId);
  

            var viewModel = new GigsViewModel
            {
                UpComingGigs = upcomingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gigs"
            };

            return View("Gigs",viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize]
        public ActionResult Search(string searchTerm = null)
        {
            var gigsFromDb = _context.Gigs
                .OrderByDescending(d => d.DateTime)
                .Where(r => searchTerm == null || r.Artist.Name.StartsWith(searchTerm))
                .Take(10)
                .Skip(0)
                .Select(r => new GigsSearch
                {
                    DateTime = r.DateTime,
                    Genre = r.Genre,
                    ArtistName = r.Artist.Name
                });
            if (Request.IsAjaxRequest())
            {
                return PartialView("_gigsSearchView", gigsFromDb);
            }

            return (View(gigsFromDb));



        }
    }
}