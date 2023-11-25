using QA.Models;

namespace QA.Dtos
{
    public class UpdateQuestionDto
    {
    
        public string Title { get; set; }

        public string Answer { get; set; }

        public int CategoryId { get; set; }
 
    }
}
