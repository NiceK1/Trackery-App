using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackery_App.Models;

namespace Trackery_App.Core
{
    public interface IDeliveryRepository
    {
        List<DeliveryModel> GetDeliveries();
        void AddDelivery(DeliveryModel delivery);
        void UpdateDelivery(DeliveryModel delivery);
        void DeleteDelivery(string id);
    }
}
