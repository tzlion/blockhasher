using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BlockHasher
{
    public class FileHashStringGenerator
    {
        public static string BuildStringForFile(string filename, int blockSize)
        {
            string messageString = "";
            
            try
            {
                messageString += "File " + filename + "\r\n";
                
                messageString += "Block size " + blockSize + "\r\n";
                
                byte[] filedata = File.ReadAllBytes(filename);
                
                var md5Provider = new MD5CryptoServiceProvider();
                
                int bc = 0;
                byte[] curpart = new byte[blockSize];
                
                foreach (byte abyte in filedata)
                {
                    if (bc >= blockSize)
                    {
                        messageString += HashToString(md5Provider.ComputeHash(curpart)) + "\r\n";
                        curpart = new byte[blockSize];
                        bc = 0;
                    }
                    curpart[bc] = abyte;
                    bc++;
                }
                
                messageString += HashToString(md5Provider.ComputeHash(curpart)) + "\r\n";
            }
            catch(Exception exception)
            {
                messageString += "An error occurred:\r\n" + exception.Message;
            }

            return messageString;
        }

        private static string HashToString(byte[] computedHash)
        {
            var stringBuilder = new StringBuilder();
            foreach (byte b in computedHash)
            {
                stringBuilder.Append(b.ToString("x2").ToLower());
            }
            return stringBuilder.ToString();
        }

    }
}