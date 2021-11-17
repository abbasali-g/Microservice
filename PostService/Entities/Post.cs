namespace PostService.Entities
{
    public class Post
    {
        public int PostId { get; set; }
        public string PostTitle { get; set; }
        public string PostContent { get; set; }
        public User User { get; set; }
    }
}