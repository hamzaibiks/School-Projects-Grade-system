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
    public partial class Form5 : Form
    {
        private int currentJuryID;
        public Form5(int juryNumber)
        {
            InitializeComponent();

            currentJuryID = juryNumber;

            this.Text = "Data Entry - Jury " + currentJuryID;
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                double grade1 = Convert.ToDouble(guna2TextBox3.Text);
                double grade2 = Convert.ToDouble(guna2TextBox1.Text);
                double grade3 = Convert.ToDouble(guna2TextBox4.Text);
                double grade4 = Convert.ToDouble(guna2TextBox2.Text);
                double grade5 = Convert.ToDouble(guna2TextBox5.Text);

                double juryTotal = grade1 + grade2 + grade3 + grade4 + grade5;

                string groupID = guna2TextBox6.Text;
                string projectName = guna2TextBox7.Text;
                string dateNow = DateTime.Now.ToString("yyyy-MM-dd");

                using (var connection = new SQLiteConnection(DatabaseHelper.ConnectionString))
                {
                    connection.Open();

                    string checkQuery = "SELECT COUNT(*) FROM Projects WHERE GroupID = @GroupID";
                    bool exists = false;
                    using (var checkCmd = new SQLiteCommand(checkQuery, connection))
                    {
                        checkCmd.Parameters.AddWithValue("@GroupID", groupID);
                        exists = Convert.ToInt32(checkCmd.ExecuteScalar()) > 0;
                    }

                    string c1 = $"J{currentJuryID}_C1";
                    string c2 = $"J{currentJuryID}_C2";
                    string c3 = $"J{currentJuryID}_C3";
                    string c4 = $"J{currentJuryID}_C4";
                    string c5 = $"J{currentJuryID}_C5";
                    string moyen = $"Jury{currentJuryID}_Moyen";

                    if (exists)
                    {
                        string updateQuery = $"UPDATE Projects SET {c1}=@g1, {c2}=@g2, {c3}=@g3, {c4}=@g4, {c5}=@g5, {moyen}=@total, ProjectName=@ProjectName WHERE GroupID=@GroupID";
                        using (var cmd = new SQLiteCommand(updateQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@g1", grade1);
                            cmd.Parameters.AddWithValue("@g2", grade2);
                            cmd.Parameters.AddWithValue("@g3", grade3);
                            cmd.Parameters.AddWithValue("@g4", grade4);
                            cmd.Parameters.AddWithValue("@g5", grade5);
                            cmd.Parameters.AddWithValue("@total", juryTotal);
                            cmd.Parameters.AddWithValue("@ProjectName", projectName);
                            cmd.Parameters.AddWithValue("@GroupID", groupID);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        string insertQuery = $"INSERT INTO Projects (GroupID, ProjectName, {c1}, {c2}, {c3}, {c4}, {c5}, {moyen}, EvaluationDate) VALUES (@GroupID, @ProjectName, @g1, @g2, @g3, @g4, @g5, @total, @Date)";
                        using (var cmd = new SQLiteCommand(insertQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@GroupID", groupID);
                            cmd.Parameters.AddWithValue("@ProjectName", projectName);
                            cmd.Parameters.AddWithValue("@g1", grade1);
                            cmd.Parameters.AddWithValue("@g2", grade2);
                            cmd.Parameters.AddWithValue("@g3", grade3);
                            cmd.Parameters.AddWithValue("@g4", grade4);
                            cmd.Parameters.AddWithValue("@g5", grade5);
                            cmd.Parameters.AddWithValue("@total", juryTotal);
                            cmd.Parameters.AddWithValue("@Date", dateNow);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                Form7 successPopup = new Form7();
                successPopup.FormClosed += (s, args) => this.Close();
                successPopup.Show();
                this.Hide();
            }
            catch (FormatException)
            {
                MessageBox.Show("Please ensure all grades are valid numbers.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
