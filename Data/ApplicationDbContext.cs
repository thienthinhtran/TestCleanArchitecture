using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        #region DbSet

        public DbSet<Brand> Brand { get; set; }
        public DbSet<Bill> Bill { get; set; }
        public DbSet<BillCustomer> BillCustomer { get; set; }
        public DbSet<CPU> CPU { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Domain.Entities.Machine> Machine { get; set; }
        public DbSet<Ram> Ram { get; set; }
        public DbSet<SaleInfo> SaleInfo { get; set; }
        public DbSet<SaleMachine> SaleMachine { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserToken> UserToken {  get; set; }
        #endregion

    }
}
