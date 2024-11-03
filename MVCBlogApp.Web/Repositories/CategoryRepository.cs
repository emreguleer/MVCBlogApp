using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Web.Mvc;
using MVCBlogApp.Web.Models;
using System.Web.Mvc.Routing.Constraints;

namespace MVCBlogApp.Web.Repositories
{
    public class CategoryRepository
    {
        private readonly string _connectionString;

        public CategoryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> GetAllCategories()
        {
            List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> categories = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand("SELECT * FROM Categories ", conn))
                {
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categories.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                            {
                                Value = reader["Id"].ToString(),
                                Text = reader["Name"].ToString(),
                            });
                        }
                    }
                }
            }
            return categories;
        }
        public Category GetCategoryById(int id)
        {
            Category category = new Category();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Categories WHERE Id=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        category.Id = (int)reader["Id"];
                        category.Name = (string)reader["Name"];
                    }
                }
            }
            return category;
        }
        public void Add(Category category)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Categories (Name) VALUES (@name)", conn))
                {
                    cmd.Parameters.AddWithValue("@name", category.Name);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public List<Category> GetAllCategoriesCount()
        {
            List<Category> categories = new List<Category>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT c.Id , c.Name , COUNT(a.CategoryId) AS ArticleNumber FROM Categories c LEFT JOIN Articles a ON c.Id = a.CategoryId GROUP BY c.Id, c.Name;", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categories.Add(new Category
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"],
                                ArticleNumber = (int)reader["ArticleNumber"],
                            });
                        }
                    }
                }
            }
            return categories;
        }
        public void Update(Category category)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE Categories SET Name=@name WHERE Id=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", category.Id);
                    cmd.Parameters.AddWithValue("@name", category.Name);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void Delete(Category category)
        {
            
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Categories WHERE Id=@id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", category.Id);
                        cmd.ExecuteNonQuery();
                       
                    }
                }
           
            
        }

    }
}
