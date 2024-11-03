namespace MVCBlogApp.Web.ViewModels
{
    public class ArticleVM
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string AuthorName { get; set; }
        public string CategoryName { get; set; }
    }
}
