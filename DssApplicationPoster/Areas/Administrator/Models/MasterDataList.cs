using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DssApplicationPoster.Areas.Administrator.Models
{
    public class MasterDataList
    {
        public List<ImageList> GetImageLists { get; set; }
        public List<VideoList> GetVideoLists { get; set; }
        public List<TextRunning> GetTextRunnings { get; set; }
    }
}
