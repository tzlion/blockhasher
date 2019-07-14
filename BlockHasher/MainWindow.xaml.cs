using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Grid_Drop_1(object sender, DragEventArgs e)
        {
            bool balls = e.Data.GetDataPresent("FileName");
            string[] datum = (string[])e.Data.GetData("FileName");

            if (balls)
            {

                string hashstring = "Reading " + datum[0] + "\n";

                try
                {
                    int nobytes = int.Parse(blocksize.Text, System.Globalization.NumberStyles.HexNumber);

                    hashstring = hashstring + "BLOCK SIZE " + nobytes.ToString() + "\n";

                    byte[] filedata = File.ReadAllBytes(datum[0]);

                    int bc = 0;

                    byte[] curpart = new byte[nobytes];

                    foreach (byte abyte in filedata)
                    {
                       

                        if (bc < nobytes)
                        {
                          //  
                        }
                        else
                        { 
                            hashstring = hashstring + hashtostring(new MD5CryptoServiceProvider().ComputeHash(curpart)) + "\n";
                            curpart = new byte[nobytes];
                            bc = 0;
                        }


                        curpart[bc] = abyte;


                        bc++;
                    }

                    hashstring = hashstring + hashtostring(new MD5CryptoServiceProvider().ComputeHash(curpart)) + "\n";
                   

                }
                catch
                {
                    hashstring = "Didnt work";
                }

                hashlist.Text = hashlist.Text + hashstring + "\n";


            }

        }


        private static string hashtostring(byte[] computedHash)
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
