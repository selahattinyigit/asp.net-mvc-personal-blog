//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace selahattin.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SelahattinBlogEntities : DbContext
    {
        public SelahattinBlogEntities()
            : base("name=SelahattinBlogEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<aboutMe> aboutMe { get; set; }
        public virtual DbSet<projects> projects { get; set; }
        public virtual DbSet<siteIdentity> siteIdentity { get; set; }
        public virtual DbSet<blog> blog { get; set; }
        public virtual DbSet<adwors> adwors { get; set; }
        public virtual DbSet<aboneler> aboneler { get; set; }
    }
}
