using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DssApplicationPoster.Models;

namespace DssApplicationPoster.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var dataTableListImage = MediaContentModel.GetMediaContentListImage();
            List<ImageContent> resultListImage = new List<ImageContent>();
            if (dataTableListImage.Rows.Count > 0)
            {
                resultListImage = MediaContentModel.SetDataMediaContentListImage(dataTableListImage);
            }

            var dataTableVideo = MediaContentModel.GetMediaContentVideo();
            VideoContent resultVideo = new VideoContent();
            if (dataTableVideo.Rows.Count > 0)
            {
                resultVideo = MediaContentModel.SetDataMediaContentVideo(dataTableVideo);
            }

            var dataTableText = MediaContentModel.GetMediaContentTextRun();
            TextRunning resultText = new TextRunning();
            if (dataTableText.Rows.Count > 0)
            {
                resultText = MediaContentModel.SetDataMediaContentTextRun(dataTableText);
            }

            GetAllContent result = new GetAllContent()
            {
                ListImageContent = resultListImage,
                GetVideoContent = resultVideo,
                GetTextRun = resultText
            };

            return View(result);
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
