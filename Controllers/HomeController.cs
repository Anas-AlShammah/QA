using Microsoft.AspNetCore.Mvc;
using QA.Dtos;
using QA.Interfaces;
using QA.Models;
using QA.ViewModels;
using System.Diagnostics;

namespace QA.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IQuestion _question;
        private readonly ICategory _category;

        public HomeController(ICategory category, IQuestion question,ILogger<HomeController> logger)
        {
            _category = category;
            _logger = logger;
            _question = question;
        }
        [HttpPost]
        public IActionResult AddQuestion(QuestionDto questionDto)
        {
            _question.Add(questionDto);
            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            var categiries = _category.GetAll();
            return View(categiries);
          
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Q1(int id)
        {
           var question = _question.GetQuestion(id);
            var realted = question.Category.questions;
            var QuestionMode = new QuestionVM()
            {
                Question = question,
                questions = realted
            };
            return View(QuestionMode);
        }
        public IActionResult Search(string str)
        {
            var questions = _question.SearchAll(str);
            ViewBag.Question = str;
            return View(questions);
        }
        public IActionResult Q2()
        {
            return View();
        }
		public async Task<IActionResult> QuestionsForCategory(int Id)
		{
			ViewBag.Category = _category.CategoryName(Id);
			var questions = _category.GetAllQuestionsForCategory(Id);
           

            return View(questions);
		}
		public IActionResult Questions()
        {
            var questions = _question.GetAll();
            return View(questions);
        }
        public IActionResult Question()
        {
           
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}