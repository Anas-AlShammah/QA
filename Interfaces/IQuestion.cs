using QA.Dtos;
using QA.Models;

namespace QA.Interfaces
{
    public interface IQuestion
    {
        void Add(QuestionDto questionDto);

        List<Question> GetAll();
        List<Question> SearchAll(string question);
        List<Question> realated(int id);
        int NumberOfQuestions();
        Question GetQuestion(int id);
        Question Search(string question);
    }
}
