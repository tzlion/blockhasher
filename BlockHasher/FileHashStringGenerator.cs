using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BlockHasher
{
    public class FileHashStringGenerator
    {
        public static string BuildStringForFile(string filename, int blockSize)
        {
            string messageString = "File " + filename + "\r\n" + "Block size " + blockSize + "\r\n";
            
            try
            {
                byte[] fileData = File.ReadAllBytes(filename);
                var hashes = BuildHashList(fileData, blockSize);
                foreach (byte[] hash in hashes)
                {
                    messageString += HashToString(hash) + "\r\n";
                }
            }
            catch(Exception exception)
            {
                messageString += "An error occurred:\r\n" + exception.Message;
            }

            return messageString;
        }

        private static List<byte[]> BuildHashList(byte[] fileData, int blockSize)
        {
            var md5Provider = new MD5CryptoServiceProvider();
                
            int bytePosInBlock = 0;
            byte[] currentBlockData = new byte[blockSize];
            var hashes = new List<byte[]>();
            foreach (byte currentByte in fileData)
            {
                if (bytePosInBlock >= blockSize)
                {
                    hashes.Add(md5Provider.ComputeHash(currentBlockData));
                    currentBlockData = new byte[blockSize];
                    bytePosInBlock = 0;
                }
                currentBlockData[bytePosInBlock] = currentByte;
                bytePosInBlock++;
            }
            hashes.Add(md5Provider.ComputeHash(currentBlockData));
            return hashes;
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