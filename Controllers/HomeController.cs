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

        public HomeController(IQuestion question,ILogger<HomeController> logger)
        {
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
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Q1(int id)
        {
           var question = _question.GetQuestion(id);
            var realted = _question.realated(id);
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