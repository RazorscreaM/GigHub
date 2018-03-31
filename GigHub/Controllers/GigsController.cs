using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {

        private readonly ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();

            var veiwModel = new GigsViewModel()
            {
                UpComingGigs = gigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gig's I'm Attending"
            };

            return View("Gigs",veiwModel);
        }



        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Genres = _context.Genres.ToList()
            };

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            //read about MVC reflection

            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("Create", viewModel);
            }


            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(), //va lua id'ul la userul care ii logat la moment
                DateTime = viewModel.GetDateTime(), //parsing la date time
                GenreId = viewModel.Genre, //ia genre id la genul care ii vine din create method
                Venue = viewModel.Venue //same as above
            };
            _context.Gigs.Add(gig);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }


    }
}