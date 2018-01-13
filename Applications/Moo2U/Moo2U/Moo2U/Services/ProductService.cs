namespace Moo2U.Services {
    using System;
    using System.Collections.Generic;
    using Moo2U.Model;
    using SQLite;

    public class ProductService : IProductService {

        readonly SQLiteConnection _cn;

        public ProductService(ISQLiteConnectionService sqaLiteConnectionService) {
            if (sqaLiteConnectionService == null) {
                throw new ArgumentNullException(nameof(sqaLiteConnectionService));
            }
            _cn = sqaLiteConnectionService.GetConnection();
        }

        public IList<Product> GetAll() {
            return _cn.Query<Product>("SELECT * FROM [Product]");
        }

        public Int32 Insert(Product product) {
            if (product == null) {
                throw new ArgumentNullException(nameof(product));
            }
            return _cn.Insert(product);
        }

    }
}