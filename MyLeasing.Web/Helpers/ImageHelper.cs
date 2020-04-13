using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MyLeasing.Web.Helpers
{
    public class ImageHelper : IImageHelper
    {
        public async Task<string> UploadImageAsync(IFormFile imageFile)
        {
            var guid = Guid.NewGuid().ToString(); //Guid son caracteres aleatorios para que no se repita el mismo nombre
            var file = $"{guid}.jpg";
            var path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot\\images\\Properties",//Carpeta donde se guardan las imagenes 
                file);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return $"~/images/Properties/{file}";
        }
    }
}
