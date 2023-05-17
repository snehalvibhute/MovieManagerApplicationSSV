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

namespace MovieManagerApplicationSSV
{
    public partial class Add_Movie : Form
    {

        Main main = new Main();
        public Add_Movie()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            {
                string connectionString = main.GetConnectionString();

                string sqlCommand = ("INSERT INTO Movies(Title, Year, Director, Genre, RottenTomatoesScore, TotalEarned) " + "VALUES(@Title, @Year, @Director, @Genre, @RottenTomatoesScore, @TotalEarned)");
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                        {
                            try
                            {
                                command.Parameters.Add("Title", SqlDbType.VarChar, 500).Value = txtMovieTitle.Text;
                                command.Parameters.Add("Year", SqlDbType.Int).Value = txtYear.Text;
                                command.Parameters.Add("Director", SqlDbType.VarChar, 256).Value = txtDirector.Text;
                                switch (cmBoxGenre.Text)
                                {
                                    case ("Animation"):
                                        command.Parameters.Add("Genre", SqlDbType.Int).Value = 1;
                                        break;
                                    case ("Action"):
                                        command.Parameters.Add("Genre", SqlDbType.Int).Value = 2;
                                        break;
                                    case ("Comedy"):
                                        command.Parameters.Add("Genre", SqlDbType.Int).Value = 3;
                                        break;
                                    case ("Drama"):
                                        command.Parameters.Add("Genre", SqlDbType.Int).Value = 4;
                                        break;
                                    case ("Horror"):
                                        command.Parameters.Add("Genre", SqlDbType.Int).Value = 5;
                                        break;
                                    case ("Mystery"):
                                        command.Parameters.Add("Genre", SqlDbType.Int).Value = 6;
                                        break;
                                    case ("Romance"):
                                        command.Parameters.Add("Genre", SqlDbType.Int).Value = 7;
                                        break;
                                    case ("Science Fiction"):
                                        command.Parameters.Add("Genre", SqlDbType.Int).Value = 8;
                                        break;
                                    case ("Western"):
                                        command.Parameters.Add("Genre", SqlDbType.Int).Value = 9;
                                        break;
                                }
                                if (txtRottenScore == null)
                                {
                                    command.Parameters.Add("RottenTomatoesScore", SqlDbType.Int).Value = DBNull.Value;
                                }
                                else
                                    command.Parameters.Add("RottenTomatoesScore", SqlDbType.Int).Value = txtRottenScore.Text;

                                if (txtEarnings == null)
                                {
                                    command.Parameters.Add("TotalEarned", SqlDbType.Decimal).Value = DBNull.Value;
                                }
                                else
                                    command.Parameters.Add("TotalEarned", SqlDbType.Decimal).Value = txtEarnings.Text;

                                connection.Open();
                                command.ExecuteNonQuery();
                                connection.Close();

                                MessageBox.Show(txtMovieTitle.Text + " has been added to the database.", "Success");

                            }
                            catch (FormatException formatException)
                            {
                                MessageBox.Show(formatException.Message, "Invalid Format");
                            }
                            catch (ArgumentNullException argumentNullException)
                            {
                                MessageBox.Show(argumentNullException.Message, "Null Argument");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error");
                }
            }
        

            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtDirector.Text = "";
            txtEarnings.Text = "";
            txtMovieTitle.Text = "";
            txtRottenScore.Text = "";
            txtYear.Text = "";
            cmBoxGenre.Text = "";

        }

        
    }
}
