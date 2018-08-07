using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kloud.Models
{
    public class CarComparer : EqualityComparer<Car>
    {
        public override bool Equals(Car x, Car y)
        {
            if (string.Compare(x.Brand, y.Brand) == 0 && string.Compare(x.Colour, y.Colour) == 0)
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode(Car obj)
        {
            return 0;
        }
    }
}
