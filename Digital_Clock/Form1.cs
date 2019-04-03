using Digital_Clock.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Digital_Clock
{
    public partial class Form1 : Form
    {
        int totalSeconds;

        bool isPaused = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        

        private void tmrCountDown_Tick(object sender, EventArgs e)
        {
            totalSeconds--;
            lblCountDown.Text = "00:" + (totalSeconds / 60).ToString("00") + ":" + (totalSeconds % 60).ToString("00");
            if (totalSeconds <= 0)
            {
                tmrCountDown.Enabled = false;
                MessageBox.Show("Time to get back to Work");                
                btnReset.PerformClick();
            }
        }

        private void btnBreak_Click(object sender, EventArgs e)
        {
            CountDown(900, "00:15:00");
        }

        private void btnLunch_Click(object sender, EventArgs e)
        {
            CountDown(3600, "01:00:00");
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (!isPaused)
            {
                tmrCountDown.Enabled = false;
                btnPause.Text = "Start";
                isPaused = true;
            }
            else
            {
                tmrCountDown.Enabled = true;
                btnPause.Text = "Pause";
                isPaused = false;
            }            
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            tmrCountDown.Enabled = false;
            isPaused = false;
            btnPause.Text = "Pause";
            lblCountDown.Text = "00:00:00";
        }

        // Method to initiate the countdown
        private void CountDown(int seconds, string startTime)
        {
            totalSeconds = seconds;
            lblCountDown.Text = startTime;
            tmrCountDown.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = Properties.Settings.Default.F1State;
            this.Location = Properties.Settings.Default.F1Location;
            this.Size = Properties.Settings.Default.F1Size;
        }

        private void Form1_Closing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.F1State = this.WindowState;
            if (this.WindowState == FormWindowState.Normal)
            {
                Settings.Default.F1Location = this.Location;
                Settings.Default.F1Size = this.Size;
            }
            else
            {
                // ISSUE: variable of a compiler-generated type
                Settings settings1 = Settings.Default;
                Rectangle restoreBounds = this.RestoreBounds;
                Point location = restoreBounds.Location;
                settings1.F1Location = location;
                // ISSUE: variable of a compiler-generated type
                Settings settings2 = Settings.Default;
                restoreBounds = this.RestoreBounds;
                Size size = restoreBounds.Size;
                settings2.F1Size = size;
            }
            Settings.Default.Save();
        }
    }
}
