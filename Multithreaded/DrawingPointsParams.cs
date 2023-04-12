﻿using System.Windows.Forms;

namespace Multithreaded
{
    public class DrawingPointsParams
    {
        #region Properties
        
        public int Max_Number_of_Points { get; set; }
        public int Cycles_to_Disappear { get; set; }
        public PictureBox Picture_to_Draw_On { get; set; }
        public int Width { get { return Picture_to_Draw_On.Width; } }
        public int Height { get { return Picture_to_Draw_On.Height; } }
        public double Min_Distance_Apart { get; set; }

        #endregion


        #region Constructors

        public DrawingPointsParams(int max_Number_of_Points,
                                   int cycles_to_Disappear,
                                   PictureBox picture_to_Draw_On)
        {
            Max_Number_of_Points = max_Number_of_Points;
            Cycles_to_Disappear = cycles_to_Disappear;
            Picture_to_Draw_On = picture_to_Draw_On;
        }

        public DrawingPointsParams(int max_Number_of_Points,
                                   int cycles_to_Disappear,
                                   PictureBox picture_to_Draw_On,
                                   double min_dist_apart)
        {
            Max_Number_of_Points = max_Number_of_Points;
            Cycles_to_Disappear = cycles_to_Disappear;
            Picture_to_Draw_On = picture_to_Draw_On;
            Min_Distance_Apart = min_dist_apart;
        }

        public DrawingPointsParams() : this(100, 100, new PictureBox(), 3) { }

        #endregion
    }
}
