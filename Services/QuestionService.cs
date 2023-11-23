using QA.Data;
using QA.Dtos;
using QA.Interfaces;
using QA.Models;
using System.Text.RegularExpressions;

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

        public Question GetQuestion(int id)
        {
            var question= _context.Question.FirstOrDefault(q=>q.Id == id);
            return question;
        }

        public List<Question> realated(int id)
        {
            var mainQuestion = _context.Question.FirstOrDefault(q => q.Id == id);

            if (mainQuestion == null)
            {
                return new List<Question>();
            }

            var allQuestions = _context.Question.ToList(); // Fetch all questions

            var relatedQuestions = allQuestions
                .Where(q => q.Id != id && AreWordsSimilar(mainQuestion, q))
                .ToList();

            return relatedQuestions;
        }

        private bool AreWordsSimilar(Question q1, Question q2)
        {
            var words1 = Regex.Split(q1.Title + " " + q1.Answer, @"\W+").Distinct();
            var words2 = Regex.Split(q2.Title + " " + q2.Answer, @"\W+").Distinct();

            var similarity = words1.Intersect(words2).Count() /
                             (double)(words1.Union(words2).Count());

            const double similarityThreshold = 0.5;

            return similarity >= similarityThreshold;
        }

        public Question Search(string question)
        {
            throw new NotImplementedException();
        }

        public List<Question> SearchAll(string question)
        {
            question = question.Replace("\r\n", "").Replace("\n", "");

            var searchWords = question.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var allQuestions = _context.Question.ToList();

            var filteredQuestions = allQuestions
                .Where(q =>
                    searchWords.Any(word =>
                        q.Title.Contains(word) || q.Answer.Contains(word)
                    )
                )
                .ToList();

            return filteredQuestions;
        }

        public int NumberOfQuestions()
        {
            var number = _context.Question.Count();
            return number;
        }
    }
  
}
