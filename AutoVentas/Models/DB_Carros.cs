using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace AutoVentas.Models
{
    public partial class DB_Carros: DbContext
    {

        public DB_Carros() : base("name=DB_Carros") { }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Usuario> Usuario{ get; set; }
        public virtual DbSet<Marca> Marca { get; set; }
        public virtual DbSet<TipoVehiculo> TipoVehiculo { get; set; }
        public virtual DbSet<TipoCombustible> TipoCombustible { get; set; }
        public virtual DbSet<Vehiculo> Vehiculo { get; set; }
        public virtual DbSet<FormaDePago> FormaDePago { get; set; }
        public virtual DbSet<Reservacion> Reservacion { get; set; }
        public virtual DbSet<VentasRealizadas> VentasRealizadas{ get; set; }
        public virtual DbSet<ComprasRealizadas> ComprasRealizadas{ get; set; }
        public virtual DbSet<Archivo> Archivo { get; set; }

    }
}