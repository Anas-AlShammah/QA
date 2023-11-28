using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QA.Dtos;
using QA.Interfaces;
using QA.ViewModels;

namespace QA.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminController : Controller
    {
      
        private readonly IQuestion _question;
        private readonly IUserService _userService;
        private readonly ICategory _category;
        public AdminController(ICategory category,IUserService userService, IQuestion question)
        {
            _category = category;
            _userService = userService;
             _question = question;
        }
        public async Task<IActionResult> Index()
        {
            
            var adminModel = new ChartVM()
            {
                NumberOfQuestion=_question.NumberOfQuestions(),
                NumberOfUser= await _userService.UserCount()
            };
            return View(adminModel);
        }
        public IActionResult Register()
        {
            return View();
        }
        public async Task<IActionResult> AllQuestions()
        {

            var categiries = _category.GetAll();
            return View(categiries);
        }
		public async Task<IActionResult> QuestionsForCategory(int Id)
		{
            //_category.RemoveDuplicateQuestions(Id);
			var questions = _category.GetAllQuestionsForCategory(Id);
            ViewBag.Category = _category.CategoryName(Id);
            return View(questions);
		}
        [HttpPost]
        public async Task<IActionResult> DeleteQuestion(int Id)
        {

            _question.Delete(Id);
            return Redirect("index");
        }
        public IActionResult AddQuestion()
        {
            var categiries = _category.GetAll();
            return View(categiries);
        }
        public IActionResult UpdateQuestion(int Id)
        {
            var question = _question.GetQuestion(Id);
            return View(question);
        }
        [HttpPost]
        public IActionResult AddQuestion(QuestionDto questionDto)
        {
            _question.Add(questionDto);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult AddQuestions(string text,int Category)
        {
            _question.AddAll(text,Category);
            return RedirectToAction("Index");
        }
        public IActionResult Questions()
        {
            var questions = _question.GetAll();
            return View(questions);
        }
        public async Task<IActionResult> AllUsers()
        {
            var users = await _userService.GetAllUsers();

            return View(users);

        }

    }
}
