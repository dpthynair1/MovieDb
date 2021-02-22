using System;
namespace MovieDb.Models
{
    public class Cart
    {
        public Cart()
        {
        }
        public int Id { get; set; }
        public string SeatNo { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public int MovieId { get; set; }

    }
}
