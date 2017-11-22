namespace AutoVentas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DB_Carros : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ComprasRealizadas",
                c => new
                    {
                        IDCompra = c.Int(nullable: false, identity: true),
                        IDVehiculo = c.Int(nullable: false),
                        FechaCompra = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.IDCompra)
                .ForeignKey("dbo.Vehiculoes", t => t.IDVehiculo, cascadeDelete: true)
                .Index(t => t.IDVehiculo);
            
            CreateTable(
                "dbo.Vehiculoes",
                c => new
                    {
                        IDVehiculo = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        IDMarca = c.Int(nullable: false),
                        Modelo = c.Int(nullable: false),
                        Precio = c.Double(nullable: false),
                        Estado = c.String(),
                        IDTipoCombustible = c.Int(nullable: false),
                        IDTipoVehiculo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IDVehiculo)
                .ForeignKey("dbo.Marcas", t => t.IDMarca, cascadeDelete: true)
                .ForeignKey("dbo.TipoCombustibles", t => t.IDTipoCombustible, cascadeDelete: true)
                .ForeignKey("dbo.TipoVehiculoes", t => t.IDTipoVehiculo, cascadeDelete: true)
                .Index(t => t.IDMarca)
                .Index(t => t.IDTipoCombustible)
                .Index(t => t.IDTipoVehiculo);
            
            CreateTable(
                "dbo.Marcas",
                c => new
                    {
                        IDMarca = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.IDMarca);
            
            CreateTable(
                "dbo.TipoCombustibles",
                c => new
                    {
                        IDTipoCombustible = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.IDTipoCombustible);
            
            CreateTable(
                "dbo.TipoVehiculoes",
                c => new
                    {
                        IDTipoVehiculo = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.IDTipoVehiculo);
            
            CreateTable(
                "dbo.FormaDePagoes",
                c => new
                    {
                        IDFormaDePago = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.IDFormaDePago);
            
            CreateTable(
                "dbo.Reservacions",
                c => new
                    {
                        IDReservacion = c.Int(nullable: false, identity: true),
                        IDVehiculo = c.Int(nullable: false),
                        IDUsuario = c.Int(nullable: false),
                        IDFormaDePago = c.Int(nullable: false),
                        FechaReserva = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.IDReservacion)
                .ForeignKey("dbo.FormaDePagoes", t => t.IDFormaDePago, cascadeDelete: true)
                .ForeignKey("dbo.Usuarios", t => t.IDUsuario, cascadeDelete: true)
                .ForeignKey("dbo.Vehiculoes", t => t.IDVehiculo, cascadeDelete: true)
                .Index(t => t.IDVehiculo)
                .Index(t => t.IDUsuario)
                .Index(t => t.IDFormaDePago);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        IDUsuario = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Apellido = c.String(nullable: false),
                        Telegono = c.Long(nullable: false),
                        Email = c.String(nullable: false),
                        Direccion = c.String(nullable: false),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        IDRol = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IDUsuario)
                .ForeignKey("dbo.Rols", t => t.IDRol, cascadeDelete: true)
                .Index(t => t.IDRol);
            
            CreateTable(
                "dbo.Rols",
                c => new
                    {
                        IDRol = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.IDRol);
            
            CreateTable(
                "dbo.VentasRealizadas",
                c => new
                    {
                        IDVenta = c.Int(nullable: false, identity: true),
                        IDVehiculo = c.Int(nullable: false),
                        IDUsuario = c.Int(nullable: false),
                        IDFormaDePago = c.Int(nullable: false),
                        FechaVenta = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.IDVenta)
                .ForeignKey("dbo.FormaDePagoes", t => t.IDFormaDePago, cascadeDelete: true)
                .ForeignKey("dbo.Usuarios", t => t.IDUsuario, cascadeDelete: true)
                .ForeignKey("dbo.Vehiculoes", t => t.IDVehiculo, cascadeDelete: true)
                .Index(t => t.IDVehiculo)
                .Index(t => t.IDUsuario)
                .Index(t => t.IDFormaDePago);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VentasRealizadas", "IDVehiculo", "dbo.Vehiculoes");
            DropForeignKey("dbo.VentasRealizadas", "IDUsuario", "dbo.Usuarios");
            DropForeignKey("dbo.VentasRealizadas", "IDFormaDePago", "dbo.FormaDePagoes");
            DropForeignKey("dbo.Reservacions", "IDVehiculo", "dbo.Vehiculoes");
            DropForeignKey("dbo.Reservacions", "IDUsuario", "dbo.Usuarios");
            DropForeignKey("dbo.Usuarios", "IDRol", "dbo.Rols");
            DropForeignKey("dbo.Reservacions", "IDFormaDePago", "dbo.FormaDePagoes");
            DropForeignKey("dbo.ComprasRealizadas", "IDVehiculo", "dbo.Vehiculoes");
            DropForeignKey("dbo.Vehiculoes", "IDTipoVehiculo", "dbo.TipoVehiculoes");
            DropForeignKey("dbo.Vehiculoes", "IDTipoCombustible", "dbo.TipoCombustibles");
            DropForeignKey("dbo.Vehiculoes", "IDMarca", "dbo.Marcas");
            DropIndex("dbo.VentasRealizadas", new[] { "IDFormaDePago" });
            DropIndex("dbo.VentasRealizadas", new[] { "IDUsuario" });
            DropIndex("dbo.VentasRealizadas", new[] { "IDVehiculo" });
            DropIndex("dbo.Usuarios", new[] { "IDRol" });
            DropIndex("dbo.Reservacions", new[] { "IDFormaDePago" });
            DropIndex("dbo.Reservacions", new[] { "IDUsuario" });
            DropIndex("dbo.Reservacions", new[] { "IDVehiculo" });
            DropIndex("dbo.Vehiculoes", new[] { "IDTipoVehiculo" });
            DropIndex("dbo.Vehiculoes", new[] { "IDTipoCombustible" });
            DropIndex("dbo.Vehiculoes", new[] { "IDMarca" });
            DropIndex("dbo.ComprasRealizadas", new[] { "IDVehiculo" });
            DropTable("dbo.VentasRealizadas");
            DropTable("dbo.Rols");
            DropTable("dbo.Usuarios");
            DropTable("dbo.Reservacions");
            DropTable("dbo.FormaDePagoes");
            DropTable("dbo.TipoVehiculoes");
            DropTable("dbo.TipoCombustibles");
            DropTable("dbo.Marcas");
            DropTable("dbo.Vehiculoes");
            DropTable("dbo.ComprasRealizadas");
        }
    }
}
