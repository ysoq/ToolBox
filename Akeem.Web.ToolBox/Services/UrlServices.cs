using Akeem.Web.ToolBox.Models;
using System;
using System.Collections.Generic;
using System.Data.HashFunction.MurmurHash;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akeem.Web.ToolBox.Services
{
    public class UrlServices
    {
        const string Str = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private readonly ToolsContext toolsContext;
        public UrlServices(ToolsContext toolsContext)
        {
            this.toolsContext = toolsContext;
        }

        public string ToHash1(string code)
        {
            StringBuilder sb = new StringBuilder();
            var bytes = GetByte(code);
            foreach (var item in bytes)
            {
                sb.Append(String.Format("{0:x}", Convert.ToInt32(item)));
            }
            return sb.ToString();
        }

        public string ToHash(string code)
        {
            byte[] b = new byte[4];
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
            Random r = new Random(BitConverter.ToInt32(b, 0));
            string s = null;

            for (int i = 0; i < 6; i++)
            {
                s += Str.Substring(r.Next(0, Str.Length - 1), 1);
            }
            return s;
        }

        internal async Task<ToolShortUrl> CompressAsync(ToolShortUrl urlModel)
        {
            ToolShortUrl firstModel = toolsContext.ToolShortUrl.FirstOrDefault(item => urlModel.Url.Equals(item.Url));
            if (firstModel == null)
            {
                string hash = ToHash(urlModel.Url);
                firstModel = new ToolShortUrl()
                {
                    Compress = hash,
                    ExpiredTime = DateTime.Now.AddYears(1),
                    Url = urlModel.Url,
                    CreTime = DateTime.Now
                };
                var result = await toolsContext.AddAsync(firstModel);
                await toolsContext.SaveChangesAsync();
            }
            return firstModel;
        }

        internal async Task AddRequsetAsync(ToolShortUrl urlModel)
        {
            try
            {
                ToolShortUrlReport urlReport = toolsContext.ToolShortUrlReport.FirstOrDefault(item => item.Id == urlModel.Id);
                if (urlReport == null)
                {
                    urlReport = new ToolShortUrlReport()
                    {
                        Id = urlModel.Id,
                        WatchNum = 1
                    };
                    _ = await toolsContext.ToolShortUrlReport.AddAsync(urlReport);
                }
                else
                {
                    urlReport.WatchNum++;
                }
                _ = await toolsContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }

        internal ToolShortUrl GetModel(string compress)
        {
            return toolsContext.ToolShortUrl.FirstOrDefault(item => item.Compress == compress); ;
        }

        public byte[] GetByte(string code)
        {
            byte[] srcBytes = Encoding.UTF8.GetBytes(code);
            var cfg = new MurmurHash3Config() { HashSizeInBits = 32, Seed = 0 };
            var mur = MurmurHash3Factory.Instance.Create(cfg);
            var hv = mur.ComputeHash(srcBytes);
            //var base64 = hv.AsBase64String();
            var hashBytes = hv.Hash;
            return hashBytes;
        }
    }
}
