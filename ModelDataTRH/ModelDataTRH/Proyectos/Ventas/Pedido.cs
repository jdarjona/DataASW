using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDataTRH.Proyectos.Ventas
{
    public enum Status
    {

        /// <comentarios/>
        Open,

        /// <comentarios/>
        Released,

        /// <comentarios/>
        Pending_Approval,

        /// <comentarios/>
        Pending_Prepayment,
    }
    public enum Tipo_Gestion_Transporte
    {

        /// <comentarios/>
        _blank_,

        /// <comentarios/>
        Logistica,

        /// <comentarios/>
        Sus_medios,
    }
    public class Pedido
    {
        

        public string keyField{ get; set; }

        public string noField{ get; set; }

        public string sell_to_Customer_NoField{ get; set; }

        public string sell_to_Contact_NoField{ get; set; }

        public string sell_to_Customer_NameField{ get; set; }

        public string sell_to_AddressField{ get; set; }

        public string sell_to_Address_2Field{ get; set; }

        public string sell_to_CityField{ get; set; }

        public string codigo_Cliente_ContactoField{ get; set; }

        public string telefono_ClienteField{ get; set; }

        public string fax_ClienteField{ get; set; }

        public string email_ClienteField{ get; set; }

        public string vAT_Registration_NoField{ get; set; }

        public string sell_to_ContactField{ get; set; }

        public int no_of_Archived_VersionsField{ get; set; }

        public bool no_of_Archived_VersionsFieldSpecified{ get; set; }

        public DateTime order_DateField{ get; set; }

        public bool order_DateFieldSpecified{ get; set; }

        public DateTime document_DateField{ get; set; }

        public bool document_DateFieldSpecified{ get; set; }

        public DateTime requested_Delivery_DateField{ get; set; }

        public bool requested_Delivery_DateFieldSpecified{ get; set; }

        public DateTime promised_Delivery_DateField{ get; set; }

        public bool promised_Delivery_DateFieldSpecified{ get; set; }

        public int numero_IncidenciasField{ get; set; }

        public bool numero_IncidenciasFieldSpecified{ get; set; }

        public string salesperson_CodeField{ get; set; }

        public string cod_Agrupacion_PedidoField{ get; set; }

        public Status statusField{ get; set; }

        public bool statusFieldSpecified{ get; set; }

        public bool eslingasField{ get; set; }

        public bool eslingasFieldSpecified{ get; set; }

        public DateTime fecha_Revision_PreciosField{ get; set; }

        public bool fecha_Revision_PreciosFieldSpecified{ get; set; }

        public string external_Document_NoField{ get; set; }

        public string ship_to_CodeField{ get; set; }

        public string ship_to_NameField{ get; set; }

        public string ship_to_AddressField{ get; set; }

        public string ship_to_Address_2Field{ get; set; }

        public string ship_to_Post_CodeField{ get; set; }

        public string ship_to_CityField{ get; set; }

        public string ship_to_CountyField{ get; set; }

        public string ship_to_ContactField{ get; set; }

        public string ship_to_Country_Region_CodeField{ get; set; }

        public string location_CodeField{ get; set; }

        public DateTime shipment_DateField{ get; set; }

        public bool shipment_DateFieldSpecified{ get; set; }

        public decimal coste_Total_TransporteField{ get; set; }

        public bool coste_Total_TransporteFieldSpecified{ get; set; }

        public decimal euros_Tm_PorteField{ get; set; }

        public bool euros_Tm_PorteFieldSpecified{ get; set; }

        public Tipo_Gestion_Transporte tipo_Gestion_TransporteField{ get; set; }

        public bool tipo_Gestion_TransporteFieldSpecified{ get; set; }

        public int nº_Camiones_CalField{ get; set; }

        public bool nº_Camiones_CalFieldSpecified{ get; set; }

        public string telefono_ContactoField{ get; set; }

        public string email_ContactoField{ get; set; }

        public string movil_ContactoField{ get; set; }

        public string fax_ContactoField{ get; set; }

        public string cargo_ContactoField{ get; set; }

        public string comentariosField{ get; set; }

        public string email_EnvioField{ get; set; }

        public string telefono_EnvioField{ get; set; }

        public string fax_EnvioField{ get; set; }

        public bool doble_DescargaField{ get; set; }

        public bool doble_DescargaFieldSpecified{ get; set; }

        public string cod_Agencia_TransporteField{ get; set; }

        public bool camion_GruaField{ get; set; }

        public bool camion_GruaFieldSpecified{ get; set; }

        public string cod_Pedido_TransporteField{ get; set; }

        public DateTime? fecha_Carga_RequeridaField { get; set; }

        public bool fecha_Carga_RequeridaFieldSpecified{ get; set; }

        public Sales_Order_Subform_ws[] salesLinesField{ get; set; }
    }

    public enum Type
    {

        /// <comentarios/>
        _blank_,

        /// <comentarios/>
        G_L_Account,

        /// <comentarios/>
        Item,

        /// <comentarios/>
        Resource,

        /// <comentarios/>
        Fixed_Asset,

        /// <comentarios/>
        Charge_Item,
    }

    public  class Sales_Order_Subform_ws
    {

        public string keyField{ get; set; }

        public string document_NoField{ get; set; }

        public Type typeField{ get; set; }

        public bool typeFieldSpecified{ get; set; }

        public string noField{ get; set; }

        public string variant_CodeField{ get; set; }

        public string vAT_Prod_Posting_GroupField{ get; set; }

        public string descriptionField{ get; set; }

        public string location_CodeField{ get; set; }

        public decimal quantityField{ get; set; }

        public bool quantityFieldSpecified{ get; set; }

        public string unit_of_Measure_CodeField{ get; set; }

        public string unit_of_MeasureField{ get; set; }

        public decimal cantidad_PAQField{ get; set; }

        public bool cantidad_PAQFieldSpecified{ get; set; }

        public decimal cantidad_PAÑOField{ get; set; }

        public bool cantidad_PAÑOFieldSpecified{ get; set; }

        public decimal cantidad_M2Field{ get; set; }

        public bool cantidad_M2FieldSpecified{ get; set; }

        public decimal cantidad_KGField{ get; set; }

        public bool cantidad_KGFieldSpecified{ get; set; }

        public decimal descuentoField{ get; set; }

        public bool descuentoFieldSpecified{ get; set; }

        public decimal unit_PriceField{ get; set; }

        public bool unit_PriceFieldSpecified{ get; set; }

        public decimal precio_Ud_DestinoField{ get; set; }

        public bool precio_Ud_DestinoFieldSpecified{ get; set; }

        public decimal line_AmountField{ get; set; }

        public bool line_AmountFieldSpecified{ get; set; }

        public decimal precio_M2_DestinoField{ get; set; }

        public bool precio_M2_DestinoFieldSpecified{ get; set; }

        public decimal precio_PANO_DestinoField{ get; set; }

        public bool precio_PANO_DestinoFieldSpecified{ get; set; }

        public decimal precio_TM_DestinoField{ get; set; }

        public bool precio_TM_DestinoFieldSpecified{ get; set; }

        public decimal paquetesPendientesCargarField{ get; set; }

        public bool paquetesPendientesCargarFieldSpecified{ get; set; }

        public decimal paquetes_CargadosField{ get; set; }

        public bool paquetes_CargadosFieldSpecified{ get; set; }

        public int line_NoField{ get; set; }

        public bool line_NoFieldSpecified{ get; set; }

        public string aliasField{ get; set; }

        public decimal precio_Patio_M2Field{ get; set; }

        public bool precio_Patio_M2FieldSpecified{ get; set; }

        public decimal precio_Patio_TMField{ get; set; }

        public bool precio_Patio_TMFieldSpecified{ get; set; }

        public decimal precio_Patio_PanoField{ get; set; }

        public bool precio_Patio_PanoFieldSpecified{ get; set; }

        public decimal precioLineaTotalField{ get; set; }

        public bool precioLineaTotalFieldSpecified{ get; set; }

        public decimal precioLineaTotalPatioField{ get; set; }

        public bool precioLineaTotalPatioFieldSpecified{ get; set; }
    }
    }
