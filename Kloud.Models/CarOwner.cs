using System.Collections.Generic;

namespace Kloud.Models
{
    public class CarOwner
    {
        public string Name { get; set; }
        public List<Car> Cars { get; set; } = new List<Car>();
    }
}
