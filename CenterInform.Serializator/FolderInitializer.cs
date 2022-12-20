using System.IO;

namespace CenterInform.Serializator
{
    public static class FolderInitializer
    {
        public static void Initialize(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
