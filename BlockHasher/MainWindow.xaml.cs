using System;
using System.Windows;

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
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                return;
            }
            
            string[] filenames = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (filenames == null || filenames.Length < 1)
            {
                return;
            }

            int blockSize;
            
            try
            {
                blockSize = int.Parse(blocksize.Text, System.Globalization.NumberStyles.HexNumber);
            }
            catch
            {
                hashlist.Text += "Invalid block size" + "\r\n";
                hashlist.ScrollToEnd();
                return;
            }
            
            foreach(string filename in filenames)
            {
                var messageString = FileHashStringGenerator.BuildStringForFile(filename, blockSize);
                hashlist.Text += messageString + "\r\n";
                hashlist.ScrollToEnd();
            }
        }
    }
}
