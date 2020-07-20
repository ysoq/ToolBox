using Akeem.Web.CommonUtils.Attribute;
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
    [ApiErrorHandle]
    public class EasyController : ControllerBase
    {
        private readonly ImgServices imgServices;

        public EasyController(ImgServices imgServices)
        {
            this.imgServices = imgServices;
        }

        [HttpGet("color/{width}*{height}")]

        public IActionResult BgColor1(int width, int height)
        {
            return File(this.imgServices.BackGround(width, height), "image/png");
        }
        [HttpGet("color/{size}")]

        public IActionResult BgColor2(int size)
        {
            CommonTools.Log("BgColor2");
            return File(this.imgServices.BackGround(size), "image/png");
        }
        [HttpGet("ex")]
        public IActionResult Ex()
        {
            int a = int.Parse("add");
            return Ok();
        }
    }
}
