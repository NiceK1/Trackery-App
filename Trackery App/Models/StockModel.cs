using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackery_App.Core;

namespace Trackery_App.Models
{
    public class StockModel : ObservableObject
    {
        public string Name { get; set; }
        public string SKU { get; set; }
        public string EAN { get; set; }
        public int Quantity { get; set; }
        public string Location { get; set; }
        public DateTime LastUpdated { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
