#r "System.Data"
#r "System.Data.Linq"

#load "Contactos.csx"

using System.Net;
using System.Data.SqlClient;
using System.Data.Linq;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["contactosstring"].ConnectionString;
    SqlConnection conn = new SqlConnection(connectionString);
    DataContext db = new DataContext(conn);
    Table<Contacto> contactosTable = db.GetTable<Contacto>();
    IEnumerable<Contacto> items = contactosTable.ToList();
    return req.CreateResponse(HttpStatusCode.OK, items);
}