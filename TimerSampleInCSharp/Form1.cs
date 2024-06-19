using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic;            

namespace TimerSampleInCSharp
{
    public partial class Form1 : Form
    {
        internal string fileName = "TestFile.txt";
        internal string fileName1 = "TestFile1.txt";

        public Form1()
        {
            InitializeComponent();
            try
            {
                if (!File.Exists(fileName))
                {
                    // Create() creates a file at pathName 
                    using (var fs = new FileStream(fileName, FileMode.Create))
                    using (var writer = new StreamWriter(fs))
                    {
                        string data = Interaction.InputBox("Enter the data", "Title", "Default", 0, 0);
                        writer.Write(data);
                        if (!File.Exists(fileName1))
                        {
                            using (var fs1 = new FileStream(fileName1, FileMode.Create))
                            using (var writer1 = new StreamWriter(fs1))
                            {
                                writer1.Write(data);
                            }
                        }
                    }
                }
            }
            catch (DirectoryNotFoundException)
            {
                // Nothing
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Timer t = new Timer
            {
                Interval = 15000
            };
            timer1.Enabled = true;
            timer1.Tick += new EventHandler(OnTimerEvent);
        }

        private void OnTimerEvent(object sender, EventArgs e)
        {
            try
            {
                string[] content = File.ReadAllLines(fileName);
                string[] content1 = File.ReadAllLines(fileName1);
                if(content!=null && content1!=null && content.Length != content1.Length)
                {
                    timer1.Enabled = false;
                    timer1.Stop();
                    MessageBox.Show("File data has been modified");
                }
            }
            catch (DirectoryNotFoundException)
            {
                // Nothing
            }
        }
    }
}
