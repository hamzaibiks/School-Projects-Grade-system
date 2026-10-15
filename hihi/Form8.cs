using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hihi
{
    public partial class Form8 : Form
    {

        private Bitmap printBitmap;
        public Form8()
        {
            InitializeComponent();

            this.Load += new EventHandler(Form8_Load);
        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {
            string search = guna2TextBox2.Text;

            LoadData(search, "", "");
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void Form8_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData(string searchQuery = "", string startDate = "", string endDate = "")
        {
            using (var connection = new SQLiteConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();

                string query = "SELECT GroupID, ProjectName, Jury1_Moyen, Jury2_Moyen, Jury3_Moyen, Jury4_Moyen, MoyenneGenerale, EvaluationDate FROM Projects WHERE 1=1";

                if (!string.IsNullOrWhiteSpace(searchQuery))
                {
                    query += " AND ProjectName LIKE @Search";
                }

                if (!string.IsNullOrWhiteSpace(startDate) && !string.IsNullOrWhiteSpace(endDate))
                {
                    query += " AND EvaluationDate BETWEEN @StartDate AND @EndDate";
                }

                using (var cmd = new SQLiteCommand(query, connection))
                {
                    if (!string.IsNullOrWhiteSpace(searchQuery))
                        cmd.Parameters.AddWithValue("@Search", "%" + searchQuery + "%");

                    if (!string.IsNullOrWhiteSpace(startDate))
                    {
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate);
                    }

                    using (var adapter = new SQLiteDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        guna2DataGridView1.DataSource = dataTable;
                    }
                }
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string search = guna2TextBox2.Text;

            string start = guna2DateTimePicker1.Value.ToString("yyyy-MM-dd");
            string end = guna2DateTimePicker2.Value.ToString("yyyy-MM-dd");

            LoadData(search, start, end);
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            int height = guna2DataGridView1.Height;
            guna2DataGridView1.Height = guna2DataGridView1.RowCount * guna2DataGridView1.RowTemplate.Height + guna2DataGridView1.ColumnHeadersHeight;

            printBitmap = new Bitmap(guna2DataGridView1.Width, guna2DataGridView1.Height);
            guna2DataGridView1.DrawToBitmap(printBitmap, new Rectangle(0, 0, guna2DataGridView1.Width, guna2DataGridView1.Height));

           
            guna2DataGridView1.Height = height;

            PrintPreviewDialog printPreview = new PrintPreviewDialog();
            PrintDocument printDoc = new PrintDocument();
            printDoc.PrintPage += new PrintPageEventHandler(printDoc_PrintPage);
            printPreview.Document = printDoc;
            printPreview.ShowDialog();
        }

        private void printDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Jury Grading System - Archive Report", new Font("Arial", 16, FontStyle.Bold), Brushes.Black, new Point(50, 50));
            e.Graphics.DrawImage(printBitmap, 50, 100);
        }
    }
}
