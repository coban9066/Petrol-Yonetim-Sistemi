using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Petrol
{
    public partial class Form2 : Form
    {
        private readonly DataBaseHelper dbHelper = new DataBaseHelper();

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string subeAdi = textBox1.Text; 

            if (string.IsNullOrWhiteSpace(subeAdi))
            {
                MessageBox.Show("Lütfen bir şube adı giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = dbHelper.GetConnection())
                {
                    string query = "SELECT ad, soyad, yetki FROM kullanici WHERE sube = @sube";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@sube", subeAdi);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            listBox1.Items.Clear(); 
                            listBox2.Items.Clear(); 

                            while (reader.Read())
                            {
                                string ad = reader["ad"].ToString();
                                string soyad = reader["soyad"].ToString();
                                string yetki = reader["yetki"].ToString();

                                listBox1.Items.Add($"{ad} {soyad}");

                                listBox2.Items.Add(yetki);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
