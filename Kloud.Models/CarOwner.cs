using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kloud.Models
{
    public class CarOwner
    {
        public string Name { get; set; }
        public List<Car> Cars { get; set; } = new List<Car>();
    }
}
