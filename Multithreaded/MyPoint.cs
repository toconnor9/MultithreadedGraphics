using System;

namespace Multithreaded
{
    public class MyPoint
    {
        #region Properties
        
        public int X { get; set; }
        public int Y { get; set; }
        public int Current_Cycle { get; set; }

        #endregion


        #region Constructors

        public MyPoint() : this(0, 0, 0) { }

        public MyPoint(int x, int y, int current_Cycles)
        {
            X = x;
            Y = y;
            Current_Cycle = current_Cycles;
        }

        #endregion


        #region Methods

        public override string ToString()
        {
            return $"({X}, {Y}) - {Current_Cycle}";
        }

        public double DistanceFrom(MyPoint other_pt)
        {
            return Math.Sqrt(((other_pt.X - X) * (other_pt.X - X)) + ((other_pt.Y - Y) * (other_pt.Y - Y)));
        }

        public bool IsTooCloseTo(MyPoint other_pt)
        {
            double dist_from = DistanceFrom(other_pt);

            if (dist_from < 3)
                return true;

            return false;
        }

        public bool IsTooCloseTo(MyPoint other_pt, double min_dist)
        {
            double dist_from = DistanceFrom(other_pt);

            if (dist_from < min_dist)
                return true;

            return false;
        }

        #endregion
    }
}
