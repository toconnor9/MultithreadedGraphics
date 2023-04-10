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

        #endregion
    }
}
