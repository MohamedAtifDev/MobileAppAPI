

namespace MobileApp.BL.services
;
    public class fileUploader
    {
        
        public static string upload(string foldername,IFormFile file)
        {
            string path = Directory.GetCurrentDirectory() + "/"+foldername;
            string filename=Guid.NewGuid()+Path.GetFileName(file.FileName);
            string finalpath=Path.Combine(path,filename);
            using (var stream = new FileStream(finalpath,FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return filename;
        }

        public static void delete(string filename, string foldername)
        {
            if (File.Exists(Directory.GetCurrentDirectory() + "/wwwroot/" + foldername + "/" + filename))
            {
                File.Delete(Directory.GetCurrentDirectory() + "/wwwroot/" + foldername + "/" + filename);
            }

        }
    }

