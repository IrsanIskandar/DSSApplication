using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DssApplicationPoster.Areas.Administrator.Models
{
    public class VideoList
    {
        public int NumberVideo { get; set; }
        public string VideoName { get; set; }
        public string VideoSize { get; set; }
        public string VideoPath { get; set; }
        public string IsShow { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
