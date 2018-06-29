<%@ WebService language="C#" class="localidades" %>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Services;
using System.Xml.Serialization;
  
[WebService(Namespace="http://cam.pronosticoextendido.net/",
			Description="Servicio web de devolucion de localidades de pronostico para PE.net")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]			
public class localidades : System.Web.Services.WebService
{

	[WebMethod]
	public string HelloWorld()
	{
		return "Hello World";
	}
	
    [WebMethod]
    public List<string> ObtenerLocalidades(string prefixText)
    {
		SqlDataSource ds = new SqlDataSource();
		ds.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["tsa_cam"].ConnectionString;
		ds.ProviderName = System.Configuration.ConfigurationManager.ConnectionStrings["tsa_cam"].ProviderName;
		ds.SelectCommand = "SELECT l.localidad, p.nprov, pa.pais " +
		"FROM localidades l " +
		"LEFT OUTER JOIN departamentos d ON (l.idDepartamentos = d.OGR_FID) " +
		"LEFT OUTER JOIN provincias p ON (d.idProvincias = p.OGR_FID) " +
		"INNER JOIN paises pa ON (l.idPaises = pa.idPaises) " +
		"WHERE l.codigoPron1 IS NOT NULL AND " +
		"(l.localidad LIKE '" + prefixText + "%' " +
		"OR p.nprov LIKE '" + prefixText + "%' " +
		"OR pa.pais LIKE '" + prefixText + "%') " +
		"GROUP BY l.localidad, p.nprov, pa.pais " +
		"ORDER BY pa.orden, pa.pais, l.idLocalidadesPron, p.nprov, l.localidad LIMIT 0, 50";
		DataView FData = (DataView)ds.Select(DataSourceSelectArguments.Empty);
        List<string> localidades = new List<string>();
		foreach (DataRow FRow in FData.Table.Rows)
		{
			if (FRow["nprov"].ToString() != "")
				localidades.Add(FRow["localidad"].ToString() + ", " + 
					FRow["nprov"].ToString() + ", " + FRow["pais"].ToString());
			else
				localidades.Add(FRow["localidad"].ToString() + ", " + FRow["pais"].ToString());
        }
        return localidades;
    }

}
