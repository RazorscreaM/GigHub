using GigHub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GigHub.ViewModels
{
    public class GigFormViewModel
    {

        [Required]
        public string Venue { get; set; }

        [Required]
        [FutureDate] // din validation Attribute override facem in ViewModels
        public string Date { get; set; }

        [Required]
        [ValidTime] // din validation Attribute override facem 
        public string Time { get; set; }

        [Required]
        public byte Genre { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

        public DateTime GetDateTime()
        {
            return DateTime.Parse(string.Format("{0} {1}", Date, Time));
        }
    }
}