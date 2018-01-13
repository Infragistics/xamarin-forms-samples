namespace Moo2U.Services {
    using System;
    using System.Collections.Generic;
    using Moo2U.Model;
    using SQLite;

    public class CustomerService : ICustomerService {

        readonly SQLiteConnection _cn;

        public CustomerService(ISQLiteConnectionService sqLiteConnectionService) {
            if (sqLiteConnectionService == null) {
                throw new ArgumentNullException(nameof(sqLiteConnectionService));
            }
            _cn = sqLiteConnectionService.GetConnection();
        }

        public IList<Customer> GetAll() {
            return _cn.Query<Customer>("SELECT * FROM [Customer]");
        }

        public Int32 Insert(Customer customer) {
            if (customer == null) {
                throw new ArgumentNullException(nameof(customer));
            }
            return _cn.Insert(customer);
        }

    }
}
