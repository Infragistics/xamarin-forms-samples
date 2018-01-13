namespace Moo2U.Services {
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Moo2U.Model;

    public interface IDeliveryStopService {

        Int32 Insert(DeliveryStop deliveryStop);

        Task<List<DeliveryListItem>> GetDeliveryListItemsForDateAsync(DateTime dateDelivery);

    }
}