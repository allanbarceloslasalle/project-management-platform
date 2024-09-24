namespace Api.Models
{
    public class BlogPostComment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public int BlogPostId { get; set; }
        public BlogPost BlogPost { get; set; }

    }
}