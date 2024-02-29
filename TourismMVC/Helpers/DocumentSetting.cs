namespace TourismMVC.Helpers
{
    public class DocumentSetting
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            //1. Get located folder path
            string folderpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName);

            //2.Get file name and make it unique 
            //file.Name : contain name of type like .pdf , .jpg
            //file.FileName: contain name of file 
            //Guid.NewGuid() : generate unique names
            string filename = file.FileName;

            //Get file path that i will save file come from IFormFile on it 
            string filePath = Path.Combine(folderpath, filename);

            //4. save file as streams :[data per time]
            using var filestream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(filestream);

            return filename;

        }

        public static void DeleteFile(string foldername, string filename)
        {
            string filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", foldername, filename);
            if (File.Exists(filepath))
                File.Delete(filepath);
        }
    }
}
