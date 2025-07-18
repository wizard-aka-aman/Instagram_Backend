
using System.Reflection.Emit;
using Instagram.Model.Tables;
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
        public DbSet<Users> Users { get; set; }
        public DbSet<Posts> Posts { get; set; }
        public DbSet<Likes> Likes { get; set; }
        public DbSet<Comments> Comments{ get; set; }
        public DbSet<Followers> Followers { get; set; }


    }
}
