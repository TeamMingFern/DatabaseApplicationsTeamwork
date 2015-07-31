namespace Supermarket.Data.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Supermarket.Data.SupermarketContext>
    {
        public Configuration()
        {
            AutomaticMigrationDataLossAllowed = true;
            AutomaticMigrationsEnabled = true;
            ContextKey = "Supermarket.Data.SupermarketContext";
        }

        protected override void Seed(Supermarket.Data.SupermarketContext context)
        {

        }
    }
}
