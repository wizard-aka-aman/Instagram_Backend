
using Microsoft.EntityFrameworkCore;

namespace Instagram.Model
{
    public class InstagramContext :DbContext
    {


        public InstagramContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
         

    }
}
