using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hihi
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();

            guna2TextBox2.Leave += new EventHandler(guna2TextBox2_Leave);
        }

        private void guna2TextBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(guna2TextBox2.Text)) return;

            using (var connection = new SQLiteConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Projects WHERE GroupID = @GroupID";

                using (var cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@GroupID", guna2TextBox2.Text);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            guna2TextBox1.Text = reader["ProjectName"] != DBNull.Value ? reader["ProjectName"].ToString() : "";

                            // Jury 1 Notes
                            guna2TextBox3.Text = reader["J1_C1"] != DBNull.Value ? reader["J1_C1"].ToString() : "";
                            guna2TextBox4.Text = reader["J1_C2"] != DBNull.Value ? reader["J1_C2"].ToString() : "";
                            guna2TextBox5.Text = reader["J1_C3"] != DBNull.Value ? reader["J1_C3"].ToString() : "";
                            guna2TextBox7.Text = reader["J1_C4"] != DBNull.Value ? reader["J1_C4"].ToString() : "";
                            guna2TextBox6.Text = reader["J1_C5"] != DBNull.Value ? reader["J1_C5"].ToString() : "";

                            // Jury 2 Notes
                            guna2TextBox8.Text = reader["J2_C1"] != DBNull.Value ? reader["J2_C1"].ToString() : "";
                            guna2TextBox9.Text = reader["J2_C2"] != DBNull.Value ? reader["J2_C2"].ToString() : "";
                            guna2TextBox10.Text = reader["J2_C3"] != DBNull.Value ? reader["J2_C3"].ToString() : "";
                            guna2TextBox11.Text = reader["J2_C4"] != DBNull.Value ? reader["J2_C4"].ToString() : "";
                            guna2TextBox12.Text = reader["J2_C5"] != DBNull.Value ? reader["J2_C5"].ToString() : "";

                            // Jury 3 Notes
                            guna2TextBox13.Text = reader["J3_C1"] != DBNull.Value ? reader["J3_C1"].ToString() : "";
                            guna2TextBox14.Text = reader["J3_C2"] != DBNull.Value ? reader["J3_C2"].ToString() : "";
                            guna2TextBox15.Text = reader["J3_C3"] != DBNull.Value ? reader["J3_C3"].ToString() : "";
                            guna2TextBox16.Text = reader["J3_C4"] != DBNull.Value ? reader["J3_C4"].ToString() : "";
                            guna2TextBox17.Text = reader["J3_C5"] != DBNull.Value ? reader["J3_C5"].ToString() : "";

                            // Jury 4 Notes
                            guna2TextBox18.Text = reader["J4_C1"] != DBNull.Value ? reader["J4_C1"].ToString() : "";
                            guna2TextBox19.Text = reader["J4_C2"] != DBNull.Value ? reader["J4_C2"].ToString() : "";
                            guna2TextBox20.Text = reader["J4_C3"] != DBNull.Value ? reader["J4_C3"].ToString() : "";
                            guna2TextBox21.Text = reader["J4_C4"] != DBNull.Value ? reader["J4_C4"].ToString() : "";
                            guna2TextBox22.Text = reader["J4_C5"] != DBNull.Value ? reader["J4_C5"].ToString() : "";

                            // Calculate Final Average
                            double j1 = reader["Jury1_Moyen"] != DBNull.Value ? Convert.ToDouble(reader["Jury1_Moyen"]) : 0;
                            double j2 = reader["Jury2_Moyen"] != DBNull.Value ? Convert.ToDouble(reader["Jury2_Moyen"]) : 0;
                            double j3 = reader["Jury3_Moyen"] != DBNull.Value ? Convert.ToDouble(reader["Jury3_Moyen"]) : 0;
                            double j4 = reader["Jury4_Moyen"] != DBNull.Value ? Convert.ToDouble(reader["Jury4_Moyen"]) : 0;

                            double finalAverage = (j1 + j2 + j3 + j4) / 4;
                            guna2TextBox23.Text = finalAverage.ToString("0.00");
                        }
                        else
                        {
                            MessageBox.Show("No project found with that Group ID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void guna2TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Separator4_Click(object sender, EventArgs e)
        {

        }

        private void guna2Separator6_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox5_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox7_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void guna2TextBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox19_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(guna2TextBox2.Text) || string.IsNullOrWhiteSpace(guna2TextBox23.Text))
            {
                MessageBox.Show("Please ensure a Group ID is entered and the average is calculated.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var connection = new SQLiteConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();
                string query = "UPDATE Projects SET MoyenneGenerale = @Moyenne WHERE GroupID = @GroupID";

                using (var cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Moyenne", Convert.ToDouble(guna2TextBox23.Text));
                    cmd.Parameters.AddWithValue("@GroupID", guna2TextBox2.Text);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                       Form9 form9 = new Form9();
                        form9.Show();
                        this.Hide();

                    }
                }
            }
        }
    }
}
