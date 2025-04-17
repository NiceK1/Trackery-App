using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackery_App.Models;

namespace Trackery_App.Core
{
    public interface IStockRepository
    {
        List<StockModel> GetStock();
        void AddStock(DeliveryModel delivery);
        void UpdateStock(DeliveryModel delivery);
        void DeleteStock(string id);
    }
}
