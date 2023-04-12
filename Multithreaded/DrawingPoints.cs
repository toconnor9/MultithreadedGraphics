using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Linq;


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

                // This keeps it from happening all at once
                // Thread.Sleep(60);

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
            int real_count = 0;
            List<MyPoint> myPoints = new List<MyPoint>();
            MyPoint new_pt = new MyPoint();

            
            while (count < parms.Max_Number_of_Points && real_count < (parms.Max_Number_of_Points * 1.2))
            {
                real_count++;

                x = rand.Next(0, parms.Picture_to_Draw_On.Width);
                y = rand.Next(0, parms.Picture_to_Draw_On.Height);
                curr_Cycle = rand.Next(0, parms.Cycles_to_Disappear);

                new_pt = new MyPoint(x, y, curr_Cycle);


                // If this new point isn't too close to any other points already in the list
                if (CheckIfTheresRoom(new_pt, myPoints, parms.Min_Distance_Apart))
                {
                    myPoints.Add(new_pt);
                
                    count++;
                }
            }

            return myPoints;
        }


        /// <summary>
        /// Check if the passed in point is too close to any other points in the list
        /// </summary>
        /// <param name="curr_pt"></param>
        /// <param name="list_of_pts"></param>
        /// <param name="min_dist"</param>
        /// <returns></returns>
        private bool CheckIfTheresRoom(MyPoint curr_pt, List<MyPoint> list_of_pts, double min_dist)
        {
            // Get a list of points within the minimum distance side to side and top to bottom
            List<MyPoint> nearbyPoints = list_of_pts.FindAll(p => Math.Abs(p.X - curr_pt.X) <= min_dist && Math.Abs(p.Y - curr_pt.Y) <= min_dist);
            bool room_to_breath = true;
            double dist;

            // Scan the sub-list, if there are any points in the grid 
            foreach (MyPoint p in nearbyPoints)
            {
                dist = p.DistanceFrom(curr_pt);
                
                if (dist < min_dist)
                {
                    Console.WriteLine($"{curr_pt} and {p} are too close {dist}");
                    room_to_breath = false;
                    break;
                }
            }

            return room_to_breath;
        }
    }
}
