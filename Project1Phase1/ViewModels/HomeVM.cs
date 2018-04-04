using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project1Phase1.ViewModels
{
    public class HomeVM
    {
        [Required]
        [DisplayName("Home Password")]
        public string homeId { get; set; }
        [Required]
        [DisplayName("Home Name")]
        public string homeName { get; set; }
    }
}
