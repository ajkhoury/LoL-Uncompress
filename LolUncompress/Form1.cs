using System;
using System.Windows.Forms;
using ComponentAce.Compression.Libs.zlib;

namespace LolUncompress
{
    public partial class Form1 : Form
    {
  
        public Form1()
        {
            InitializeComponent();
        }

        private static void UncompressFile(string inFile, string outFile)
        {
            // Do it slow way because calling CopyStream sometimes causes an inflated exception on LoL comrpessed files
            int stopByte = -1;
            System.IO.FileStream outFileStream = new System.IO.FileStream(outFile, System.IO.FileMode.Create);
            ZInputStream inZStream = new ZInputStream(System.IO.File.Open(inFile, System.IO.FileMode.Open, System.IO.FileAccess.Read));
            int data;
            while (stopByte != (data = inZStream.Read())) {
                byte _dataByte = (byte)data;
                outFileStream.WriteByte(_dataByte);
            }
            inZStream.Close();
            outFileStream.Close();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            openFileDialog1.Title = "Select a file to decompress";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                saveFileDialog1.Title = "Save decompressed file to";
                saveFileDialog1.FileName = openFileDialog1.FileName + ".decompressed";
                if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    UncompressFile(openFileDialog1.FileName, saveFileDialog1.FileName);
                }
            }
        }
    }
}
