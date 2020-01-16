namespace Keyteki.Data
{
    using CrimsonDev.Gameteki.Data;
    using Microsoft.EntityFrameworkCore;

    public class KeytekiDbContext : GametekiDbContext, IKeytekiDbContext
    {
        public KeytekiDbContext(DbContextOptions<KeytekiDbContext> options)
            : base(options)
        {
        }
    }
}