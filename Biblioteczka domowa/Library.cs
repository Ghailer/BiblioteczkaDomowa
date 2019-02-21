using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows;

namespace Biblioteczka_domowa
{
    class Library
    {
        public List<Book> Books { get { return(this.SortBySerieThenTitle()); } }
        public Dictionary<int, string> series { get { return (this.GetSeries()); } }
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\libraryDB.mdf;Integrated Security=True";
        /// <summary>
        /// returns list of all books - without conditions
        /// </summary>
        /// <param name="filter"> Adds conditions</param>
        /// <returns></returns>
        private List<Book> GetBooks(string filter)
        {
            List<Book> listOfBooks = new List<Book>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("SELECT book.Id, book.Title, serie.Title as Serie, status.Name, book.Author, owner.Name + owner.Surname as 'Właściciel', holder.Name + holder.Surname as 'Posiadacz' FROM Books book "+
            "LEFT JOIN Series serie " +
            "on book.Serie = serie.Id " +
            "JOIN Status status " +
            "on book.Status = Status.Id "+
            "Join People owner " +
            "on book.Owner = owner.Id " +
            "Join People holder " +
            "on book.Holder = holder.Id "+ filter
            , connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {                    
                        listOfBooks.Add(new Book(int.Parse(reader["Id"].ToString()), reader["Title"].ToString(), reader["Serie"].ToString(), reader["Name"].ToString(), reader["Author"].ToString(), reader["Właściciel"].ToString(), reader["Posiadacz"].ToString()));
                    }
                }
                connection.Close();
            }
            return listOfBooks;
        }

        /// <summary>
        /// returns list of all books sorted by serie's title. If there's no serie, then book's title acts as serie
        /// </summary>
        /// <returns>sorted List of Books</returns>
        private List<Book> SortBySerieThenTitle()
        {
            string filter = "ORDER BY CASE " +
            "WHEN serie.Title IS NOT NULL THEN serie.Title " +
            "ELSE book.Title " +
            "END";
            return (GetBooks(filter));
        } 


        /// <summary>
        /// Returns all books of specified serie
        /// </summary>
        /// <param name="serieId"> Id of serie</param>
        /// <returns>List of "Book" type</returns>
        public List<Book> GetBooksOfSerie(int serieId)
        {
            string filter = "WHERE serie.Id =" + serieId;
            return (GetBooks(filter));
        }

        /// <summary>
        /// Returns list of series
        /// </summary>
        /// <returns>dictionary containing ID and Name of serie</returns>
        private Dictionary<int, string> GetSeries()
        {
            Dictionary<int, string> series = new Dictionary<int, string>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("SELECT * FROM Series serie ", connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        series.Add(int.Parse(reader["Id"].ToString()), reader["Title"].ToString());
                    }
                }
            }
            return (series);
        }


        private Dictionary<int, string> GetPlaces()
        {
            Dictionary<int, string> places = new Dictionary<int, string>();
            string sqlString = "SELECT * FROM Placement";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(sqlString, connection))
            {
                connection.Open();
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while(reader.Read())
                    {
                        places.Add(int.Parse(reader["Id"].ToString()), reader["Place"].ToString());
                    }
                }

            }
            return (places);
        }
        public void AddNewBook(string title, int serie, string Author, DateTime release, int status, int placement, int owner, int holder, string coverPath)
        {
            string sqlRequest = "INSERT INTO Books VALUES (@Title, @Serie, @Author, @Release, @Status, @Placement, @Owner, @Holder, @Cover)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(sqlRequest, connection))
            {
                command.Parameters.AddWithValue("@Title",title);
                command.Parameters.AddWithValue("@Serie", serie);
                command.Parameters.AddWithValue("@Author", Author);
                command.Parameters.AddWithValue("@Release", "1992-09-09");
                command.Parameters.AddWithValue("@Status", status);
                command.Parameters.AddWithValue("@Placement", placement);
                command.Parameters.AddWithValue("@Owner", owner);
                command.Parameters.AddWithValue("@Holder", holder);
                command.Parameters.AddWithValue("@Cover", coverPath);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void AddNewSerie(string name)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("INSERT INTO Series VALUES @Name"))
            {
                command.Parameters.AddWithValue("@Name", name);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void AddNewPeople(string name, string surname)
        {
            string sqlrequest = "INSERT INTO People VALUES @Name, @Surname";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(sqlrequest, connection))
            {
                connection.Open();
                SqlParameter param = new SqlParameter("@Name", name);
                command.Parameters.Add(param);
                command.Parameters.AddWithValue("@Surname", surname);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void AddNewPlace(string name)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("INSERT INTO Placement  VALUES @Name"))
            {
                command.Parameters.AddWithValue("@Name", name);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
