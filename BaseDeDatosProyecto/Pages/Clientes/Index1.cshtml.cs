using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace BaseDeDatosProyecto.Pages.MarcasComputadoras
{
    public class IndexModel1 : PageModel
    {
        public List<MarcasComputadoras> listClientes = new List<MarcasComputadoras>();

        public void OnGet()
        {
            try
            {
                //Server = DESKTOP - KM8ED4I; Database; Initial Catalog = myStore; Trusted_Connection = True;
                String connectionString = "Data Source=Localhost;Initial Catalog=Proyecto;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql2 = "SELECT * FROM MarcasComputadoras";
                    using (SqlCommand command2 = new SqlCommand(sql2, connection))
                    {
                        using (SqlDataReader reader = command2.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MarcasComputadoras clientInfoMarcasComputadoras = new MarcasComputadoras();
                                clientInfoMarcasComputadoras.id = "" + reader.GetInt32(0);
                                clientInfoMarcasComputadoras.nombre = reader.GetString(1);
                                clientInfoMarcasComputadoras.pais_origen= reader.GetString(2);

                                listClientes.Add(clientInfoMarcasComputadoras);

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

        public class MarcasComputadoras
        {
            public String id;
            public String nombre;
            public String pais_origen;
        }
    }
}
