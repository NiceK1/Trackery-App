using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackery_App.Core;

namespace Trackery_App.Models
{
    public class DeliveryModel : ObservableObject
    {
        public string Id { get; set; }
        public DateTime DeliveryEstimate { get; set; }
        public string Sender { get; set; }
        public bool IsSent { get; set; }
        public bool IsReceived { get; set; }
    }
}
