using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCMusicStore.Models
{
    public class Order:EntityBase
    {
        public string ShipAddress { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int UserDetailId { get; set; }

        //Mapping
        public virtual List<OrderDetail> OrderDetails { get; set; }
        public virtual UserDetail UserDetail { get; set; }
    }
}