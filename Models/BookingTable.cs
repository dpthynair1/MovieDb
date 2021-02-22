using System;
using System.ComponentModel.DataAnnotations.Schema;
using MovieDb.Models;

namespace MovieDb.Models
{
    public class BookingTable
    {
        public BookingTable()
        {
        }

        public int Id { get; set; }
        public string SeatNo { get; set; }
        public string UserId { get; set; }
        public DateTime DateToPresent { get; set; }
        public int MovieDetailId { get; set; }
        public int Amount { get; set; }


        [ForeignKey("MovieDetailsId")]
        public virtual MovieDetails MovieDetails { get; set; }

    }
}
