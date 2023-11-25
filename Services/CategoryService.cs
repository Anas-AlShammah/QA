using Microsoft.EntityFrameworkCore;
using QA.Data;
using QA.Interfaces;
using QA.Models;

namespace QA.Services
{
    public class CategoryService : ICategory
    {
        private readonly AppDbContext _context;
        public CategoryService(AppDbContext context)
        {
            _context = context;
        }
        public List<Category> GetAll()
        {
            var categories = _context.Categories.ToList();
            return categories;
        }

		public List<Question> GetAllQuestionsForCategory(int Id)
		{
			var questions = _context.Categories
                .Include(c=>c.questions)
                .Where(c=>c.Id == Id)
                .SelectMany(c=>c.questions)
                .ToList();
            return questions;
		}
	}
}
