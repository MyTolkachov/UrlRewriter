using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using URL.Rewriter.DAL;
using URL.Rewriter.DAL.Models;

namespace URL.Rewriter.BLL
{
    public class UrlHelper
    {
        UrlsDbContext dbContext = new UrlsDbContext();

        public string Getlong(string shortUrl)
        {
            return dbContext.Urls.FirstOrDefault(u => u.Short == shortUrl).Long;
        }

        public string GenerateUrl(string longUrl)
        {
            string domainName = string.Empty;
            Regex rg = new Regex("://(?<host>([a-z\\d][-a-z\\d]*[a-z\\d]\\.)*[a-z][-a-z\\d]+[a-z])");
            if (rg.IsMatch(longUrl))
            {
                const int httpsLength = 9;
                domainName = rg.Match(longUrl).Result("${host}");
                var urlPart = longUrl.Substring(domainName.Length + httpsLength, longUrl.Length - domainName.Length - httpsLength);
                var url = new UrlEntity
                {
                    Long = longUrl,
                    Short = domainName + "/" + GenerateShort(urlPart)
                };

                dbContext.Urls.Add(url);
                dbContext.SaveChanges();

                return url.Short;
            }
            else
            {
                return string.Empty;
            }      
        }

        public string GenerateShort(string longUrl)
        {
            var pool = new[]
            {
                "q", "w", "e", "r", "t", "y", "u", "i", "o", "p", "a", "s", "d", "f", "g", "h", "j", "k", "l", "z", "x",
                "c", "v", "b", "n", "m", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0"
            };

            var shortUrl = new StringBuilder();
            var randomer = new Random();

            for (int i = 0; i < 5; i++)
            {
                shortUrl.Append(pool[randomer.Next(1, 35)]);
            }

            return shortUrl.ToString();
        }
    }
}
