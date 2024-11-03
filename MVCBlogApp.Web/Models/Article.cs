namespace MVCBlogApp.Web.Models
{
    public class Article : BaseModel
    {
        public string Description { get; set; }
        public string Summary { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }
    }
}
