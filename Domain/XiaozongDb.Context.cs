﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Domain
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<SYS_USER> SYS_USER { get; set; }
        public virtual DbSet<SYS_MODULE> SYS_MODULE { get; set; }
        public virtual DbSet<SYS_PERMISSION> SYS_PERMISSION { get; set; }
        public virtual DbSet<SYS_ROLE> SYS_ROLE { get; set; }
        public virtual DbSet<SYS_ROLE_PERMISSION> SYS_ROLE_PERMISSION { get; set; }
        public virtual DbSet<SYS_USER_ROLE> SYS_USER_ROLE { get; set; }
    }
}