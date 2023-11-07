using System.Diagnostics;
using MessageProvider.Handlers;
using MessageProvider.Models;
using Microsoft.AspNetCore.Mvc;

namespace MessageProvider.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMessageHandler _messageHandler;

        public HomeController(IMessageHandler messageHandler)
        {
            _messageHandler = messageHandler;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("message/{messageType}")]
        public IActionResult Message(string messageType)
        {
            var model = new JobViewModel() { MessageType = messageType };

            return View(model);
        }

        [HttpPost("/home/message", Name = "custom")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Message(JobViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Message",model);
            }

            await _messageHandler.PostMessage(model);

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}