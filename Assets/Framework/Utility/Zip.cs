using ICSharpCode.SharpZipLib.Zip;
using System.Collections;
using System.IO;
using System;

/// 压缩类
public static class Zip
{

    /// 得到文件下的所有文件
    /// 文件夹路径
    private static ArrayList GetFileList(string directory)
    {
        ArrayList fileList = new ArrayList();
        bool isEmpty = true;
        foreach (string file in Directory.GetFiles(directory))
        {
            fileList.Add(file);
            isEmpty = false;
        }
        if (isEmpty)
        {
            if (Directory.GetDirectories(directory).Length == 0)
            {
                fileList.Add(directory + @"/");
            }
        }
        foreach (string dirs in Directory.GetDirectories(directory))
        {
            foreach (object obj in GetFileList(dirs))
            {
                fileList.Add(obj);
            }
        }
        return fileList;
    }

    /// <summary>
    /// 压缩文件
    /// </summary>
    /// <param name="fileToZip">需要压缩的文件列表</param>
    /// <param name="zipedFile">生成的zip文件路径</param>
    public static void ZipFile(string fileToZip, string zipedFile, int compressionLevel=9, int buffSize=2048)
    {
        if (!File.Exists(fileToZip))
        {
            throw new FileNotFoundException("The specified file " + fileToZip + " could not be found.");
        }

        using (ZipOutputStream zipStream = new ZipOutputStream(File.Create(zipedFile)))
        {
            string fileName = Path.GetFileName(fileToZip);
            ZipEntry zipEntry = new ZipEntry(fileName);
            zipStream.PutNextEntry(zipEntry);
            zipStream.SetLevel(compressionLevel);
            using (FileStream fileStream = new FileStream(fileToZip, FileMode.Open, FileAccess.Read))
            {
                byte[] buffer = new byte[buffSize];
                int size = fileStream.Read(buffer, 0, buffer.Length);
                zipStream.Write(buffer, 0, size);
                while (size < fileStream.Length)
                {
                    int sizeRead = fileStream.Read(buffer, 0, buffer.Length);
                    zipStream.Write(buffer, 0, sizeRead);
                    size += sizeRead;
                }
            }
        }
    }

