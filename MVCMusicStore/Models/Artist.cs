using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCMusicStore.Models
{
    public class Artist : EntityBase
    {
        [Required(ErrorMessage = "Artist name cannot be empty"), StringLength(150, ErrorMessage = "Allowed character count is 150 for Artist Name")]
        public string Name { get; set; }
        public int? Rating { get; set; }

        //Mapping
        public virtual List<Album> Albums { get; set; }
    }
}