using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAppCoreFramework.Models;
using WebAppCoreFramework.ViewModels;
using WebAppCoreFramework.Services;

namespace WebAppCoreFramework.Controllers
{
    public class FunctionsController : Controller
    {
        private readonly SqsService _sqsService;

        public FunctionsController()
        {
            _sqsService = new SqsService();
        }

        public IActionResult Index()
        {
            return View(new Email());
        }

        [HttpGet]
        public IActionResult Create(FunctionViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(FunctionViewModel model)
        {
            model.Response = await _sqsService.SendMessage(model.ToEmail());
            return RedirectToAction("Create", model);
        }
    }
}