using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SQLQueryByFunction
{
    class Program
    {
        static void Main(string[] args)
        {
            GetWebResponse().Wait();
        }

        private static async Task GetEnumerableResponse()
        {
            //Cuando se trata de una respuesta de tipo IEnumerable
            var basicUrl = string.Format("https://aminespinoza.azurewebsites.net/api/SqlQuery?code=tjl2DxWzWI5gu66g5bA9gGJ/va65AaKNrWbJTz4rQP3abAP5kKEerw==");

            HttpClient request = new HttpClient();
            var responseString = await request.GetStringAsync(basicUrl);

            IEnumerable<Contacto> serializedEntity = JsonConvert.DeserializeObject<IEnumerable<Contacto>>(responseString);
        }


        private static async Task GetWebResponse()
        {
            //Cuando se trata de una respuesta de tipo HttpWebResponse
            var basicUrl = string.Format("https://aminespinoza.azurewebsites.net/api/SqlQuery?code=tjl2DxWzWI5gu66g5bA9gGJ/va65AaKNrWbJTz4rQP3abAP5kKEerw==");

            HttpClient request = new HttpClient();
            var responseString = await request.GetStringAsync(basicUrl);

            dynamic json = JsonConvert.DeserializeObject(responseString);

            List<Contacto> newContactList = new List<Contacto>();

            foreach (JToken item in json)
            {
                Contacto newContact = new Contacto();
                newContact.id = item.Value<int>("id");
                newContact.nombre = item.Value<string>("nombre");
                newContact.apellido = item.Value<string>("apellido");
                newContact.correo = item.Value<string>("correo");
                newContact.telefono = item.Value<string>("telefono");
                newContact.edad = item.Value<int>("edad");
                newContact.mesNacimiento = item.Value<int>("mesNacimiento");
                newContact.diaNacimiento = item.Value<int>("diaNacimiento");
                newContact.anioNacimiento = item.Value<int>("anioNacimiento");

                newContactList.Add(newContact);
            }
        }
    }

    public class Contacto
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }
        public int edad { get; set; }
        public int mesNacimiento { get; set; }
        public int diaNacimiento { get; set; }
        public int anioNacimiento { get; set; }
    }
}
