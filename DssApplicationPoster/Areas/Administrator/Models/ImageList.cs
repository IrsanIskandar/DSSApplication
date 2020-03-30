using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DssApplicationPoster.Areas.Administrator.Models
{
    public class ImageList
    {
        public int NumberImage { get; set; }
        public string ImageName { get; set; }
        public string ImageSize { get; set; }
        public string ImagePath { get; set; }
        public string IsShow { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
