using QA.Data;
using QA.Dtos;
using QA.Interfaces;
using QA.Models;

namespace QA.Services
{
    public class QuestionService : IQuestion
    {
        private readonly AppDbContext _context;
        public QuestionService(AppDbContext context)
        {
            _context = context;
        }
        public void Add(QuestionDto questionDto)
        {
            var question = new Question()
            {
                Title = questionDto.Title,
                Answer = questionDto.Answer,
                KeyWord = questionDto.KeyWord,
            };
            _context.Question.Add(question);
            _context.SaveChanges();
            
        }

        public List<Question> GetAll()
        {
            var questions = _context.Question.ToList();
            return questions;
        }

        public Question Search(string question)
        {
            throw new NotImplementedException();
        }
    }
}
