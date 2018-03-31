using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{
    public class Gig
    {
        public int Id { get; set; }

     
        public ApplicationUser Artist { get; set; }

        [Required]
        public string ArtistId { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(256)]
        public string Venue { get; set; }

        public Genre Genre { get; set; } // class cu name si id in database

        [Required]
        public byte GenreId { get; set; } //va prelua idul pentru database

    }
}