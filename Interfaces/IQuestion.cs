using QA.Dtos;
using QA.Models;

namespace QA.Interfaces
{
    public interface IQuestion
    {
        void Add(QuestionDto questionDto);

        List<Question> GetAll();

        Question Search(string question);
    }
}
