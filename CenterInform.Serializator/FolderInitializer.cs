using System;
using System.IO;

namespace CenterInform.Serializator
{
    public static class FolderInitializer
    {
        public static void Initialize(string path)
        {
            if (path == null)
            {
                string workingDirectory = Environment.CurrentDirectory;

                path = Directory.GetParent(workingDirectory).Parent.Parent.FullName + "\\SerializationFolder";
            }

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
