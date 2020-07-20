using Akeem.Web.CommonUtils;
using Akeem.Web.CommonUtils.Attribute;
using Akeem.Web.ToolBox.Models;
using Akeem.Web.ToolBox.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Akeem.Web.ToolBox.Controllers
{
    [Route("/")]
    [ViewErrorHandle]
    public class HomeController : Controller
    {
        private readonly IOptions<ShortUrlSetting> options;

        public UrlServices UrlServices { get; }

        public HomeController(IOptions<ShortUrlSetting> options, UrlServices urlServices)
        {
            this.options = options;
            this.UrlServices = urlServices;
        }

        [HttpGet("/Home/Compress")]
        public IActionResult Compress()
        {
            return View();
        }

        [HttpPost("/Home/Compress")]
        public async Task<IActionResult> CompressPost(ToolShortUrl urlModel)
        {
            if (string.IsNullOrEmpty(urlModel.Url))
            {
                return BadRequest();
            }
            ToolShortUrl firstModel = await UrlServices.CompressAsync(urlModel);
            return Json(new
            {
                firstModel.Compress,
                firstModel.ExpiredTime,
                options.Value.BaseUrl,
                complete = options.Value.BaseUrl + firstModel.Compress
            });
        }

        [HttpGet("/api/short")]
        [IsJsonpCallback]
        public async Task<IActionResult> CompressGet(ToolShortUrl urlModel)
        {
            return await this.CompressPost(urlModel);
        }

        [HttpGet("/{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            ToolShortUrl urlModel = UrlServices.GetModel(id);
            if (urlModel != null)
            {
                await UrlServices.AddRequsetAsync(urlModel);
                return Redirect(urlModel.Url);
            }
            return RedirectToAction("Error_404", "Home");
        }


        [HttpGet("/")]
        public IActionResult Index(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View();
            }
            return View();
        }
        [HttpGet("/Home/Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet("/404")]
        public IActionResult Error_404()
        {
            throw new ArgumentNullException();
            return View();
        }
    }
}
