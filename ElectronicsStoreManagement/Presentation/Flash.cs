using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElectronicsStore.Presentation
{
    public partial class Flash : Form
    {
        private int progressValue = 0;
        public Flash()
        {
            InitializeComponent();
            // Tạo và cấu hình Timer  1s = 1000ms, 3s = 3000ms
            timer.Interval = 1000; 
            timer.Tick += timer_Tick;

            // Cấu hình ProgressBar
            progressBar.Minimum = 0;
            progressBar.Maximum = 10;
            progressBar.Value = 0;
        }

        private void Flash_Load(object sender, EventArgs e)
        {
            timer.Interval = 3000; // Mở form trong 3 giây
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Interval = 1000; // Mở form trong 3 giây   
            timer.Start();

            progressValue++;
            progressBar.Value = progressValue;

            if (progressValue >= progressBar.Maximum)
            {
                timer.Stop();
                this.Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
