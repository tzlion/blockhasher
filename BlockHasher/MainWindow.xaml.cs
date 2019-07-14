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

        private void Grid_Drop_1(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("FileName"))
            {
                return;
            }

            string messageString = "";
            
            try
            {
                string[] filenames = (string[])e.Data.GetData("FileName");
                messageString += "Reading " + filenames[0] + "\n";
                
                int blockSize = int.Parse(blocksize.Text, System.Globalization.NumberStyles.HexNumber);
                messageString += "BLOCK SIZE " + blockSize.ToString() + "\n";
                
                byte[] filedata = File.ReadAllBytes(filenames[0]);
                
                var md5Provider = new MD5CryptoServiceProvider();
                
                int bc = 0;
                byte[] curpart = new byte[blockSize];
                
                foreach (byte abyte in filedata)
                {
                    if (bc >= blockSize)
                    {
                        messageString += HashToString(md5Provider.ComputeHash(curpart)) + "\n";
                        curpart = new byte[blockSize];
                        bc = 0;
                    }
                    curpart[bc] = abyte;
                    bc++;
                }
                
                messageString += HashToString(md5Provider.ComputeHash(curpart)) + "\n";
            }
            catch
            {
                messageString = "Didn't work";
            }

            hashlist.Text += messageString + "\n";
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
