namespace Moo2U.Services {
    using System;
    using System.Threading.Tasks;

    public interface IDatabase {

        Boolean IsDatabasePopulated();

        Task Seed();

    }
}
