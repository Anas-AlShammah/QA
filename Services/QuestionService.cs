using Microsoft.EntityFrameworkCore;
using QA.Data;
using QA.Dtos;
using QA.Interfaces;
using QA.Models;
using QA.ViewModels;
using System.Text.RegularExpressions;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

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
            var question= _context.Question
                .Include(q=>q.Category)
                .ThenInclude(c=>c.questions)
                .FirstOrDefault(q=>q.Id == id);
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

            // Split the question into search words
            var searchWords = question.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // Retrieve all questions from the database
            var allQuestions = _context.Question.ToList();

            // Perform case-insensitive, partial word matching
            var filteredQuestions = allQuestions
    .Where(q =>
        searchWords.All(word =>
            q.Title.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0
        )
    )
    .OrderBy(q => q.Title.Length)
    .ToList();
            //question = question.Replace("\r\n", "").Replace("\n", "");

            //var searchWords = question.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            //var allQuestions = _context.Question.ToList();

            //var filteredQuestions = allQuestions
            //    .Where(q =>
            //        searchWords.Any(word =>
            //            q.Title.Contains(word) 
            //        )
            //    )
            //    .ToList();
            //|| q.Answer.Contains(word)
            return filteredQuestions;
        }

        public int NumberOfQuestions()
        {
            var number = _context.Question.Count();
            return number;
        }

        public void AddAll(string text, int Category)
        {
            // Define the regular expression pattern to match question-answer pairs
            string pattern = @"السؤال رقم\s*:\s*\d+\s*(.*?)\s*الإجابة\s*:\s*(.*?)\s*(?=السؤال رقم|$)";

            // Use Regex to match question-answer pairs in the text
            MatchCollection matches = Regex.Matches(text, pattern, RegexOptions.Singleline);

            foreach (Match match in matches)
            {
                string question = match.Groups[1].Value.Trim();
                string answer = match.Groups[2].Value.Trim();

                var question1 = new Question()
                    {
                        Title = question,
                        Answer = answer,
                        KeyWord = "test",
                        CategoryId = Category
                    };
                    _context.Question.Add(question1);
                    _context.SaveChanges();
                }
            }

        public void Delete(int Id)
        {
            var question= _context.Question
                            .Where(q=>q.Id == Id)
                            .FirstOrDefault();
            _context.Question.Remove(question);
            _context.SaveChanges();
        }

		public int Update(int Id, UpdateQuestionDto updateQuestionDto)
		{
			var question = _context.Question  
			   .FirstOrDefault(q => q.Id == Id);
            question.Answer= updateQuestionDto.Answer;
            question.Title = updateQuestionDto.Title;
            _context.SaveChanges();
            return question.CategoryId;
		}

        public void AddOneQuestion(AddQuestion addQuestion)
        {
            var questions = new Question()
            {
                Title = addQuestion.Title,
                Answer = addQuestion.Answer,
                CategoryId = addQuestion.CategoryId,
                KeyWord = "test"
            };
            _context.Question.Add(questions);
            _context.SaveChanges();
        }
    }

    }


