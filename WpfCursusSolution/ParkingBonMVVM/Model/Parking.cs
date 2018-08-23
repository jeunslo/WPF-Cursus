using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingBonMVVM.Model
{
    public class Parking
    {
        public Parking()
        {
            AankomstTijd = DateTime.Now.ToLongTimeString();
            Datum = DateTime.Now;
            VertrekTijd = DateTime.Now.ToLongTimeString();
            Bedrag = 0m;
        }

        public string AankomstTijd { get; set; }
        public string VertrekTijd { get; set; }
        public DateTime Datum { get; set; }

        public decimal Bedrag { get; set; }
    }
}
