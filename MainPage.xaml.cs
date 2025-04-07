using Microsoft.Data.SqlClient;
using System.Xml.Linq;

namespace SQL_Plan_Explorer
{
    public partial class MainPage : ContentPage
    {
        int count = 1;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
           count += count*2;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            string connectionString = "Persist Security Info=False;Integrated Security=true;Initial Catalog=Test;Server=IDTYUTIN\\MSSQLSERVER01;Encrypt=False;";
            string query = "SELECT * FROM books";

           using (var connection = new SqlConnection(connectionString)) {
             connection.Open();
           
             // Включаем получение плана в XML
             using (var cmd = new SqlCommand("SET SHOWPLAN_XML ON;", connection)) {
               cmd.ExecuteNonQuery();
             }
           
             // Выполняем запрос и получаем план
             using (var cmd = new SqlCommand(query, connection)) {
               using (var reader = cmd.ExecuteReader()) {
                 if (reader.Read()) {
                   string xmlPlan = reader.GetString(0);
              dwa.Text = XDocument.Parse(xmlPlan).ToString();
              SemanticScreenReader.Announce("");
              // Далее парсим XML и визуализируем...
            }
               }
             }
           }
        }
    }

}
