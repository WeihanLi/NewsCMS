using System.Data.Entity;

namespace Models
{
    public class CMSEntity: DbContext
    {
            public CMSEntity(): base("name=CMSEntity")
            {
            }

            public virtual DbSet<News> News { get; set; }
            public virtual DbSet<Category> Category { get; set; }
            public virtual DbSet<User> User { get; set; }
    }
}
