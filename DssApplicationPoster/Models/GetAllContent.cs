using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DssApplicationPoster.Models
{
    public class GetAllContent
    {
        public List<ImageContent> ListImageContent { get; set; }
        public ImageContent GetImageContent { get; set; }
        public VideoContent GetVideoContent { get; set; }
        public TextRunning GetTextRun { get; set; }
    }
}
