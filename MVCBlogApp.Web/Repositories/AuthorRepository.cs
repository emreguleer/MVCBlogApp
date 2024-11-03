using MVCBlogApp.Web.Models;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Web.Mvc;

namespace MVCBlogApp.Web.Repositories
{
    public class AuthorRepository
    {
        private readonly string _connectionString;

        public AuthorRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> GetAllAuthors()
        {
            List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> auditors = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Authors ", conn))
                {
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            auditors.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                            {
                                Value = reader["Id"].ToString(),
                                Text = reader["Name"].ToString(),
                            });
                        }
                    }
                }
            }
            return auditors;
        }
        public void Add(Author author)
        {
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand("INSERT INTO Authors (Name) VALUES (@name)", conn))
                {
                    cmd.Parameters.AddWithValue("@name", author.Name);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public Author GetAuthorById(int id)
        {
            Author author = new Author();
            using(SqlConnection conn = new SqlConnection( _connectionString))
            {
                conn.Open();
                using( SqlCommand cmd = new SqlCommand("SELECT * FROM Authors WHERE Id=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        author.Id = (int)reader["Id"];
                        author.Name = (string)reader["Name"];
                    }
                }
            }
            return author;
        }
        public List<Author> GetAllAuthorsCount()
        {
            List<Author> authors = new List<Author>();
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using( SqlCommand cmd = new SqlCommand("SELECT au.Id, au.Name, COUNT(a.AuthorId) AS ArticleNumber FROM Authors au LEFT JOIN Articles a ON au.Id = a.AuthorId GROUP BY au.Id, au.Name;", conn))
                {
                    using( SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            authors.Add(new Author
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"],
                                ArticleNumber = (int)reader["ArticleNumber"],
                            });
                        }
                    }
                }
            }
            return authors;
        }
        public void Update(Author author)
        {
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand("UPDATE Authors SET Name=@name WHERE Id=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", author.Id);
                    cmd.Parameters.AddWithValue("@name", author.Name);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void Delete(Author author)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Authors WHERE Id=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", author.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        
    }
}
