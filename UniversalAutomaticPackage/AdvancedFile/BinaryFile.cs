using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UniversalAutomaticPackage.AdvancedFile
{
    public class BinaryFile
    {
        FileInfo fileInfo;
        public BinaryFile(string file)
        {
            fileInfo = new FileInfo(file);
        }
        public bool StartedWith(string bytes,Encoding encoding)
        {
            var target=encoding.GetBytes(bytes);
            var fs=fileInfo.OpenRead();
            if (fileInfo.Length < target.Length)
            {
                return false;
            }
            for (int i = 0; i < target.Length; i++)
            {
                if (fs.ReadByte() != target[i])
                {
                    return false;   
                }
            }
            fs.Close();
            return true;
        }
        public bool StartedWith(byte[] bytes)
        {
            var fs = fileInfo.OpenRead();
            if (fileInfo.Length < bytes.Length)
            {
                return false;
            }
            for (int i = 0; i < bytes.Length; i++)
            {
                if (fs.ReadByte() != bytes[i])
                {
                    return false;
                }
            }
            fs.Close();
            return true;
        }
    }
}
