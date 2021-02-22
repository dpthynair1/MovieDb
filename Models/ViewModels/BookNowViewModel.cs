using System;
namespace MovieDb.Models.ViewModels
{
    public class BookNowViewModel
    {
        public string Movie_Name { get; set; }
        public DateTime Movie_Date { get; set; }
        public string SeatNo { get; set; }
        public int Amount { get; set; }
        public int MovieId { get; set; }
    }
}
