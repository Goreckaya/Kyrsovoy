﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Kursovoy.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class HospitalKPEntities3 : DbContext
    {
        public HospitalKPEntities3()
            : base("name=HospitalKPEntities3")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Appointment> Appointment { get; set; }
        public virtual DbSet<Cabinet> Cabinet { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Gender> Gender { get; set; }
        public virtual DbSet<Grajdanstvo> Grajdanstvo { get; set; }
        public virtual DbSet<MedicalOfficer> MedicalOfficer { get; set; }
        public virtual DbSet<Patient> Patient { get; set; }
        public virtual DbSet<RoleID> RoleID { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<StatusPriema> StatusPriema { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Vhod> Vhod { get; set; }
    }
}
