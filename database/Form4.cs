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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace database
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        [Obsolete]
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=WIN-IP6D56K14LH\\SQLEXPRESS;Initial Catalog=Kollej;Integrated Security=True;Encrypt=False");
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into Oqituvchi values (@ismi, @familyasi)", con);

                //cmd.Parameters.AddWithValue("@id_oqituvchi", int.Parse(textBox1.Text));
                cmd.Parameters.Add("@ismi", textBox2.Text);
                cmd.Parameters.Add("@familyasi", textBox3.Text);
                cmd.ExecuteNonQuery();

                con.Close();
                MessageBox.Show("Muvfoqiyatli Qo'shildi!", "Yaxshi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
            }



        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=WIN-IP6D56K14LH\\SQLEXPRESS;Initial Catalog=Kollej;Integrated Security=True;Encrypt=False");
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from Oqituvchi", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            this.Hide();
            form3.ShowDialog();
            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=WIN-IP6D56K14LH\\SQLEXPRESS;Initial Catalog=Kollej;Integrated Security=True;Encrypt=False");
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Oqituvchi SET ismi=@ismi, familyasi=@familyasi WHERE id_oqituvchi=@id_oqituvchi ", conn);

                cmd.Parameters.AddWithValue("@id_oqituvchi", int.Parse(textBox1.Text));
                cmd.Parameters.AddWithValue("@ismi", textBox2.Text);
                cmd.Parameters.AddWithValue("@familyasi", textBox3.Text);
                cmd.ExecuteNonQuery();

                conn.Close();
                MessageBox.Show("Muvfoqiyatli O'zgartirildi!", "Yaxshi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Xatolik" + ex);
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=WIN-IP6D56K14LH\\SQLEXPRESS;Initial Catalog=Kollej;Integrated Security=True;Encrypt=False");
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE Oqituvchi WHERE id_oqituvchi=@id_oqituvchi ", conn);

                cmd.Parameters.AddWithValue("@id_oqituvchi", int.Parse(textBox1.Text));
                cmd.ExecuteNonQuery();

                conn.Close();
                MessageBox.Show("Muvfoqiyatli O'chirildi!", "Yaxshi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xatolik" + ex);
                textBox1.Clear();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=WIN-IP6D56K14LH\\SQLEXPRESS;Initial Catalog=Kollej;Integrated Security=True;Encrypt=False");
            con.Open();
            DataTable dt = new DataTable();

            try
            {
                if (!string.IsNullOrEmpty(textBox1.Text))
                {
                    if (int.TryParse(textBox1.Text, out int idValue))
                    {
                        SqlCommand cmd = new SqlCommand("SELECT * FROM Oqituvchi WHERE id_oqituvchi=@id_oqituvchi", con);
                        cmd.Parameters.AddWithValue("@id_oqituvchi", idValue);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd); 
                        adapter.Fill(dt);
                    }
                    else
                    {
                        MessageBox.Show("Iltimos, to'g'ri ID kiriting!", "Xatolik", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (!string.IsNullOrEmpty(textBox2.Text))
                {
                    string searchPattern = textBox2.Text + "%"; 
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Oqituvchi WHERE ismi LIKE @ismiPattern", con);
                    cmd.Parameters.AddWithValue("@ismiPattern", searchPattern);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd); 
                    adapter.Fill(dt);
                }
                else
                {
                    MessageBox.Show("Iltimos, qidirish uchun kalit so'z kiriting.", "Xatolik", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Bunaqa ma'lumot topilmadi.", "Ma'lumot", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Xatolik", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
