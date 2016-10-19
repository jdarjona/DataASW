using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ModelDataTRH.Ventas
{
    public class Oferta
    {
        string keyField { get; set; }
        string noField { get; set; }
        string sell_to_Customer_NoField { get; set; }
        string sell_to_Contact_NoField { get; set; }
        string sell_to_Customer_NameField { get; set; }
        string sell_to_ContactField { get; set; }
        string sell_to_AddressField { get; set; }
        string sell_to_Address_2Field { get; set; }
        string sell_to_CityField { get; set; }
        string codigo_Cliente_ContactoField { get; set; }
        string telefono_ClienteField { get; set; }
        string fax_ClienteField { get; set; }
        string email_ClienteField { get; set; }
        string vAT_Registration_NoField { get; set; }
        int no_of_Archived_VersionsField { get; set; }
        bool no_of_Archived_VersionsFieldSpecified { get; set; }
        DateTime order_DateField { get; set; }
        bool order_DateFieldSpecified { get; set; }
        DateTime document_DateField { get; set; }
        bool document_DateFieldSpecified { get; set; }
        DateTime requested_Delivery_DateField { get; set; }
        bool requested_Delivery_DateFieldSpecified { get; set; }
        DateTime promised_Delivery_DateField { get; set; }
        bool promised_Delivery_DateFieldSpecified { get; set; }
        int numero_IncidenciasField { get; set; }
        bool numero_IncidenciasFieldSpecified { get; set; }
        string salesperson_CodeField { get; set; }
        string cod_Agrupacion_PedidoField { get; set; }
        int statusField { get; set; }
        bool statusFieldSpecified { get; set; }
        bool eslingasField { get; set; }
        bool eslingasFieldSpecified { get; set; }
        DateTime fecha_Revision_PreciosField { get; set; }
        bool fecha_Revision_PreciosFieldSpecified { get; set; }
        string external_Document_NoField { get; set; }
        string ship_to_CodeField { get; set; }
        string ship_to_NameField { get; set; }
        string ship_to_AddressField { get; set; }
        string ship_to_Address_2Field { get; set; }
        string ship_to_Post_CodeField { get; set; }
        string ship_to_CityField { get; set; }
        string ship_to_CountyField { get; set; }
        string ship_to_ContactField { get; set; }
        string ship_to_Country_Region_CodeField { get; set; }
        string location_CodeField { get; set; }
        DateTime shipment_DateField { get; set; }
        bool shipment_DateFieldSpecified { get; set; }
        double coste_Total_TransporteField { get; set; }
        bool coste_Total_TransporteFieldSpecified { get; set; }
        double euros_Tm_PorteField { get; set; }
        bool euros_Tm_PorteFieldSpecified { get; set; }
        int tipo_Gestion_TransporteField { get; set; }
        bool tipo_Gestion_TransporteFieldSpecified { get; set; }
        int nº_Camiones_CalField { get; set; }
        bool nº_Camiones_CalFieldSpecified { get; set; }
        string telefono_ContactoField { get; set; }
        string email_ContactoField { get; set; }
        string movil_ContactoField { get; set; }
        string fax_ContactoField { get; set; }
        string cargo_ContactoField { get; set; }
        string comentariosField { get; set; }
        string email_EnvioField { get; set; }
        string telefono_EnvioField { get; set; }
        string fax_EnvioField { get; set; }
        bool doble_DescargaField { get; set; }
        bool doble_DescargaFieldSpecified { get; set; }
        string cod_Agencia_TransporteField { get; set; }
        bool camion_GruaField { get; set; }
        bool camion_GruaFieldSpecified { get; set; }
        int idAlmacenField { get; set; }
        bool idAlmacenFieldSpecified { get; set; }

        SalesLinesField salesLinesField = new SalesLinesField();

        public class SalesLinesField
        {

            string keyField { get; set; }
            string document_NoField { get; set; }
            int typeField { get; set; }
            bool typeFieldSpecified { get; set; }
            string noField { get; set; }
            string variant_CodeField { get; set; }
            string vAT_Prod_Posting_GroupField { get; set; }
            string descriptionField { get; set; }
            string location_CodeField { get; set; }
            double quantityField { get; set; }
            bool quantityFieldSpecified { get; set; }
            string unit_of_Measure_CodeField { get; set; }
            string unit_of_MeasureField { get; set; }
            double cantidad_PAQField { get; set; }
            bool cantidad_PAQFieldSpecified { get; set; }
            double cantidad_PAÑOField { get; set; }
            bool cantidad_PAÑOFieldSpecified { get; set; }
            double cantidad_M2Field { get; set; }
            bool cantidad_M2FieldSpecified { get; set; }
            double cantidad_KGField { get; set; }
            bool cantidad_KGFieldSpecified { get; set; }
            double descuentoField { get; set; }
            bool descuentoFieldSpecified { get; set; }
            double unit_PriceField { get; set; }
            bool unit_PriceFieldSpecified { get; set; }
            double precio_Ud_DestinoField { get; set; }
            bool precio_Ud_DestinoFieldSpecified { get; set; }
            double line_AmountField { get; set; }
            bool line_AmountFieldSpecified { get; set; }
            double precio_M2_DestinoField { get; set; }
            bool precio_M2_DestinoFieldSpecified { get; set; }
            double precio_PANO_DestinoField { get; set; }
            bool precio_PANO_DestinoFieldSpecified { get; set; }
            double precio_TM_DestinoField { get; set; }
            bool precio_TM_DestinoFieldSpecified { get; set; }
            double paquetesPendientesCargarField { get; set; }
            bool paquetesPendientesCargarFieldSpecified { get; set; }
            double paquetes_CargadosField { get; set; }
            bool paquetes_CargadosFieldSpecified { get; set; }
            int line_NoField { get; set; }
            bool line_NoFieldSpecified { get; set; }
            string aliasField { get; set; }
            double precio_Patio_M2Field { get; set; }
            bool precio_Patio_M2FieldSpecified { get; set; }
            double precio_Patio_TMField { get; set; }
            bool precio_Patio_TMFieldSpecified { get; set; }
            double precio_Patio_PanoField { get; set; }
            bool precio_Patio_PanoFieldSpecified { get; set; }
            double precioLineaTotalField { get; set; }
            bool precioLineaTotalFieldSpecified { get; set; }
            double precioLineaTotalPatioField { get; set; }
            bool precioLineaTotalPatioFieldSpecified { get; set; }

        }
    }
}