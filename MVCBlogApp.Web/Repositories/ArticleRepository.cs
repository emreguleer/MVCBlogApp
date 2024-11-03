using MVCBlogApp.Web.Models;
using MVCBlogApp.Web.ViewModels;
using System.Data.SqlClient;

namespace MVCBlogApp.Web.Repositories
{
    public class ArticleRepository
    {
        private readonly string _connectionString;

        public ArticleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<ArticleVM> GetAllArticles()
        {
            try
            {
                List<ArticleVM> articleVMs = new List<ArticleVM>();
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT au.Id AS AuthorId, c.Id AS CategoryId, a.Id, a.Name, a.Summary, au.Name AS AuthorName, c.Name AS CategoryName, a.CreatedDate  FROM Articles a JOIN Authors au ON a.AuthorId=au.Id JOIN Categories c ON a.CategoryId=c.Id ", conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                articleVMs.Add(new ArticleVM
                                {
                                    Id = (int)reader["Id"],
                                    AuthorId = (int)reader["AuthorId"],
                                    CategoryId = (int)reader["CategoryId"],
                                    Name = (string)reader["Name"],
                                    Summary = (string)reader["Summary"],
                                    CreatedDate = (DateTime)reader["CreatedDate"],
                                    AuthorName = (string)reader["AuthorName"],
                                    CategoryName = (string)reader["CategoryName"]

                                });
                            }
                        }
                    }
                }
                return articleVMs;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }    
        public ArticleVM GetArticleById(int id)
        {
            ArticleVM articleVM = new ArticleVM();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT a.Id, a.Name, a.Summary, a.Description, au.Name AS AuthorName, c.Name AS CategoryName, a.CreatedDate, c.Id AS CategoryId, au.Id AS AuthorId FROM Articles a JOIN Authors au ON a.AuthorId=au.Id JOIN Categories c ON a.CategoryId=c.Id WHERE a.Id=@id ", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        articleVM.Id = (int)reader["Id"];
                        articleVM.Name = (string)reader["Name"];
                        articleVM.Summary = (string)reader["Summary"];
                        articleVM.Description = (string)reader["Description"];
                        articleVM.CreatedDate = (DateTime)reader["CreatedDate"];
                        articleVM.AuthorName = (string)reader["AuthorName"];
                        articleVM.CategoryName = (string)reader["CategoryName"];
                        articleVM.CategoryId = (int)reader["CategoryId"];
                        articleVM.AuthorId = (int)reader["AuthorId"];
                    }
                }
            }
            return articleVM;
        }
        public List<ArticleVM> GetArticlesByAuthorId(int id)
        {
            List<ArticleVM> articleVMs = new List<ArticleVM>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT a.Id, a.Name, a.Description,a.Summary, au.Name AS AuthorName, c.Name AS CategoryName, c.Id AS CategoryId, a.CreatedDate  FROM Articles a JOIN Authors au ON a.AuthorId=au.Id JOIN Categories c ON a.CategoryId=c.Id WHERE au.Id=@id ", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            articleVMs.Add(new ArticleVM
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"],
                                Description = (string)reader["Description"],
                                Summary= (string)reader["Summary"],
                                CreatedDate = (DateTime)reader["CreatedDate"],
                                AuthorName = (string)reader["AuthorName"],
                                CategoryName = (string)reader["CategoryName"],
                                CategoryId = (int)reader["CategoryId"]

                            });
                        }
                    }
                }
            }
            return articleVMs;
        }
        public List<ArticleVM> GetArticlesByCategoryId(int id)
        {
            List<ArticleVM> articleVMs = new List<ArticleVM>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT a.Id, a.Name, a.Summary, a.Description, au.Name AS AuthorName, c.Name AS CategoryName, au.Id AS AuthorId, a.CreatedDate  FROM Articles a JOIN Authors au ON a.AuthorId=au.Id JOIN Categories c ON a.CategoryId=c.Id WHERE c.Id=@id ", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            articleVMs.Add(new ArticleVM
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"],
                                Summary = (string)reader["Summary"],
                                Description = (string)reader["Description"],
                                CreatedDate = (DateTime)reader["CreatedDate"],
                                AuthorName = (string)reader["AuthorName"],
                                CategoryName = (string)reader["CategoryName"],
                                AuthorId = (int)reader["AuthorId"]

                            });
                        }
                    }
                }
            }
            return articleVMs;
        }
        public void Add(Article article)
        {
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand("INSERT INTO Articles (Name, Description, Summary, CreatedDate,  CategoryId, AuthorId) VALUES (@name, @description, @summary, @createdDate, @categoryId, @authorId)", conn))
                {
                    cmd.Parameters.AddWithValue("@name", article.Name);
                    cmd.Parameters.AddWithValue("@description", article.Description);
                    cmd.Parameters.AddWithValue("@summary", article.Summary);
                    cmd.Parameters.AddWithValue("@createdDate", article.CreatedDate);
                    cmd.Parameters.AddWithValue("@categoryId", article.CategoryId);
                    cmd.Parameters.AddWithValue("@authorId", article.AuthorId);
                    cmd.ExecuteNonQuery();
                }
            }

        }
        public void Update(ArticleVM articleVM)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE Articles SET Name=@name, Summary=@summary, Description=@description, CategoryId=@categoryId, AuthorId=@authorId WHERE Id=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", articleVM.Id);
                    cmd.Parameters.AddWithValue("@name", articleVM.Name);
                    cmd.Parameters.AddWithValue("@description", articleVM.Description);
                    cmd.Parameters.AddWithValue("@summary", articleVM.Summary);
                    cmd.Parameters.AddWithValue("@categoryId", articleVM.CategoryId);
                    cmd.Parameters.AddWithValue("@authorId", articleVM.AuthorId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void Delete(ArticleVM articleVM)
        {

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Articles WHERE Id=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", articleVM.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
