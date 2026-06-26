using Microsoft.EntityFrameworkCore;

namespace Infrastructure.SeedData
{
    public static class SeedExtensions
    {
        public static void ApplySeedData(this ModelBuilder modelBuilder)
        {
            SeedData.ApplySeedData.Seed(modelBuilder);
        }
    }
}
