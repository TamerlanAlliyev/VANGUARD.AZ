namespace Vanguard.Extensions;

public static class ImageFileExtension
{
    public static async Task<string> SaveToAsync(this IFormFile file, string folderPath)
    {
        if (file == null)
            throw new ArgumentNullException(nameof(file));

        if (string.IsNullOrEmpty(folderPath))
            throw new ArgumentNullException(nameof(folderPath));

        Directory.CreateDirectory(folderPath); 

        string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
        string filePath = Path.Combine(folderPath, uniqueFileName);

        using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }

        return uniqueFileName;
    }


    public static bool FileTypeAsync(this IFormFile file, string fileType)
    {
        if (file.ContentType.StartsWith(fileType))
        {
            return true;
        }
        return false;
    }


    public static bool FileTypeAsync(this IFormFile file, params string[] allowedFileTypes)
    {
        return allowedFileTypes.Any(fileType => file.ContentType.StartsWith(fileType));
    }


    public static bool FileSize(this IFormFile file, int size)
    {
        if (file.Length < size * 1024 * 1024)
        {
            return true;
        }
        return false;
    }

    public static void DeleteImagesService(string path, string fileName)
    {

        if (!string.IsNullOrEmpty(fileName))
        {
            var oldImagePath = Path.Combine(path, fileName);
            if (File.Exists(oldImagePath))
            {
                File.Delete(oldImagePath);
            }
        }
    }
}
