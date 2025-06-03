using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Product
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BindData();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        public string connString()
        {
            return "Data Source=GWTN141-10\\MSSQLSERVER01;Initial Catalog=Product;User ID=sa;Password=1234;Encrypt=False";
        }

        private void button1_Click(object sender, EventArgs e)
        {


            string connStr = connString();
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = "INSERT INTO Products (ProductId, ProductName, Design, Color, Create_Date) VALUES (@val1, @val2, @val3, @val4, @val5)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@val1", int.Parse(textBox1.Text));
                    cmd.Parameters.AddWithValue("@val2", textBox2.Text);
                    cmd.Parameters.AddWithValue("@val3", textBox3.Text);
                    cmd.Parameters.AddWithValue("@val4", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@val5", DateTime.Now);  

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data inserted successfully.");
                        BindData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }
        void BindData()
        {
            try
            {
                string con = connString();
                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT ProductId, ProductName, Design, Color, " +
                    "CONVERT(varchar, Create_Date, 101) AS Create_Date, " +
                    "CONVERT(varchar, Update_Date, 101) AS Update_Date " +
                    "FROM Products", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.BorderStyle = BorderStyle.None;
                dataGridView1.RowsDefaultCellStyle.ForeColor = Color.Black;
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this product?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string con = connString(); // your connection string method
                using (SqlConnection connection = new SqlConnection(con))
                {
                    try
                    {
                        connection.Open();

                        string productIdToDelete = textBox1.Text; // assuming you have a TextBox named textBox1

                        string query = "DELETE FROM Products WHERE ProductId = @ProductId";
                        SqlCommand cmd = new SqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@ProductId", productIdToDelete);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Product deleted successfully.");
                            BindData();
                        }
                        else
                        {
                            MessageBox.Show("No product found with that ID.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            string con = connString(); // Your method to get the connection string


            using (SqlConnection connection = new SqlConnection(con))
            {
                try
                {
                    connection.Open();

                    string query = @"UPDATE Products 
                             SET ProductName = @ProductName, 
                                 Design = @Design, 
                                 Color = @Color, 
                                 Update_date = @Update_date
                             WHERE ProductId = @ProductId";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@ProductId", textBox1.Text);
                    cmd.Parameters.AddWithValue("@ProductName", textBox2.Text);
                    cmd.Parameters.AddWithValue("@Design", textBox3.Text);
                    cmd.Parameters.AddWithValue("@Color", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@Update_Date", DateTime.Now);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Product updated successfully.");
                        BindData();
                    }
                    else
                    {
                        MessageBox.Show("Product ID not found.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
