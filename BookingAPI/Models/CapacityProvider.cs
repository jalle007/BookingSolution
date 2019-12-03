using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace BookingApi.Models
{
    public class CapacityProvider
  {
        public CapacityProvider()
        {
            //this.Bookings = new HashSet<Booking>();
        }

        [Key]
        public int id { get; set; }
        
        [Required]
        public string name { get; set; }
        
        public string city { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string contract_number { get; set; }

        //[ForeignKey("CapacityProviderId")]
        //public Booking Booking { get; set; }
        //public virtual ICollection<Booking> Bookings { get; set; }

        //[JsonIgnore]
        //public virtual List<Booking> Bookings { get; set; } = new List<Booking>();

    }
}