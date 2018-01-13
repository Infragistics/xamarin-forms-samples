namespace Moo2U.Services {
    using System;
    using System.Collections.Generic;
    using Moo2U.Model;

    public interface ICustomerService {

        IList<Customer> GetAll();

        Int32 Insert(Customer customer);

    }
}
