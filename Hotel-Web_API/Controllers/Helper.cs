using Hotel_Web_API.Models;

namespace Hotel_Web_API.Controllers
{
    public class Helper
    {
        public string Uploadfile(FileModel fileModel)
        {

            try
            {

                string path = Path.Combine(@"C:\Users\Win10\Desktop\aziz\hotel", fileModel.file.Name);
                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    fileModel.file.CopyTo(stream);
                }
                
                return fileModel.fileName;
            }
            catch (Exception ex)
            {
                return "error" + ex.Message;
            }

        }
    }
}
