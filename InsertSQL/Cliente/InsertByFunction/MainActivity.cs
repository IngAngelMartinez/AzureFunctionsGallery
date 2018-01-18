using Android.App;
using Android.Widget;
using Android.OS;
using System.Net.Http;
using System;
using System.Text;
using Newtonsoft.Json;

namespace InsertByFunction
{
    [Activity(Label = "InsertByFunction", MainLauncher = true)]
    public class MainActivity : Activity
    {
        Button insertButton;
        EditText nombreTextControl;
        EditText apellidoTextControl;
        EditText correoTextControl;
        EditText telefonoTextControl;
        EditText edadTextControl;
        EditText nacimientoTextControl;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            insertButton = FindViewById<Button>(Resource.Id.btnInsert);
            nombreTextControl = FindViewById<EditText>(Resource.Id.txtNombre);
            apellidoTextControl = FindViewById<EditText>(Resource.Id.txtApellido);
            correoTextControl = FindViewById<EditText>(Resource.Id.txtCorreo);
            telefonoTextControl = FindViewById<EditText>(Resource.Id.txtTelefono);
            edadTextControl = FindViewById<EditText>(Resource.Id.txtEdad);
            nacimientoTextControl = FindViewById<EditText>(Resource.Id.txtNacimiento);

            insertButton.Click += InsertButton_Click;
        }

        protected override void OnPause()
        {
            base.OnPause();
            insertButton.Click -= InsertButton_Click;
        }

        private void InsertButton_Click(object sender, System.EventArgs e)
        {
            string nombreValue = nombreTextControl.Text;
            string apellidoValue = apellidoTextControl.Text;
            string correoValue = correoTextControl.Text;
            string telefonoValue = telefonoTextControl.Text;
            int edadValue = Convert.ToInt16(edadTextControl.Text);
            int diaNacimientoValue = Convert.ToInt16(nacimientoTextControl.Text.Split('/')[0]);
            int mesNacimientoValue = Convert.ToInt16(nacimientoTextControl.Text.Split('/')[1]);
            int anioNacimientoValue = Convert.ToInt16(nacimientoTextControl.Text.Split('/')[2]);
            InsertIntoFunction(nombreValue, apellidoValue, correoValue, telefonoValue, edadValue, diaNacimientoValue, mesNacimientoValue, anioNacimientoValue);
        }

        private async void InsertIntoFunction(string Nombre, string Apellido, string Correo, string Telefono, int Edad, int Dia, int Mes, int Anio)
        {
            HttpClient request = new HttpClient();
            var requestedLink = new Uri("https://aminespinoza.azurewebsites.net/api/InsertContacto?code=B1sG8aIXQ1BcPDv0AnWVf2PpGIIfBoxbunrt52Mzd9KS7epnTxzaaQ==");
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, requestedLink);

            var newContactRecord = new
            {
                nombre = Nombre,
                apellido = Apellido,
                correo = Correo,
                telefono = Telefono,
                edad = Edad,
                diaNacimiento = Dia,
                mesNacimiento = Mes,
                anioNacimiento = Anio
            };

            var messageString = JsonConvert.SerializeObject(newContactRecord);

            requestMessage.Content = new StringContent(messageString, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await request.SendAsync(requestMessage);
            var responseString = await response.Content.ReadAsStringAsync();

            SendToastNotification(Nombre);
        }

        private void SendToastNotification(string description)
        {
            string finalMessage = string.Format("El nuevo registro de {0}, está hecho", description);
            Toast.MakeText(this.ApplicationContext, finalMessage, ToastLength.Short).Show();
        }
    }  
}

