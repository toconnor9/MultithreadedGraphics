using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;


namespace Multithreaded
{
    public class DrawingPoints
    {
        // This is how we pass parameters to the thread
        public DrawingPointsParams parms { get; set; }


        /// <summary>
        /// Run all the way through the image processor
        /// </summary>
        public void Draw()
        {
            Bitmap bmp = new Bitmap(parms.Picture_to_Draw_On.Width, parms.Picture_to_Draw_On.Height);
            Graphics g = Graphics.FromImage(bmp);
            Random rand = new Random();
            List<MyPoint> myPoints = InitializePoints(parms);
            Brush black_brush = new SolidBrush(Color.Black);
            Brush white_brush = new SolidBrush(Color.White);
            Font myFont = new Font("Arial", 14);
            int next_cycle, last_l = -100;


            g.Clear(Color.White);

            for (int curr_cycle = 0; curr_cycle < parms.Cycles_to_Disappear; curr_cycle++)
            {
                next_cycle = curr_cycle + 1;

                if (curr_cycle > 0)
                    g.DrawString($"Cycle {curr_cycle} of {parms.Cycles_to_Disappear}", myFont, white_brush, new PointF(1, 1));

                g.FillRectangle(white_brush, 0, 0, 200, 30);
                g.DrawString($"Cycle {next_cycle} of {parms.Cycles_to_Disappear}", myFont, black_brush, new PointF(1,1));

                // This keeps it from happening all at once
                Thread.Sleep(5);

                foreach (MyPoint curr_pt in myPoints)
                {
                    // If this point has run out, create a new one
                    if (curr_pt.Current_Cycle == 0)
                    {
                        curr_pt.X = rand.Next(0, parms.Width);
                        curr_pt.Y = rand.Next(0, parms.Height);
                        curr_pt.Current_Cycle = parms.Cycles_to_Disappear;
                    }

                    int brightness_level = 255 - (curr_pt.Current_Cycle % 256);

                    last_l = brightness_level;
                    Brush brsh = new SolidBrush(Color.FromArgb(255, (byte)brightness_level, (byte)brightness_level, (byte)brightness_level));
                    g.FillEllipse(brsh, curr_pt.X, curr_pt.Y, 2, 2);

                    // Countdown it's brightness
                    curr_pt.Current_Cycle--;
                }

                // Show the current results
                parms.Picture_to_Draw_On.Image = bmp;
            }
        }
        
        /// <summary>
        /// Put some points all over the bitmap area
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<MyPoint> InitializePoints(DrawingPointsParams parms)
        {
            Random rand = new Random();
            int x, y, curr_Cycle;
            int count = 0;
            List<MyPoint> myPoints = new List<MyPoint>();

            
            while (count < parms.Max_Number_of_Points)
            {
                x = rand.Next(0, parms.Picture_to_Draw_On.Width);
                y = rand.Next(0, parms.Picture_to_Draw_On.Height);
                curr_Cycle = rand.Next(0, parms.Cycles_to_Disappear);
                
                myPoints.Add(new MyPoint(x, y, curr_Cycle));

                count++;
            }

            return myPoints;
        }
    }
}
