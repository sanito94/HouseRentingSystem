using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRentingSystem.Core.Models.House
{
    public class HouseQueryServiceModel
    {
        public HouseQueryServiceModel()
        {
            Houses = new List<HouseServiceModel>();
        }

        public int TotalHousesCount { get; set; }

        public IEnumerable<HouseServiceModel> Houses { get; set; }
    }
}
