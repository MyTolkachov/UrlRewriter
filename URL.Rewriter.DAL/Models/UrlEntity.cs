using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace URL.Rewriter.DAL.Models
{
    public class UrlEntity
    {
        public int Id { get; set; }
        public string Short { get; set; }
        public string Long { get; set; }
    }
}