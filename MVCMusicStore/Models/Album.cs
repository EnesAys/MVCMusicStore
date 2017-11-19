using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCMusicStore.Models
{
    public class Album : EntityBase
    {
        public Album()
        {
            AlbumArtUrl = "/Content/Images/placeholder.gif";
        }
        [Required(ErrorMessage = "Title cannot be empty"), StringLength(150, ErrorMessage = "Allowed character count is 150 for Title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Price cannot be empty"), Range(0.01, int.MaxValue, ErrorMessage = "Price muste be higher than 0")]
        public decimal Price { get; set; }
        public string AlbumArtUrl { get; set; }
        [Required(ErrorMessage = "Please select a genre")]
        public int GenreId { get; set; }
        [Required(ErrorMessage = "Please select an artist")]
        public int ArtistId { get; set; }

        //Mapping
        public virtual Genre Genre { get; set; }
        public virtual Artist Artist { get; set; }


    }
}