using Azure.Core;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TravelNotesV2.Models;

namespace TravelNotesV2.Service
{
    public class ArticleSer
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public ArticleSer(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public void CheckFolderPhotos(string contentImages, int articleId, string type)
        {
            

        }
    }
}
