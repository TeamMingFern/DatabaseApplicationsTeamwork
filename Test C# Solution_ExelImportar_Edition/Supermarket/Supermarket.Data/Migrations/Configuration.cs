using System.Data.Entity.Migrations;

namespace Supermarket.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SupermarketContext>
    {
        public Configuration()
        {
            //AutomaticMigrationDataLossAllowed = true;
            AutomaticMigrationsEnabled = true;
            ContextKey = "Supermarket.Data.SupermarketContext";
        }

        protected override void Seed(SupermarketContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
