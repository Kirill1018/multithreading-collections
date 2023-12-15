using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Multithreading_collections
{
    public partial class Form1 : Form
    {
        public static BlockingCollection<string> collection = new BlockingCollection<string>();
        public static string record;
        public Form1()
        {
            InitializeComponent();
            File.Create("synchronization.txt");
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            record = $"{Cursor.Position.X.ToString()}; {Cursor.Position.Y.ToString()}; {new DateTime().ToString()}          ";
            collection.Add(record);
            Thread thread = new Thread(Unload);
            thread.Start();
        }
        public static void Unload()
        {
            string read = collection.Take();
            using (StreamWriter writer = new StreamWriter(Path.Combine("", "synchronization.txt"))) writer.WriteLine(read);
        }
    }
}