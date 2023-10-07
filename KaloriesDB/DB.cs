
using System;
using System.Data.Entity;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KaloriesDB
{
    public class DB:DbContext
    {
        public DB():base("Data source =.\\SQLEXPRESS; Initial Catalog = FoodDB; Integrated Security = true")//"name=DbConnection") 
        {
            //Database.Connection.ConnectionString = "Data source =.\\SQLEXPRESS; Initial Catalog = FoodDB; Integrated Security = true;";
        }
        
        public DbSet<Food> Foods { get; set; }
    }
}
