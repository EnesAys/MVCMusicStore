using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCMusicStore.Models
{
    public class OrderDetail : EntityBase
    {

        public int OrderId { get; set; }
        public int AlbumId { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public double Discount { get; set; }

        //Mapping
        public virtual Order Order { get; set; }
        public virtual Album Album { get; set; }
    }
}