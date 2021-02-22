using System;
namespace MovieDB.Models.ViewModels
{
    public class MovieDetailViewmodel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateOfMovie { get; set; }
        public string MoviePicture { get; set; }
    }
}
