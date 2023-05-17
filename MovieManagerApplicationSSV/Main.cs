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
/*
 * Student Name: Snehal Vibhute
 * Date: 5/1/2023
 * Purpose: To build a Windows Forms application containing multiple forms that will interact with 
 * a SQL Server database hosted on Amazon Web Services (AWS).
 */
namespace MovieManagerApplicationSSV
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mnuAddMovie_Click(object sender, EventArgs e)
        {
            Add_Movie add_Movie = new Add_Movie();
            add_Movie.Show();
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
           About about = new About();
            about.MoreInformation = "Movie Manager Application\n\n\nThis application reads the data from a Microsoft SQL Server database. Further, it provides the typical CRUD operations: Create, Read, Update, and Delete against the movie table in our database\n\n\nAuthor:      Snehal Vibhute\nDate:        04/19/2023\nVersion:    1.0";
            about.ShowDialog();
        }

        private void mnuUpdateMovie_Click(object sender, EventArgs e)
        {
            UpdateMovie  updateMovie = new UpdateMovie();
            updateMovie.Show();
        }

        private void mnuDeleteMovie_Click(object sender, EventArgs e)
        {
            DeleteMovie deleteMovie = new DeleteMovie();
            deleteMovie.Show();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            string connectionString = GetConnectionString();
            List<Movie> movies = new List<Movie>();
            string sqlCommand = "SELECT Id, Title,  Year , Director, Genre, RottenTomatoesScore, TotalEarned  FROM  Movies  ORDER   BY  Title";
            string[] genres = {"", "Animation", "Action", "Comedy", "Drama", "Horror", "Mystery", "Romance", "Science Fiction", "Western"};

            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var movie = new Movie();
                                movie.Id = reader.GetInt32(0);
                                movie.Title = reader.GetString(1);
                                movie.Year = reader.GetInt32(2);
                                movie.Director = reader.GetString(3);

                                int genreNumber = reader.GetInt32(4);
                                movie.Genre = genres[genreNumber];

                                if (!reader.IsDBNull(5))
                                    movie.RottenTomatoesScore = reader.GetInt32(5);

                                if (!reader.IsDBNull(6))
                                    movie.TotalEarned = reader.GetDecimal(6);

                                movies.Add(movie);
                            }
                        }
                        connection.Close();

                        dgvMovies.DataSource = movies;
                        dgvMovies.Columns["TotalEarned"].DefaultCellStyle.Format = "C";
                        dgvMovies.Columns["RottenTomatoesScore"].Width = 130;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database connection failed.Error {ex.Message}");
            }
        }
            public string GetConnectionString()
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = @"coursemaster1.csbchotp6tva.us-east-2.rds.amazonaws.com,1433";
                builder.InitialCatalog = "CSCI1630";
                builder.UserID = "rw1630";
                builder.Password = "Project!";

                return builder.ConnectionString;
            }

        public void btnRefresh_Click(object sender, EventArgs e)
        {
            dgvMovies.DataSource = "";
            Main_Load(null, null);

        }

    }
}
