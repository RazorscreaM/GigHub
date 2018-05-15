using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers
{
    public class ParticipanceController : ApiController
    {
        public ApplicationDbContext _context;
        public ParticipanceController()
        {
            _context = new ApplicationDbContext();
        }

        public IHttpActionResult Participate(ParticipanceDto dto)
        {
            var userId = User.Identity.GetUserId();
            if (_context.Participance.Any(p => p.ParticipantId == userId && p.GigId == dto.GigId))
            {
                return BadRequest("Participance already exists");
            }

            var participance = new Participance
            {
                GigId = dto.GigId,
                ParticipantId = userId
            };

            _context.Participance.Add(participance);
            _context.SaveChanges();

            return Ok();

        }
    }
}
