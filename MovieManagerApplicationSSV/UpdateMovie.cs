using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieManagerApplicationSSV
{
    public partial class UpdateMovie : Form
    {
        Main main = new Main();
        public UpdateMovie()
        {
            InitializeComponent();
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            {
                string connectionString = main.GetConnectionString();
                List<Movie> movies = new List<Movie>();
                string sqlCommand = "UPDATE Movies SET Title = @Title, Year = @Year, Director =@Director, Genre = @Genre, RottenTomatoesScore = @RottenTomatoesScore, TotalEarned = @TotalEarned WHERE Id = @Id";
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))

                    {

                        using (SqlCommand command = new SqlCommand(sqlCommand, connection))

                        {
                            command.Parameters.Add("@Id", SqlDbType.Int).Value = 0;
                            command.Parameters.Add("Title", SqlDbType.VarChar, 50).Value = txtMovieTitle.Text;
                            command.Parameters.Add("Year", SqlDbType.Int).Value = int.Parse(txtYear.Text);
                            command.Parameters.Add("Director", SqlDbType.VarChar, 50).Value = txtDirector.Text;
                            command.Parameters.Add("Genre", SqlDbType.VarChar, 50).Value = cmBoxGenre.SelectedItem.ToString();
                            command.Parameters.Add("RottenTomatoesScore", SqlDbType.Int).Value = int.Parse(txtRottenScore.Text);
                            command.Parameters.Add("TotalEarned", SqlDbType.Decimal).Value = decimal.Parse(txtEarnings.Text);

                            connection.Open();

                            object result = command.ExecuteNonQuery();
                            MessageBox.Show(txtMovieTitle.Text + " has been updated in the database.", "Success");
                        }

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error");
                }
        
        }
    }
        private void btnFind_Click(object sender, EventArgs e)
            {
            string connectionString = main.GetConnectionString();

            List<Movie> movies = new List<Movie>();

            string sqlCommand = "SELECT * FROM  Movies WHERE Title = @Title";

            try

            {

                using (SqlConnection connection = new SqlConnection(connectionString))

                {

                    using (SqlCommand command = new SqlCommand(sqlCommand, connection))

                    {

                        command.Parameters.Add("Title", SqlDbType.VarChar, 50).Value = txtMovieTitle.Text;

                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result == null)

                        {

                            MessageBox.Show("No Movie Found. Please try again.");

                        }

                        else

                        {

                            using (SqlDataReader reader = command.ExecuteReader())

                            {

                                while (reader.Read())

                                {

                                    txtYear.Text = (reader["Year"].ToString());

                                    txtDirector.Text = (reader["Director"].ToString());
                                    
                                    switch (reader["Genre"].ToString())

                                    {

                                        case ("1"):

                                            cmBoxGenre.Text = "Animation";

                                            break;

                                        case ("2"):

                                            cmBoxGenre.Text = "Action";

                                            break;
                                        
                                        case ("3"):

                                            cmBoxGenre.Text = "Comedy";

                                            break;

                                        case ("4"):

                                            cmBoxGenre.Text = "Drama";

                                            break;

                                        case ("5"):

                                            cmBoxGenre.Text = "Horror";

                                            break;
                                        
                                        case ("6"):

                                            cmBoxGenre.Text = "Mystery";

                                            break;

                                        case ("7"):

                                            cmBoxGenre.Text = "Romance";

                                            break;

                                        case ("8"):

                                            cmBoxGenre.Text = "Science Fiction";

                                            break;

                                        case ("9"):

                                            cmBoxGenre.Text = "Western";

                                            break;

                                        default:

                                            cmBoxGenre.Text = "";

                                            break;

                                    }

                                    if (reader["RottenTomatoesScore"] == DBNull.Value)

                                    {

                                        txtRottenScore.Text = "";

                                    }

                                    else

                                        txtRottenScore.Text = (reader["RottenTomatoesScore"].ToString());

                                    if (reader["TotalEarned"] == DBNull.Value)

                                    {

                                        txtEarnings.Text = "";

                                    }

                                    else

                                        txtEarnings.Text = (reader["TotalEarned"].ToString());

                                }

                            }

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

}

    
     

    


