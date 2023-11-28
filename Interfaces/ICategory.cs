using QA.Models;

namespace QA.Interfaces
{
    public interface ICategory
    {
        List<Category> GetAll();
        List<Question> GetAllQuestionsForCategory(int Id);
        string CategoryName(int Id);
        void RemoveDuplicateQuestions(int categoryId);

	}
}
