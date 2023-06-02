using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static BaseDeDatosProyecto.Pages.Clientes.IndexModel;

namespace BaseDeDatosProyecto.Pages.Clientes
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public string errorMessage = "";
        public string sucessMessage = "";
        public void OnGet()
        {
        }


        public void OnPost()
        {
            // Crear una nueva instancia de clientInfo
            clientInfo = new ClientInfo();

            // Obtener los valores de los campos del formulario
            
            clientInfo.nombre = Request.Form["nombre"];
            clientInfo.email = Request.Form["email"];
            clientInfo.telefono = Request.Form["telefono"];
            clientInfo.direccion = Request.Form["direccion"];

            if (string.IsNullOrEmpty(clientInfo.nombre) || string.IsNullOrEmpty(clientInfo.email) ||
                string.IsNullOrEmpty(clientInfo.telefono) || string.IsNullOrEmpty(clientInfo.direccion))
            {
                errorMessage = "Todos los campos son requeridos";
                return;
            }

            // guardar el nuevo cliente en la base de datos
            try
            {
                String connectionString = "Data Source=Localhost;Initial Catalog=Proyecto;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString)) 
                { 
                    connection.Open();
                    String sql = "INSERT INTO clientes " +
                                 "(nombre,email,telefono,direccion) Values " +
                                 "(@nombre,@email,@telefono,@direccion)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nombre",clientInfo.nombre);
                        command.Parameters.AddWithValue("@email", clientInfo.email);
                        command.Parameters.AddWithValue("@telefono", clientInfo.telefono);
                        command.Parameters.AddWithValue("@direccion", clientInfo.direccion);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
                clientInfo.nombre = ""; clientInfo.email = "";
                clientInfo.telefono = ""; clientInfo.direccion = "";
                sucessMessage = "Nuevo Cliente agregado";

                Response.Redirect("/Clientes/Index");
            
        }
    }
}
