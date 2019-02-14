using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Configuration;
using System.IO;
using System.Xml.Serialization;

namespace EvdekiIsimWebService
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tubitak.ozkanunsal.com/")] //buası önemli aynı adres olması lazım oraya atıcaz burayı
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces xmlns
        {
            get
            {
                XmlSerializerNamespaces xsn = new XmlSerializerNamespaces();
                xsn.Add("me", "http://tubitak.ozkanunsal.com/");
                return xsn;
            }

            set
            {
                // needed for serialization 
            }
        }

        [WebMethod]
        public string Send(string Metin)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("Metin", Metin);
            DataLayer.DataAccess.Insert("Veri", parameters);
            return "başarılı";
        }
        [WebMethod]
        public string VeriList()
        {
            string veriList;
            DataTable dt = DataLayer.DataAccess.GetTable("Veri");
            if (dt.Rows.Count > 0)
            {
                veriList = Convert.ToString(dt.Rows[0]["Metin"]);
                return JsonConvert.SerializeObject(veriList);
            }
            return null;

           

        }




    }
}
