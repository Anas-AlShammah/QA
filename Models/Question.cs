namespace QA.Models
{
    public class Question
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Answer { get; set; }

        public string KeyWord { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } = default!;
    }
}
