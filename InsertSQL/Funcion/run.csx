#r "Newtonsoft.Json"

#load "Contactos.csx"

using System.Net;
using System.Data.SqlClient;
using System.Configuration;
using Newtonsoft.Json;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    log.Info("C# HTTP trigger function processed a request.");

    dynamic body = await req.Content.ReadAsStringAsync();
    var e = JsonConvert.DeserializeObject<Contacto>(body as string);

    bool successful;
    try
    {
        var cnnString  = ConfigurationManager.ConnectionStrings["contactosstring"].ConnectionString;
        
        using(var connection = new SqlConnection(cnnString))
        {
            connection.Open();

            DateTime currentTime = DateTime.Now;
            
            var recordInsert = "INSERT INTO [dbo].[Contactos] ([nombre],[apellido],[correo],[telefono],[edad],[mesNacimiento],[diaNacimiento],[anioNacimiento])" +  
                               "VALUES ('" + e.nombre + "','" + e.apellido + "','" + e.correo + "','" + e.telefono + "'," + e.edad + "," + e.mesNacimiento + "," + e.diaNacimiento + "," + e.anioNacimiento + ")";
                               
            log.Info(recordInsert);
        
            using (SqlCommand cmd = new SqlCommand(recordInsert, connection))
            {
                var rows = cmd.ExecuteNonQuery();
            }

            connection.Close();
            successful =true;
        }
    }
    catch
    {
        successful=false;
    }

    return successful
        ? req.CreateResponse(HttpStatusCode.OK, "It's all set")
        : req.CreateResponse(HttpStatusCode.BadRequest, "Something went wrong");
}
