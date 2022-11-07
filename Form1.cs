using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace WinFormsApp1
{
    public partial class Form1 : Form

    {
        Bitmap background;
        Bitmap snow;
        private readonly IList<Snowflake> snowflakes;


        int speed = 5;
        int n = 0;
        private readonly Timer timer;
        public Form1()
        {
            InitializeComponent();
            snowflakes = new List<Snowflake>();
            background = new Bitmap(Properties.Resources.fon);
            snow = new Bitmap(Properties.Resources.snow__1_);
            timer = new Timer();
            timer.Interval = 1;
            timer.Tick += Timer_Tick;
            AddSnowFlakes();

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            Dr();
            foreach (var SnowFlake in snowflakes)
            {
                SnowFlake.Y += SnowFlake.size;

                if (SnowFlake.Y > Screen.PrimaryScreen.WorkingArea.Height)
                {
                    SnowFlake.Y = -SnowFlake.size;
                }

            }
            timer.Start();
            
        }

        private void AddSnowFlakes()
        {
            var rnd = new Random();
            for (int i = 0; i < 100; i++)
            {
                snowflakes.Add(new Snowflake
                {
                    X = rnd.Next(Screen.PrimaryScreen.WorkingArea.Width),
                    Y = rnd.Next(Screen.PrimaryScreen.WorkingArea.Height),
                    size = rnd.Next(20, 40)
                });
            }
        }
       

        private void Form1_Paint(object sender, PaintEventArgs e)
        {


            Dr();

        }
        private void Dr()
        {
            var scene = new Bitmap(background,
                ClientRectangle.Width,
                ClientRectangle.Height);
            for (var i = 0; i < snowflakes.Count; i++)
            {

                if (snowflakes[i].Y > Screen.PrimaryScreen.WorkingArea.Height)
                {
                    snowflakes[i].Y = -snowflakes[i].size;
                }
                else
                {
                    snowflakes[i].Y += speed + snowflakes[i].size;
                }
            }
        
            for (var i = 0; i < snowflakes.Count; i++)
            {

             
              
                for ( i = 0; i < snowflakes.Count; i++)
                {
                    if (snowflakes[i].Y > 0)
                    {
                        var gr = Graphics.FromImage(scene);
                        gr.DrawImage(snow,
                            new Rectangle(
                            snowflakes[i].X,
                            snowflakes[i].Y,
                            snowflakes[i].size,
                            snowflakes[i].size));
                    }
                }
                var g = this.CreateGraphics();
                g.DrawImage(scene, 0, 0);
            }

        }

        private void Form1_Click(object sender, EventArgs e)
        {
            if (n == 0)
            {
                timer.Start();
                n++;
            }
            else if (n == 1)
            {
                timer.Stop();
                n = 0;
            }
        }
    } }

