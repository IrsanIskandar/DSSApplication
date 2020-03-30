using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DssApplicationPoster.Areas.Administrator.Models
{
    public class UploadImages
    {
        public int ID { get; set; }
        public string FileName { get; set; }
        public string FileSize { get; set; }
        public string FilePath { get; set; }
        public int IsShow { get; set; }

    }
}
