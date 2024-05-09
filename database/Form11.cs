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

namespace database
{
    public partial class Form11 : Form
    {
        public Form11()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=WIN-IP6D56K14LH\\SQLEXPRESS;Initial Catalog=Kollej;Integrated Security=True;Encrypt=False");
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from dars_nomi", con);
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=WIN-IP6D56K14LH\\SQLEXPRESS;Initial Catalog=Kollej;Integrated Security=True;Encrypt=False");
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into dars_nomi values (@talim_turi, @guruh_nomi, @bugungi_mavzu)", con);

                cmd.Parameters.Add("@talim_turi", textBox2.Text);
                cmd.Parameters.Add("@guruh_nomi", textBox3.Text);
                cmd.Parameters.Add("@bugungi_mavzu", textBox5.Text);
                cmd.ExecuteNonQuery();

                con.Close();
                MessageBox.Show("Muvfoqiyatli Qo'shildi!", "Yaxshi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox5.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox5.Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=WIN-IP6D56K14LH\\SQLEXPRESS;Initial Catalog=Kollej;Integrated Security=True;Encrypt=False");
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE dars_nomi SET talim_turi=@talim_turi, guruh_nomi=@guruh_nomi, bugungi_mavzu=@bugungi_mavzu WHERE id_dars_nomi=@id_dars_nomi", conn);

                cmd.Parameters.AddWithValue("@id_dars_nomi", int.Parse(textBox1.Text));
                cmd.Parameters.Add("@talim_turi", textBox2.Text);
                cmd.Parameters.Add("@guruh_nomi", textBox3.Text);
                cmd.Parameters.Add("@bugungi_mavzu", textBox5.Text);
                cmd.ExecuteNonQuery();

                conn.Close();
                MessageBox.Show("Muvfoqiyatli O'zgartirildi!", "Yaxshi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox5.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Xatolik" + ex);
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox5.Clear();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=WIN-IP6D56K14LH\\SQLEXPRESS;Initial Catalog=Kollej;Integrated Security=True;Encrypt=False");
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE dars_nomi WHERE id_dars_nomi=@id_dars_nomi ", conn);

                cmd.Parameters.AddWithValue("@id_dars_nomi", int.Parse(textBox1.Text));
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
                        SqlCommand cmd = new SqlCommand("SELECT * FROM dars_nomi WHERE id_dars_nomi=@id_dars_nomi", con);
                        cmd.Parameters.AddWithValue("@id_dars_nomi", idValue);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);
                    }
                    else
                    {
                        MessageBox.Show("Iltimos, to'g'ri ID kiriting!", "Xatolik", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (!string.IsNullOrEmpty(textBox3.Text))
                {
                    string searchPattern = textBox3.Text + "%";
                    SqlCommand cmd = new SqlCommand("SELECT * FROM dars_nomi WHERE guruh_nomi LIKE @guruh_nomiPattern", con);
                    cmd.Parameters.AddWithValue("@guruh_nomiPattern", searchPattern);
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
    }
}
