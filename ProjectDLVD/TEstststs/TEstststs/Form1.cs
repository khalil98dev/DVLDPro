using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TEstststs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = "";
            string Extention = ""; 
            
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                
                path = openFileDialog1.FileName;
                Extention = Path.GetExtension(path);    
                string Distination = @"C:\test\" + Guid.NewGuid().ToString() + Extention;
                File.Copy(path, Distination, true );    
            


            }




        }
    }
}
