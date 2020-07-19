using Akeem.Web.ToolBox.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Akeem.Web.ToolBox.Controllers
{
    [Route("et")]
    [ApiController]
    public class EasyController: ControllerBase
    {
        private readonly ImgServices imgServices;

        public EasyController(ImgServices imgServices, ILogger<HomeController> logger)
        {
            this.imgServices = imgServices;
            this.Logger = logger;
        }

        public ILogger<HomeController> Logger { get; }

        [HttpGet("color/{width}*{height}")]

        public IActionResult BgColor1(int width, int height)
        {
            try
            {
                return File(this.imgServices.BackGround(width, height), "image/png");
            }
            catch (Exception ex)
            {
                Logger.LogError("BgColor2", ex);
                throw ex;
            }
        }
        [HttpGet("color/{size}")]

        public IActionResult BgColor2(int size)
        {
            try
            {
                return File(this.imgServices.BackGround(size), "image/png");
            }
            catch (Exception ex)
            {
                Logger.LogError("BgColor2", ex);
                throw ex;
            }
        }
    }
}
