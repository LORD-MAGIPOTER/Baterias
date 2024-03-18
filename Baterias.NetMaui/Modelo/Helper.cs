using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Text.Json;



namespace Baterias.NetMaui.Modelo
{
    public class Helper
    {
        readonly HttpClient Cliente;
        List<Producto> ?DatosProducto = [];// ? INdica que DatosProducto puede ser nulo
        List<string>? Nombres = [];
        public Helper()
        {
            Cliente = new HttpClient();
            Cliente.DefaultRequestHeaders.Accept.Clear();
            Cliente.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/Json"));
        }

        public async Task<List<Producto>> ObtenerProductos()
        {
            string contenido;
            const string URI = "http://192.168.100.18/Baterias/api/Productos/ListaPilas/";

            try
            {
                HttpResponseMessage respuesta = await Cliente.GetAsync(URI);
                respuesta = respuesta.EnsureSuccessStatusCode();

                if (respuesta.IsSuccessStatusCode)
                {
                    contenido = await respuesta.Content.ReadAsStringAsync();
                    DatosProducto = JsonConvert.DeserializeObject<List<Producto>>(contenido);
                }
            }
            catch (HttpRequestException)
            {
                Producto pr = new Producto
                {
                    Nombre = "Ha ocurrido un problema al desplegar los datos"
                };
                DatosProducto.Add(pr);
            }
            return DatosProducto;

        }

        public async Task<List<string>> NombresProd()
        {
            string contenido;
            const string  URI = "http://192.168.100.18/Baterias/api/Productos/GetNombres";
            try
            {
                HttpResponseMessage respuesta = await Cliente.GetAsync(URI);
                respuesta = respuesta.EnsureSuccessStatusCode();
                if (respuesta.IsSuccessStatusCode)
                {
                    contenido = await respuesta.Content.ReadAsStringAsync();
                    Nombres = JsonConvert.DeserializeObject<List<string>>(contenido);
                }
            }
            catch (HttpRequestException)
            {
                string nombre = "Ha ocurrido un error al obtener los datos";
                Nombres.Add(nombre);
            }
            return Nombres;
        }
        

        public async Task<List<Producto>> ProductosPorNombre(string nombre)
        {
            string contenido;
            string URI = "http://192.168.100.18/Baterias/api/Productos/ProductoPorNombre/" + nombre;

            try
            {
                HttpResponseMessage respuesta = await Cliente.GetAsync(URI);
                respuesta = respuesta.EnsureSuccessStatusCode();

                if (respuesta.IsSuccessStatusCode){
                    contenido = await respuesta.Content.ReadAsStringAsync();
                    DatosProducto = JsonConvert.DeserializeObject<List<Producto>>(contenido);
                }
            }
            catch (HttpRequestException)
            {
                Producto Pr = new Producto
                {
                    Nombre = "Ha ocurrido un problema al desplegar los datos"
                };
                DatosProducto.Add(Pr);
            }
            return DatosProducto;
        }

        public async Task<List<Producto>> ProductosPorFecha(DateTime fechaI, DateTime fechaF)
        {
            string contenido;
            //se toman los parametros recibidos y se convierten en texto con formato añomesdía
            string fechaIString = fechaI.ToString("yyyy-MM-dd");
            string fechaFString = fechaF.ToString("yyyy-MM-dd");
            string URI = $"http://192.168.100.18/Baterias/api/Productos/PilasPorFecha/{fechaIString}/{fechaFString}";

            try
            {
                HttpResponseMessage respuesta = await Cliente.GetAsync(URI);
                respuesta = respuesta.EnsureSuccessStatusCode();

                if (respuesta.IsSuccessStatusCode)
                {
                    contenido = await respuesta.Content.ReadAsStringAsync();
                    DatosProducto = JsonConvert.DeserializeObject<List<Producto>>(contenido);
                }
            }
            catch (HttpRequestException)
            {
                Producto Pr = new Producto
                {
                    Nombre = "Ha ocurrido un problema al desplegar los datos"
                };
                DatosProducto.Add(Pr);
            }
            return DatosProducto;
        }

        //FUNCION PARA ENVIAR LOS DATOS AL API Y AGREGARLOS A LA BD
        public async Task<int> Agregar(Producto pila)
        {
            int inserto;
                string URI = "http://192.168.100.18:80/Baterias/api/Productos/AgregarPila";
                try
                {
                //Se tiene que hacer a la inversa,
                //es decir ahora se serializa el objeto para mandarlo en formato json

                //string pilaJson = JsonConvert.SerializeObject(pila);
                // Crea un objeto StringContent que contiene el JSON de la pila,
                // con UTF8 para que acepte nuestro alfabeto 
                //var contenido = new StringContent(pilaJson, Encoding.UTF8, "application/json");
                using StringContent jsonContent = new(
                        System.Text.Json.JsonSerializer.Serialize(new
                        {
                            Nombre = pila.Nombre,
                            Presentacion = pila.Presentacion,
                            FechaCaducidad= pila.FechaCaducidad,
                            Precio= pila.Precio,
                            ImagenPath= pila.ImagenPath
                        }),
                        Encoding.UTF8,
                        "application/json");

                //se realiza la solicitud POST al URI con el contenido del objeto pila
                HttpResponseMessage respuesta = await Cliente.PostAsync(URI, jsonContent);

                    //Se verifica si la solicitud tuvo exito
                    respuesta.EnsureSuccessStatusCode();
                if (respuesta.IsSuccessStatusCode) {
                    inserto = 1;
                }
                else
                {
                    inserto = 0;
                }
                return inserto;
                }
                catch (HttpRequestException ex)
                {
                    throw new Exception("Error al hacer la solicitud HTTP: " + ex.Message);
                }
            }









    }
}
