using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IB130149_Flashcard_Service.Models
{
    public partial class DBContext: DbContext
    {
        public DBContext() : base("MojConnectionString")
        {
            Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Korisnici> Korisnici { get; set; }
        public virtual DbSet<Profili> Profili { get; set; }
        public virtual DbSet<Kategorije> Kategorije { get; set; }
        public virtual DbSet<Dekovi> Dekovi { get; set; }
        public virtual DbSet<Pitanja> Pitanja { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
        }
    }

}