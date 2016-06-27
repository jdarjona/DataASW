namespace RepositorySqlTRH
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class entityNavision : DbContext
    {
        public entityNavision()
            : base("name=entityNavision")
        {
        }

        public virtual DbSet<vListadoPedidosMonitorizacion> vListadoPedidosMonitorizacion { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<vListadoPedidosMonitorizacion>()
                .Property(e => e.indice)
                .IsFixedLength();

            modelBuilder.Entity<vListadoPedidosMonitorizacion>()
                .Property(e => e.estadoDescripcion)
                .IsUnicode(false);

            modelBuilder.Entity<vListadoPedidosMonitorizacion>()
                .Property(e => e.pesoKg)
                .HasPrecision(38, 20);

            modelBuilder.Entity<vListadoPedidosMonitorizacion>()
                .Property(e => e.precio)
                .HasPrecision(38, 17);

            modelBuilder.Entity<vListadoPedidosMonitorizacion>()
                .Property(e => e.Distancia_Fabrica)
                .HasPrecision(38, 20);
        }
    }
}
