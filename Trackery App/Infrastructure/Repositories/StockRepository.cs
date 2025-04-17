using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackery_App.Core;
using Trackery_App.Models;

namespace Trackery_App.Infrastructure.Repositories
{
    public class StockRepository : RepositoryBase, IStockRepository
    {
        public List<StockModel> GetStock()
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM [Stock]";
                    using (var reader = command.ExecuteReader())
                    {
                        var stock = new List<StockModel>();
                        while (reader.Read())
                        {
                            var item = new StockModel
                            {
                                Name = reader["Name"].ToString(),
                                SKU = reader["SKU"].ToString(),
                                EAN = reader["EAN"].ToString(),
                                Quantity = Convert.ToInt32(reader["Quantity"]),
                                Location = reader["Location"].ToString(),
                                LastUpdated = Convert.ToDateTime(reader["LastUpdated"]),
                                AdditionalInfo = reader["AdditionalInfo"].ToString()
                            };
                            stock.Add(item);
                        }
                        return stock;
                    }
                }
            }
        }
        public void AddStock(DeliveryModel delivery)
        {
            throw new NotImplementedException();
        }

        public void DeleteStock(string id)
        {
            throw new NotImplementedException();
        }

        

        public void UpdateStock(DeliveryModel delivery)
        {
            throw new NotImplementedException();
        }
    }
}
