using DataModel.Entities;
using Microsoft.EntityFrameworkCore;

namespace SampleData
{
    public class Sample
    {
        public static void SeedData(DbContext context)
        {
            if (context == null) throw new ArgumentNullException("DbContext cannot be null");

            if (!context.Set<Customer>().Any())
            {
                 context.Set<Customer>().Add(new Customer { Name = "Customer One" });
                 context.Set<Customer>().Add(new Customer { Name = "Customer Two" });
                 context.Set<Customer>().Add(new Customer { Name = "Customer Three" });                
            }


            context.SaveChanges();
        }
    }
}
