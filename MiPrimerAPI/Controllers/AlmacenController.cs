
// 1) Espacios de nombres y declaración de la clase:

//Se importan los espacios de nombres necesarios, incluidos los de ASP.NET Core (Microsoft.AspNetCore.Http, Microsoft.AspNetCore.Mvc) y otros para manejar bases
//de datos (System.Data, System.Data.SqlClient).



using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiPrimerAPI.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MiPrimerAPI.Controllers
{
    //Se declara la clase AlmacenController, que actúa como un controlador en ASP.NET Core.

    //[Route("api/[controller]")]: Define la ruta de la API para este controlador. En este caso, la ruta será algo como "api/Almacen".
    // [ApiController]: Indica que esta clase es un controlador de API y automáticamente realiza comprobaciones de estado de modelo y
    // otros comportamientos específicos de API.

    [Route("api/[controller]")]
    [ApiController]

    
    public class AlmacenController : ControllerBase
    {
        //Método HTTP GET:
        //[HttpGet]: Indica que este método manejará solicitudes HTTP GET.
        [HttpGet]

        //public IEnumerable<RespuestaApi> Get(): Este método devuelve una colección de RespuestaApi.
        public IEnumerable<RespuestaApi> Get()
        {
            //using (var db = new AlmacenamientoContext()): Crea una instancia del contexto de la base de datos AlmacenamientoContext
            using (var db = new AlmacenamientoContext())  
            {
                //IEnumerable<RespuestaApi> respuestaAPI = db.RespuestaApis.ToList();: Consulta la base de datos para obtener todas las entradas
                //en la tabla RespuestaApi y las convierte en una lista.

                IEnumerable<RespuestaApi> respuestaAPI = db.RespuestaApis.ToList();

                //Retorna la lista como resultado de la solicitud GET.
                return respuestaAPI;
            }
        }
        //[HttpPost]: Indica que este método manejará solicitudes HTTP POST.

        [HttpPost]

        //public IActionResult fn_RegistrarEjemplo([FromBody] RequestModel pRequestModel): Este método registra un ejemplo en la base de datos a partir de
        //un modelo de solicitud (RequestModel).
        public IActionResult fn_RegistrarEjemplo([FromBody] RequestModel pRequestModel)
        {
            //if (!ModelState.IsValid) { return BadRequest(ModelState); }: Verifica si el modelo de la solicitud es válido. Si no es válido, devuelve
            //un error BadRequest con los detalles del modelo no válido.

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ResponseModel oResponseLogin = new ResponseModel();

            try
            {
                //using (var connection = new SqlConnection("...")): Crea una conexión a la base de datos SQL Server.

                using (var connection = new SqlConnection("Server=DESKTOP-GLVK46U;Database=Almacenamiento;Integrated Security=True;TrustServerCertificate=True"))
                {
                    connection.Open();

                    //using (var command = new SqlCommand("INSERT INTO RespuestaAPI ...", connection)): Crea un comando SQL para insertar datos en la tabla RespuestaAPI.
                    //Se establecen los parámetros del comando con los valores del modelo de solicitud.
                    //La cadena SQL proporcionada en el constructor del SqlCommand es una instrucción de inserción que se ejecutará en la base de datos. Esta instrucción
                    //está diseñada para insertar valores en la tabla RespuestaAPI en las columnas result_count, page_count, page_nbr, y next_page.
                    //La cadena contiene parámetros con nombres precedidos por "@" (@ResultCount, @PageCount, @PageNumber, @NextPage). Estos parámetros
                    //se utilizan para prevenir la inyección de SQL y se asignarán valores más adelante.

                    using (var command = new SqlCommand("INSERT INTO RespuestaAPI (result_count, page_count, page_nbr, next_page) VALUES (@ResultCount, @PageCount, @PageNumber, @NextPage);", connection))
                    {
                        //Se asignan valores a los parámetros de la consulta SQL utilizando el método AddWithValue del objeto command.
                        //@ResultCount, @PageCount, @PageNumber, y @NextPage son los nombres de los parámetros definidos en la instrucción SQL, y los valores
                        //provienen de propiedades del objeto pRequestModel, que es un objeto de la clase RequestModel que se pasa como argumento al método.
                        command.Parameters.AddWithValue("@ResultCount", pRequestModel.ResultCount);
                        command.Parameters.AddWithValue("@PageCount", pRequestModel.PageCount);
                        command.Parameters.AddWithValue("@PageNumber", pRequestModel.PageNumber);
                        command.Parameters.AddWithValue("@NextPage", pRequestModel.NextPage);

                        //command.ExecuteNonQuery();: Ejecuta el comando SQL para realizar la inserción.
                        //ExecuteNonQuery es un método que se utiliza para ejecutar comandos SQL que no devuelven datos, como las instrucciones INSERT, UPDATE, o DELETE.
                        //En este caso, ExecuteNonQuery ejecuta la instrucción INSERT en la base de datos, insertando una nueva fila con los valores proporcionados.
                        command.ExecuteNonQuery();
                    }
                }
                //oResponseLogin.Codigo = "1";: Asigna un código de respuesta a un objeto ResponseModel.

                oResponseLogin.Codigo = "1";

                //oResponseLogin.Descripcion = "SE REGISTRO CORRECTAMENTE";: Asigna una descripción de respuesta al objeto ResponseModel.
                oResponseLogin.Descripcion = "SE REGISTRO CORRECTAMENTE";

                //return new OkObjectResult(oResponseLogin);: Retorna un resultado HTTP 200 OK con el objeto ResponseModel como respuesta positiva.

                return new OkObjectResult(oResponseLogin);
            }
            //catch (Exception ex): Captura cualquier excepción que ocurra durante la ejecución del bloque try.

            catch (Exception ex)
            {
                //oResponseLogin = new ResponseModel();: Crea una nueva instancia de ResponseModel en caso de excepción.
                oResponseLogin = new ResponseModel();
                //oResponseLogin.Codigo = "0";: Asigna un código de error al objeto ResponseModel.
                oResponseLogin.Codigo = "0";
                //oResponseLogin.Descripcion = "OCURRIO UN ERROR " + ex.Message;: Asigna una descripción de error al objeto ResponseModel que incluye
                //el mensaje de la excepción.
                oResponseLogin.Descripcion = "OCURRIO UN ERROR " + ex.Message;

                //return new OkObjectResult(oResponseLogin);: Retorna un resultado HTTP 200 OK con el objeto ResponseModel como respuesta, indicando que hubo un error.
                return new OkObjectResult(oResponseLogin);
            }
        }
    }
}


//En resumen, este código define un controlador en ASP.NET Core que maneja solicitudes GET y POST relacionadas con una entidad llamada RespuestaApi.
//El método GET consulta la base de datos y devuelve una lista de respuestas, mientras que el método POST registra un nuevo ejemplo en la base de datos y
//devuelve un objeto ResponseModel que indica si la operación se realizó con éxito o si ocurrió algún error.