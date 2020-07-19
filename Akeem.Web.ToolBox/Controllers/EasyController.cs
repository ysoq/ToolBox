using Akeem.Web.ToolBox.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Akeem.Web.ToolBox.Controllers
{
    [Route("et/[controller]")]
    [ApiController]
    public class EasyController: ControllerBase
    {
        private readonly ImgServices imgServices;

        public EasyController(ImgServices imgServices)
        {
            this.imgServices = imgServices;
        }
        [HttpGet("bgColor/{width}*{height}")]

        public IActionResult BgColor1(int width, int height)
        {
            return File(this.imgServices.BackGround(width, height), "image/Png");
        }
        [HttpGet("bgColor/{size}")]

        public IActionResult BgColor2(int size)
        {
            return File(this.imgServices.BackGround(size), "image/Png");
        }
    }
}
