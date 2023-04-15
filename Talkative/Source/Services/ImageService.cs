using DocumentFormat.OpenXml.Vml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talkative.Source.Models;

namespace Talkative.Source.Services
{
    public class ImageService
    {

        public async Task<FileResult> OpenMediaPickerAsync()
        {
            try {

                var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title="Fotoğraf Seçin"

                });
                if (result.ContentType == "image/png" ||
                    result.ContentType == "image/jpeg" ||
                    result.ContentType == "image/jpg")
                {
                    
                    return result;
                    

                }
                else {
                     

                    return null;
                }
              

            
            }
            catch(Exception ex){ 
            return null;
            
            }
        
        
        }

        public async Task<Stream> FileResultToStream(FileResult fileResult)
        {
            if (fileResult == null)
                return null;

            return await fileResult.OpenReadAsync();
        }

        public Stream ByteArrayToStream(byte[] bytes)
        {
            return new MemoryStream(bytes);
        }

        public string ByteBase64ToString(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        public byte[] StringToByteBase64(string text)
        {
            return Convert.FromBase64String(text);
        }

        public async Task<ImageModel> Upload(FileResult fileResult)
        {
            byte[] bytes;

            try
            {
                using (var ms = new MemoryStream())
                {
                    var stream = await FileResultToStream(fileResult);
                    stream.CopyTo(ms);
                    bytes = ms.ToArray();
                }

                return new ImageModel
                {
                    byteBase64 = ByteBase64ToString(bytes),
                    contentType = fileResult.ContentType,
                    fileName = fileResult.FileName
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }





    }
}
