namespace Moo2U.SampleData {
    using System;
    using System.Threading.Tasks;

    public interface IDatabase {

        Boolean IsDatabasePopulated();

        Task Seed();

    }
}
