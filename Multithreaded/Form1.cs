using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Multithreaded
{
    public partial class Form1 : Form
    {
        Thread t;
        DrawingPoints drwPts = new DrawingPoints();


        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            int cycles, points;


            if (!int.TryParse(txtNumCycles.Text, out cycles))
                cycles = 250;

            if (!int.TryParse(txtNumPoints.Text, out points))
                points = 250;

            drwPts.parms = new DrawingPointsParams(points, cycles, picMain);

            // Use this if you just want to run the Draw method in the current thread
            // drwPts.Draw();

            t = new Thread(drwPts.Draw);

            // start the thread
            t.Start();
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            t.Abort();
        }
    }
}
