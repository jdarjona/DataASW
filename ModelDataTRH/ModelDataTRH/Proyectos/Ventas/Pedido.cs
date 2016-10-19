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
        

        public string keyField;

        public string noField;

        public string sell_to_Customer_NoField;

        public string sell_to_Contact_NoField;

        public string sell_to_Customer_NameField;

        public string sell_to_AddressField;

        public string sell_to_Address_2Field;

        public string sell_to_CityField;

        public string codigo_Cliente_ContactoField;

        public string telefono_ClienteField;

        public string fax_ClienteField;

        public string email_ClienteField;

        public string vAT_Registration_NoField;

        public string sell_to_ContactField;

        public int no_of_Archived_VersionsField;

        public bool no_of_Archived_VersionsFieldSpecified;

        public System.DateTime order_DateField;

        public bool order_DateFieldSpecified;

        public System.DateTime document_DateField;

        public bool document_DateFieldSpecified;

        public System.DateTime requested_Delivery_DateField;

        public bool requested_Delivery_DateFieldSpecified;

        public System.DateTime promised_Delivery_DateField;

        public bool promised_Delivery_DateFieldSpecified;

        public int numero_IncidenciasField;

        public bool numero_IncidenciasFieldSpecified;

        public string salesperson_CodeField;

        public string cod_Agrupacion_PedidoField;

        public Status statusField;

        public bool statusFieldSpecified;

        public bool eslingasField;

        public bool eslingasFieldSpecified;

        public System.DateTime fecha_Revision_PreciosField;

        public bool fecha_Revision_PreciosFieldSpecified;

        public string external_Document_NoField;

        public string ship_to_CodeField;

        public string ship_to_NameField;

        public string ship_to_AddressField;

        public string ship_to_Address_2Field;

        public string ship_to_Post_CodeField;

        public string ship_to_CityField;

        public string ship_to_CountyField;

        public string ship_to_ContactField;

        public string ship_to_Country_Region_CodeField;

        public string location_CodeField;

        public System.DateTime shipment_DateField;

        public bool shipment_DateFieldSpecified;

        public decimal coste_Total_TransporteField;

        public bool coste_Total_TransporteFieldSpecified;

        public decimal euros_Tm_PorteField;

        public bool euros_Tm_PorteFieldSpecified;

        public Tipo_Gestion_Transporte tipo_Gestion_TransporteField;

        public bool tipo_Gestion_TransporteFieldSpecified;

        public int nº_Camiones_CalField;

        public bool nº_Camiones_CalFieldSpecified;

        public string telefono_ContactoField;

        public string email_ContactoField;

        public string movil_ContactoField;

        public string fax_ContactoField;

        public string cargo_ContactoField;

        public string comentariosField;

        public string email_EnvioField;

        public string telefono_EnvioField;

        public string fax_EnvioField;

        public bool doble_DescargaField;

        public bool doble_DescargaFieldSpecified;

        public string cod_Agencia_TransporteField;

        public bool camion_GruaField;

        public bool camion_GruaFieldSpecified;

        public string cod_Pedido_TransporteField;

        public System.DateTime fecha_Carga_RequeridaField;

        public bool fecha_Carga_RequeridaFieldSpecified;

        public Sales_Order_Subform_ws[] salesLinesField;
    }
    public  class Sales_Order_Subform_ws
    {

        public string keyField;

        public string document_NoField;

        public Type typeField;

        public bool typeFieldSpecified;

        public string noField;

        public string variant_CodeField;

        public string vAT_Prod_Posting_GroupField;

        public string descriptionField;

        public string location_CodeField;

        public decimal quantityField;

        public bool quantityFieldSpecified;

        public string unit_of_Measure_CodeField;

        public string unit_of_MeasureField;

        public decimal cantidad_PAQField;

        public bool cantidad_PAQFieldSpecified;

        public decimal cantidad_PAÑOField;

        public bool cantidad_PAÑOFieldSpecified;

        public decimal cantidad_M2Field;

        public bool cantidad_M2FieldSpecified;

        public decimal cantidad_KGField;

        public bool cantidad_KGFieldSpecified;

        public decimal descuentoField;

        public bool descuentoFieldSpecified;

        public decimal unit_PriceField;

        public bool unit_PriceFieldSpecified;

        public decimal precio_Ud_DestinoField;

        public bool precio_Ud_DestinoFieldSpecified;

        public decimal line_AmountField;

        public bool line_AmountFieldSpecified;

        public decimal precio_M2_DestinoField;

        public bool precio_M2_DestinoFieldSpecified;

        public decimal precio_PANO_DestinoField;

        public bool precio_PANO_DestinoFieldSpecified;

        public decimal precio_TM_DestinoField;

        public bool precio_TM_DestinoFieldSpecified;

        public decimal paquetesPendientesCargarField;

        public bool paquetesPendientesCargarFieldSpecified;

        public decimal paquetes_CargadosField;

        public bool paquetes_CargadosFieldSpecified;

        public int line_NoField;

        public bool line_NoFieldSpecified;

        public string aliasField;

        public decimal precio_Patio_M2Field;

        public bool precio_Patio_M2FieldSpecified;

        public decimal precio_Patio_TMField;

        public bool precio_Patio_TMFieldSpecified;

        public decimal precio_Patio_PanoField;

        public bool precio_Patio_PanoFieldSpecified;

        public decimal precioLineaTotalField;

        public bool precioLineaTotalFieldSpecified;

        public decimal precioLineaTotalPatioField;

        public bool precioLineaTotalPatioFieldSpecified;
    }
    }
