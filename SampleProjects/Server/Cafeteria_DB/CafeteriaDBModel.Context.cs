﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cafeteria_DB
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class cafeteriaDBEntities : DbContext
    {
        public cafeteriaDBEntities()
            : base("name=cafeteriaDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ADMIN> ADMINs { get; set; }
        public virtual DbSet<CAFETERIA_TABLE> CAFETERIA_TABLE { get; set; }
        public virtual DbSet<CANVA> CANVAs { get; set; }
        public virtual DbSet<CANVA_ADMIN> CANVA_ADMIN { get; set; }
        public virtual DbSet<FOOD_TABLE> FOOD_TABLE { get; set; }
        public virtual DbSet<FOOD_TYPE> FOOD_TYPE { get; set; }
        public virtual DbSet<SHAPE_TYPE> SHAPE_TYPE { get; set; }
    
        public virtual int DeleteCanva(string iD_CANVA)
        {
            var iD_CANVAParameter = iD_CANVA != null ?
                new ObjectParameter("ID_CANVA", iD_CANVA) :
                new ObjectParameter("ID_CANVA", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DeleteCanva", iD_CANVAParameter);
        }
    
        public virtual ObjectResult<GetCanva_Result> GetCanva()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetCanva_Result>("GetCanva");
        }
    
        public virtual ObjectResult<string> InsertCanva(Nullable<double> wIDTH, Nullable<double> hEIGHT)
        {
            var wIDTHParameter = wIDTH.HasValue ?
                new ObjectParameter("WIDTH", wIDTH) :
                new ObjectParameter("WIDTH", typeof(double));
    
            var hEIGHTParameter = hEIGHT.HasValue ?
                new ObjectParameter("HEIGHT", hEIGHT) :
                new ObjectParameter("HEIGHT", typeof(double));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("InsertCanva", wIDTHParameter, hEIGHTParameter);
        }
    
        public virtual ObjectResult<SelectAllCanva_Result> SelectAllCanva()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SelectAllCanva_Result>("SelectAllCanva");
        }
    
        public virtual ObjectResult<SelectIdCanva_Result> SelectIdCanva(string iD_CANVA)
        {
            var iD_CANVAParameter = iD_CANVA != null ?
                new ObjectParameter("ID_CANVA", iD_CANVA) :
                new ObjectParameter("ID_CANVA", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SelectIdCanva_Result>("SelectIdCanva", iD_CANVAParameter);
        }
    }
}