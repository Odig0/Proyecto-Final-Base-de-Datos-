using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Net.Security;
using static BaseDeDatosProyecto.Pages.Clientes.IndexModel;

namespace BaseDeDatosProyecto.Pages.Clientes
{
    public class EditModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public string errorMessage = "";
        public string sucessMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];
            try
            {
                String connectionString = "Data Source=Localhost;Initial Catalog=Proyecto;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    
                    String sql = "SELECT id, nombre, email, telefono, direccion FROM clientes WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.nombre = reader.GetString(1);
                                clientInfo.email = reader.GetString(2);
                                clientInfo.telefono = reader.GetString(3);
                                clientInfo.direccion = reader.GetString(4);
                                

                            }
                        }
                    }
                }
            }
            catch  (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
        public void OnPost() 
        {
            clientInfo.id = Request.Form["id"];
            clientInfo.nombre = Request.Form["nombre"];
            clientInfo.email = Request.Form["email"];
            clientInfo.telefono = Request.Form["telefono"];
            clientInfo.direccion = Request.Form["direccion"];

        if (clientInfo.id.Length == 0 || clientInfo.nombre.Length == 0 || clientInfo.email.Length == 0 ||
                clientInfo.telefono.Length == 0 || clientInfo.direccion.Length == 0)
                {
                    errorMessage = "Todos los campos son requeridos";
                    return;
                }
            try
            {
                String connectionString = "Data Source=Localhost;Initial Catalog=Proyecto;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE clientes " +
                                 "SET nombre=@nombre, email=@email, telefono=@telefono, direccion=@direccion " +
                                 "WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                            command.Parameters.AddWithValue("@id", clientInfo.id);
                            command.Parameters.AddWithValue("@nombre", clientInfo.nombre);
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
            Response.Redirect("/Clientes/Index");

        }     
    }
}