    /// 压缩文件夹
    /// <summary>
    /// 压缩文件夹
    /// </summary>
    /// <param name="directoryToZip">需要压缩的文件架</param>
    /// <param name="zipedDirectory">生成的zip文件路径</param>
    public static void ZipDerctory(string directoryToZip, string zipedDirectory, string filter = "s", int compressionLevel = 9, int buffSize = 2048)
    {
        using (ZipOutputStream zipStream = new ZipOutputStream(File.Create(zipedDirectory)))
        {
            ArrayList fileList = GetFileList(directoryToZip);
            int directoryNameLength = (Directory.GetParent(directoryToZip)).ToString().Length + 1;
            byte[] buffer = new byte[buffSize];

            zipStream.SetLevel(compressionLevel);
            ZipEntry zipEntry = null;
            foreach (string fileName in fileList)
            {
                string path = fileName.Replace("\\", "/");
                string ex = Path.GetExtension(fileName);
                if (ex != string.Empty && filter.IndexOf(ex) != -1)
                    continue;

                zipEntry = new ZipEntry(path.Remove(0, directoryNameLength).Replace("\\", "/"));
                zipStream.PutNextEntry(zipEntry);
                if (!path.EndsWith(@"/"))
                {
                    using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        int size = fileStream.Read(buffer, 0, buffer.Length);
                        zipStream.Write(buffer, 0, size);
                        while (size < fileStream.Length)
                        {
                            int sizeRead = fileStream.Read(buffer, 0, buffer.Length);
                            zipStream.Write(buffer, 0, sizeRead);
                            size += sizeRead;
                        }
                    }
                }
            }
        }
    }

    /// 压缩文件列表
    /// 要压缩的文件夹路径
    /// 压缩或的文件夹路径
    public static void ZipFiles(string[] filesToZip, string sourceRoot, string zipedDirectory, string filter = "s", int compressionLevel = 9, int buffSize = 2048)
    {
        using (ZipOutputStream zipStream = new ZipOutputStream(File.Create(zipedDirectory)))
        {
            for (int i = 0; i < filesToZip.Length; i++)
            {
                string fileName = filesToZip[i].Replace(sourceRoot, string.Empty);
                ZipEntry zipEntry = new ZipEntry(fileName);
                zipStream.PutNextEntry(zipEntry);
                zipStream.SetLevel(compressionLevel);
                using (FileStream fileStream = new FileStream(filesToZip[i], FileMode.Open, FileAccess.Read))
                {
                    byte[] buffer = new byte[buffSize];
                    int size = fileStream.Read(buffer, 0, buffer.Length);
                    zipStream.Write(buffer, 0, size);
                    while (size < fileStream.Length)
                    {
                        int sizeRead = fileStream.Read(buffer, 0, buffer.Length);
                        zipStream.Write(buffer, 0, sizeRead);
                        size += sizeRead;
                    }
                }
            }
        }
    }

    /// 解压缩文件
    /// 压缩文件路径
    /// 解压缩文件路径
    public static void UnZipFile(string zipFilePath, string unZipFilePatah, int buffSize=2048)
    {
        using (ZipInputStream zipStream = new ZipInputStream(File.OpenRead(zipFilePath)))
        {
            byte[] buffer = new byte[buffSize];
            ZipEntry zipEntry = null;
            while ((zipEntry = zipStream.GetNextEntry()) != null)
            {
                string fileName = Path.GetFileName(zipEntry.Name);
                if (!string.IsNullOrEmpty(fileName))
                {
                    if (zipEntry.CompressedSize == 0)
                        break;

                    using (FileStream stream = File.Create(unZipFilePatah + fileName))
                    {
                        while (true)
                        {
                            int size = zipStream.Read(buffer, 0, buffer.Length);
                            if (size > 0)
                                stream.Write(buffer, 0, size);
                            else
                                break;
                        }
                    }
                }
            }
        }
    }

    /// 解压缩目录
    /// 压缩目录路径
    /// 解压缩目录路径
    public static void UnZipDirectory(Stream fileStream, string unZipDirecotyPath, Action<float> action, int buffSize = 2048)
    {
        UnityEngine.Debug.Log(fileStream.Length);
        using (ZipInputStream zipStream = new ZipInputStream(fileStream))
        {
            byte[] buffer = new byte[buffSize];
            ZipEntry zipEntry = null;
            while ((zipEntry = zipStream.GetNextEntry()) != null)
            {
                string entryName = zipEntry.Name.Replace("\\", "/");
                string directoryName = Path.GetDirectoryName(entryName);
                string fileName = Path.GetFileName(entryName);
                if (!string.IsNullOrEmpty(fileName))
                {
                    if (zipEntry.CompressedSize == 0)
                        break;

                    string savePath = unZipDirecotyPath + entryName;
                    directoryName = Path.GetDirectoryName(savePath);
                    if (!Directory.Exists(directoryName))
                        Directory.CreateDirectory(directoryName);

                    using (FileStream stream = File.Create(savePath))
                    {
                        while (true)
                        {
                            int size = zipStream.Read(buffer, 0, buffer.Length);
                            if (size > 0)
                                stream.Write(buffer, 0, size);
                            else
                                break;
                        }
                    }
                }

                float progress = (float)fileStream.Position / fileStream.Length;
                action(progress);
            }
        }
    }

    /// 解压缩目录
    /// 压缩目录路径
    /// 解压缩目录路径
    public static void UnZipDirectory(string zipDirectoryPath, string unZipDirecotyPath, Action<float> action, int buffSize=2048)
    {
        UnZipDirectory(File.OpenRead(zipDirectoryPath), unZipDirecotyPath, action, buffSize);
    }
}