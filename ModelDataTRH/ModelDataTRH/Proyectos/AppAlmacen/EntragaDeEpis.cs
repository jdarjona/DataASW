using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDataTRH.Proyectos
{
    public class EntragaDeEpis
    {
        public string Assigned_User_ID { get; set; }
        public string Buy_from_Contact { get; set; }
        public string Buy_from_Country_Region_Code { get; set; }
        public string Buy_from_Post_Code { get; set; }
        public string Buy_from_Vendor_Name { get; set; }
        public string Buy_from_Vendor_No { get; set; }
        public decimal Cantidad_a_Recibir { get; set; }
        public bool Cantidad_a_RecibirSpecified { get; set; }
        public decimal Cantidad_Pdt_Facturar { get; set; }
        public bool Cantidad_Pdt_FacturarSpecified { get; set; }
        public decimal Cantidad_Total { get; set; }
        public bool Cantidad_TotalSpecified { get; set; }
        public string CodigoPedidoCabecera { get; set; }
        public string Currency_Code { get; set; }
        public System.DateTime Due_Date { get; set; }
        public bool Due_DateSpecified { get; set; }
        public System.DateTime Fecha_Embarque { get; set; }
        public bool Fecha_EmbarqueSpecified { get; set; }
        public System.DateTime Fecha_Firma { get; set; }
        public bool Fecha_FirmaSpecified { get; set; }
        public System.DateTime Fecha_LLegada { get; set; }
        public bool Fecha_LLegadaSpecified { get; set; }
        public bool Firma_Direccion { get; set; }
        public bool Firma_DireccionSpecified { get; set; }
        public decimal Importe_Total_Pedido { get; set; }
        public bool Importe_Total_PedidoSpecified { get; set; }
        public string Key { get; set; }
        public string Last_Receiving_No { get; set; }
        public string Location_Code { get; set; }
        public string No { get; set; }
        public int Nº_Ped_Venta_Asignados { get; set; }
        public bool Nº_Ped_Venta_AsignadosSpecified { get; set; }
        public string Order_Address_Code { get; set; }
        public string Pay_to_Contact { get; set; }
        public string Pay_to_Country_Region_Code { get; set; }
        public string Pay_to_Name { get; set; }
        public string Pay_to_Post_Code { get; set; }
        public string Pay_to_Vendor_No { get; set; }
        public System.DateTime Posting_Date { get; set; }
        public bool Posting_DateSpecified { get; set; }
        public string Purchaser_Code { get; set; }
        public string Ref_Barco { get; set; }
        public string Ship_to_City { get; set; }
        public string Ship_to_Code { get; set; }
        public string Ship_to_Contact { get; set; }
        public string Ship_to_Country_Region_Code { get; set; }
        public string Ship_to_Name { get; set; }
        public string Ship_to_Post_Code { get; set; }
        public string Shortcut_Dimension_1_Code { get; set; }
        public string Shortcut_Dimension_2_Code { get; set; }
        public int Status { get; set; }
        public bool StatusSpecified { get; set; }
        public string Vendor_Authorization_No { get; set; }
        public string Vendor_Invoice_No { get; set; }

    }
}
