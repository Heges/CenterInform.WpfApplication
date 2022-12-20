using System;
using System.IO;

namespace CenterInform.Serializator
{
    public static class FolderInitializer
    {
        public static string pathFolder;
        public static void Initialize(string path)
        {
            if (path == null)
            {
                string workingDirectory = Environment.CurrentDirectory;

                pathFolder = Directory.GetParent(workingDirectory).Parent.Parent.FullName + "\\SerializationFolder";
            }

            if (!Directory.Exists(pathFolder))
            {
                Directory.CreateDirectory(pathFolder);
            }
        }
    }
}
