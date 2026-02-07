namespace crud_v2.Helper
{
    public class Upload
    {
        // Method to upload file
        public static string UploadFile(string FolderName, IFormFile File)
        {
            try
            {
                // 1) Get Directory
                // Combine current directory with wwwroot/Files folder and the specified folder name
                string FolderPath = Directory.GetCurrentDirectory() + "/wwwroot/Files/" + FolderName;

                // 2) Get File Name
                // Create unique filename using GUID (36 characters) + original filename
                string FileName = Guid.NewGuid() + Path.GetFileName(File.FileName);
                // Guid => Word contain from 36 character

                // 3) Merge Path with File Name
                // Combine folder path with the unique filename
                string FinalPath = Path.Combine(FolderPath, FileName);
                // combine put /

                // 4) Save File As Streams "Data Overtime"
                using (var Stream = new FileStream(FinalPath, FileMode.Create))
                {
                    File.CopyTo(Stream);
                }

                return FileName;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // Method to remove/delete file
        public static string RemoveFile(string FolderName, string fileName)
        {
            try
            {
                var directory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files", FolderName, fileName);

                if (File.Exists(directory))
                {
                    File.Delete(directory);
                    return "File Deleted";
                }

                return "File Not Deleted";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
