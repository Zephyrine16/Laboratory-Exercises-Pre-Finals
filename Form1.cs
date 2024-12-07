using System.Data;
using System.Data.SqlClient;

namespace PreFinals_Supplementary
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                MessageBox.Show("Please put an input.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            
            string connectionString = @"Data Source=DESKTOP-90RHKQ6;Initial Catalog=Supplementary;Integrated Security=True;TrustServerCertificate=True";

            
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open(); 

                    
                    string query = "INSERT INTO teybol (kolum1) VALUES (@inputdata)";

                    
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        
                        cmd.Parameters.AddWithValue("@inputdata", textBox.Text);

                        
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data saved successfully!");
                        textBox.Clear(); 
                    }
                }
                catch (Exception ex)
                {
                    
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
