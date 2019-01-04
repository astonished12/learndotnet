using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventApp.Services.ImageStorageService
{
    public interface IImageStorageService
    {
        Task<string> StoreImage(string filename, byte[] array);
        Task<List<string>> GetImages();
    }
}
