namespace Api.Models {
    public class BlogPost 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishedDate { get; set; }
        public ICollection<BlogPostComment> comments  { get; set; }



    }
}