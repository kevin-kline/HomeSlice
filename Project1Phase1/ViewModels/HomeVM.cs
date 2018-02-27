using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Project1Phase1.ViewModels
{
    public class HomeVM
    {
        [DisplayName("Home ID")]
        public Guid Id { get; set; }
        [DisplayName("Home Name")]
        public string Name { get; set; }
    }
}
