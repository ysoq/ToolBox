using System;
using System.Collections.Generic;

namespace Akeem.Web.ToolBox.Models
{
    public partial class ToolShortUrl
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Compress { get; set; }
        public DateTime? ExpiredTime { get; set; }
        public DateTime? CreTime { get; set; }
    }
}
