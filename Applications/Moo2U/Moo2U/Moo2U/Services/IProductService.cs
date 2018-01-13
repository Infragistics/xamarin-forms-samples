namespace Moo2U.Services {
    using System;
    using System.Collections.Generic;
    using Moo2U.Model;

    public interface IProductService {

        IList<Product> GetAll();

        Int32 Insert(Product product);

    }
}