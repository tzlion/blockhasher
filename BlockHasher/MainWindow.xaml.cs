using System;
using System.Text;
using System.Windows;
using System.IO;
using System.Security.Cryptography;

namespace BlockHasher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ProcessDroppedFiles(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("FileName"))
            {
                return;
            }
            
            string[] filenames = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach(string filename in filenames)
            {
                processSingleFile(filename);
            }
        }

        private void processSingleFile(string filename)
        {
            string messageString = "";
            
            try
            {
                messageString += "File " + filename + "\r\n";
                
                int blockSize = int.Parse(blocksize.Text, System.Globalization.NumberStyles.HexNumber);
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
                messageString = "An error occurred:\r\n" + exception.Message;
            }

            hashlist.Text += messageString + "\r\n";
            hashlist.ScrollToEnd();
        }
        
        private static string HashToString(byte[] computedHash)
        {
            var sBuilder = new StringBuilder();
            foreach (byte b in computedHash)
            {
                sBuilder.Append(b.ToString("x2").ToLower());
            }
            return sBuilder.ToString();
        }
    }
}
