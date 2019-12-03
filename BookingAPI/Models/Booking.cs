using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingApi.Models
{
    public class Booking
    {
        [Key]
        public int id { get; set; }

        public string object_name { get; set; }

        [Required]
        public int CapacityProviderId { get; set; }

        //[JsonIgnore]
        //[NotMapped]
        public virtual CapacityProvider CapacityProvider { get; set; }

        [Required]
        public DateTime date_from { get; set; }

        [Required]
        public DateTime date_to { get; set; }

        [Required]
        public bool is_active { get; set; }

        [Required]
        public float amount { get; set; }

        [Required]
        public string currency { get; set; }
        
        [Required]
        public string comment { get; set; }
    }
}