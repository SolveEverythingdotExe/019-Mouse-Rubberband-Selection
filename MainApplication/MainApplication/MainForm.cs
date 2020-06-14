using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainApplication
{
    public partial class MainForm : Form
    {
        //lets declare variable
        Rectangle rectangle = new Rectangle();
        Point originalLocation;

        public MainForm()
        {
            InitializeComponent();

            //lets remove flickering
            this.DoubleBuffered = true;
        }

        //now lets paint the rectangle
        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            //lets use using keyword so that objects will automatically disposed after use
            if (!rectangle.IsEmpty)
                using (SolidBrush brush = new SolidBrush(Color.Gray))
                using (Pen pen = new Pen(brush))
                    e.Graphics.DrawRectangle(pen, rectangle);
        }
        //It works

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            //lets set the location of the rectangle, if left click is clicked
            if (e.Button == MouseButtons.Left)
            {
                rectangle.Location = new Point(e.X, e.Y);
                originalLocation = new Point(e.X, e.Y);

                //then repaint the form
                this.Invalidate();
            }
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            //lets set the size of the rectangle, if mouse is dragged
            if (e.Button == MouseButtons.Left)
            {
                if (e.X > rectangle.X)
                    rectangle.Size = new Size(e.X - rectangle.X, e.Y - rectangle.Y);
                else
                {
                    //ENCHANCEMENT =========> handle also upward movement of mouse
                    rectangle.Size = new Size(originalLocation.X - e.X, originalLocation.Y - e.Y);
                    rectangle.Location = new Point(e.X, e.Y);
                }

                //then repaint the form
                this.Invalidate();
            }
        }

        //lets reset the rectangle
        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            rectangle.Size = new Size(0, 0);
            rectangle.Location = new Point(0, 0);
            this.Invalidate();
        }
    }
}
