using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackery_App.Core;
using Trackery_App.Models;

namespace Trackery_App.Infrastructure.Repositories
{
    public class DeliveryRepository : RepositoryBase, IDeliveryRepository
    {
        

        public List<DeliveryModel> GetDeliveries()
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM [Delivery]";
                    using (var reader = command.ExecuteReader())
                    {
                        var deliveries = new List<DeliveryModel>();
                        while (reader.Read())
                        {
                            var delivery = new DeliveryModel
                            {
                                Id = reader["Id"].ToString(),
                                DeliveryEstimate = DateTime.Parse(reader["DeliveryEstimate"].ToString()),
                                Sender = reader["Sender"].ToString(),
                                IsSent = bool.Parse(reader["IsSent"].ToString()),
                                IsReceived = bool.Parse(reader["IsReceived"].ToString())
                            };
                            deliveries.Add(delivery);
                        }
                        return deliveries;
                    }
                }
            }
        }
        public void AddDelivery(DeliveryModel delivery)
        {
            throw new NotImplementedException();
        }

        public void DeleteDelivery(string id)
        {
            throw new NotImplementedException();
        }
        public void UpdateDelivery(DeliveryModel delivery)
        {
            throw new NotImplementedException();
        }
    }
}
