using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ModelDataTRH
{
    [Table("vListadoPedidosMonitorizacion")]
   public  class vListadoPedidosMonitorizacion
    {
        [Key]
        [Column(Order = 0, TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] indice { get; set; }

        [Key]
        [Column("Cod_ Agrupacion Pedido", Order = 1)]
        [StringLength(20)]
        public string Cod__Agrupacion_Pedido { get; set; }

        [StringLength(41)]
        public string codigoPedido { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Estado { get; set; }

        [StringLength(18)]
        public string estadoDescripcion { get; set; }

        [Key]
        [Column("No_ Albaran_ Venta", Order = 3)]
        [StringLength(20)]
        public string No__Albaran__Venta { get; set; }

        [StringLength(4000)]
        public string Id { get; set; }

        [Key]
        [Column("Doble Descarga", Order = 4)]
        public byte Doble_Descarga { get; set; }

        [Key]
        [Column(Order = 5)]
        public DateTime FechaSolicitud { get; set; }

        public TimeSpan? HoraSolicitud { get; set; }

        public DateTime? FechaMinimaEntrega { get; set; }

        [Column("Fecha Carga Requerida")]
        public DateTime? Fecha_Carga_Requerida { get; set; }

        [Key]
        [Column("Fecha Minima Entrega Carga 1", Order = 6)]
        public DateTime Fecha_Minima_Entrega_Carga_1 { get; set; }

        [Column("Fecha Minima Entrega Carga 2")]
        public DateTime? Fecha_Minima_Entrega_Carga_2 { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(63)]
        public string Provincia { get; set; }

        [Key]
        [Column("Provincia Carga 1", Order = 8)]
        [StringLength(30)]
        public string Provincia_Carga_1 { get; set; }

        [Key]
        [Column("Provincia Carga 2", Order = 9)]
        [StringLength(30)]
        public string Provincia_Carga_2 { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(63)]
        public string Localidad { get; set; }

        [Key]
        [Column("Localidad Carga 1", Order = 11)]
        [StringLength(30)]
        public string Localidad_Carga_1 { get; set; }

        [Key]
        [Column("Localidad Carga 2", Order = 12)]
        [StringLength(30)]
        public string Localidad_Carga_2 { get; set; }

        [Key]
        [Column(Order = 13)]
        public byte Tauliner { get; set; }

        [Key]
        [Column("Camion Grua", Order = 14)]
        public byte Camion_Grua { get; set; }

        [Key]
        [Column(Order = 15)]
        public decimal pesoKg { get; set; }

        [Key]
        [Column(Order = 16)]
        public decimal precio { get; set; }

        [Key]
        [Column("Nombre Transportista", Order = 17)]
        [StringLength(100)]
        public string Nombre_Transportista { get; set; }

        [Key]
        [Column("Transportista DNI", Order = 18)]
        [StringLength(20)]
        public string Transportista_DNI { get; set; }

        [Key]
        [Column("Matricula Camion", Order = 19)]
        [StringLength(10)]
        public string Matricula_Camion { get; set; }

        [Key]
        [Column("Matricula Remolque", Order = 20)]
        [StringLength(10)]
        public string Matricula_Remolque { get; set; }

        [Key]
        [Column("Codigo Agencia", Order = 21)]
        [StringLength(10)]
        public string Codigo_Agencia { get; set; }

        [Key]
        [Column("Transporte Cerrado", Order = 22)]
        public byte Transporte_Cerrado { get; set; }

        [Key]
        [Column("Enviado Pdf", Order = 23)]
        public byte Enviado_Pdf { get; set; }

        [Key]
        [Column("Cod_ Pedido Transporte", Order = 24)]
        [StringLength(20)]
        public string Cod__Pedido_Transporte { get; set; }

        [Key]
        [Column("Nombre Agencia", Order = 25)]
        [StringLength(50)]
        public string Nombre_Agencia { get; set; }

        [Column("Search Name")]
        [StringLength(50)]
        public string Search_Name { get; set; }

        [Column("Distancia Fabrica")]
        public decimal? Distancia_Fabrica { get; set; }

        [StringLength(20)]
        public string Code { get; set; }

        [Key]
        [Column(Order = 26)]
        [StringLength(63)]
        public string inicialesComercial { get; set; }
    }
}
