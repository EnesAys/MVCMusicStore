using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCMusicStore.Models
{
    public class EntityBase
    {
        public EntityBase()
        {
            CreatedDate = DateTime.Now;
            IsDeleted = false;
        }
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}