using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elastic.Object
{
    public class GeocodingDetails
    {
        public List<addressComponents> address_components { get; set; }
        public string formatted_address { get; set; }
        public string geometry { get; set; }
        public string place_id { get; set; }
        public string[] types { get; set; }

        public class addressComponents
        {
            public string long_name { get; set; }
            public string short_name { get; set; }
            public string[] types { get; set; }
        }
    }

    
}
