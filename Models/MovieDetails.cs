using System;
using System.Collections.Generic;
using MovieDb.Models;

namespace MovieDb.Models
{
    public class MovieDetails
    {
        public MovieDetails()
        {
        }


        public int Id { get; set; }
        public string Movie_Name { get; set; }
        public string Movie_description { get; set; }
        public DateTime DateAndTime { get; set; }
        public string MoviePicture { get; set; }
        public virtual ICollection<BookingTable> BookingTables { get; set; }
    }
}
