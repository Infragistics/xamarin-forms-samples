namespace Moo2U.Services {
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Moo2U.Model;

    public interface IDeliveryHistoryService {

        IList<DeliveryHistory> GetAll();

        Int32 Insert(DeliveryHistory deliveryHistory);

        Task<PerformanceAggregate> GetPerformanceAggregateAsync(Period period);

    }
}