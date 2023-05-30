using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace BaseDeDatosProyecto.Pages.Clientes
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> listClientes = new List<ClientInfo>();

        public void OnGet()
        {
            try
            {
                //Server = DESKTOP - KM8ED4I; Database; Initial Catalog = myStore; Trusted_Connection = True;
                String connectionString = "Data Source=Localhost;Initial Catalog=Proyecto;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM clientes";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientInfo clientInfo = new ClientInfo();
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.nombre = reader.GetString(1);
                                clientInfo.email = reader.GetString(2);
                                clientInfo.telefono = reader.GetString(3);
                                clientInfo.direccion = reader.GetString(4);
                                clientInfo.fecha_creacion = reader.GetDateTime(5).ToString();

                                listClientes.Add(clientInfo);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }

        public class ClientInfo
        {
            public String id;
            public String nombre;
            public String email;
            public String telefono;
            public String direccion;
            public String fecha_creacion;
        }
    }
}
