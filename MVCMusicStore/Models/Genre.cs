using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCMusicStore.Models
{
    public class Genre : EntityBase
    {
         [Required(ErrorMessage = "Genre name cannot be empty"), StringLength(50, ErrorMessage = "Allowed character count is 50 for Genre Name")]
        public string Name { get; set; }
        public string Description { get; set; }

        //Mapping
        public virtual List<Album> Albums { get; set; }
    }
}