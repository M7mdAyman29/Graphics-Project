using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class Form1 : Form
    {
        int inside = 0;
        int top = 8;
        int bottom = 4;
        int right = 2;
        int left = 1;
        bool t = false;
        bool s = false;
        bool ro = false;
        Color c = Color.Black;
        Bitmap b;
        public Form1()
        {
            InitializeComponent();
        }
        int Round(double a) { return (int)(a + 0.5); }



        //DDA
        public void DDA_Line(int x1 ,int x2 , int y1 ,int  y2)
        {
            if (ro)
            {
                double angle = double.Parse(Angle.Text) * 0.01745;
                int x4 = (int)(x2 - ((x2 - x1) * Math.Cos(angle) - (y2 - y1) * Math.Sin(angle)));
                int y4 = (int)(y2 - ((x2 - x1) * Math.Sin(angle) + (y2 - y1) * Math.Cos(angle)));
                x1 = x2;
                y1 = y2;
                x2 = x4;
                y2 = y4; 
            }
             else if(t)
            {
                x1 += int.Parse(tx.Text);
                y1 += int.Parse(ty.Text);
                x2 += int.Parse(tx.Text);
                y2 += int.Parse(ty.Text);
            } 
            else if(s)
            {
                x1 *= int.Parse(tx.Text);
                y1 *= int.Parse(ty.Text);
                x2 *= int.Parse(tx.Text);
                y2 *= int.Parse(ty.Text);
            }

            b = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            double xinc, yinc, x = x1, y = y1;
            double dx = x2-x1, dy = y2-y1;
            double steps = Math.Abs(dx) > Math.Abs(dy) ? Math.Abs(dx) : Math.Abs(dy);
            xinc = dx / steps;
            yinc = dy / steps;
            for (int i = 0; i <= steps; i++)
            {
                DataGridViewRow r = new DataGridViewRow();
                r.CreateCells(dgv);
                r.Cells[0].Value = i;
                r.Cells[1].Value = x;
                r.Cells[2].Value = y;
                r.Cells[3].Value = Round(x);
 
                b.SetPixel(Round(x), Round(y) , c);

                r.Cells[4].Value = Round(y);
                x += xinc;
                y += yinc;
                dgv.Rows.Add(r);

            }
            pictureBox1.Image = b;
        }
        public void Bres_Line_Draw(int x1, int x2, int y1, int y2)
        {
            b = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            if (ro)
            {
                double angle = double.Parse(Angle.Text) * 0.01745;
                int x4 = (int)(x2 - ((x2 - x1) * Math.Cos(angle) - (y2 - y1) * Math.Sin(angle)));
                int y4 = (int)(y2 - ((x2 - x1) * Math.Sin(angle) + (y2 - y1) * Math.Cos(angle)));
                x1 = x2;
                y1 = y2;
                x2 = x4;
                y2 = y4;
            }
            else if (t)
            {
                x1 += int.Parse(tx.Text);
                y1 += int.Parse(ty.Text);
                x2 += int.Parse(tx.Text);
                y2 += int.Parse(ty.Text);
            }
            else if (s)
            {
                x1 *= int.Parse(tx.Text);
                y1 *= int.Parse(ty.Text);
                x2 *= int.Parse(tx.Text);
                y2 *= int.Parse(ty.Text);
            }
            int x = x1, y = y1;
            int dx = Math.Abs((int)x2 - (int)x1), dy = Math.Abs((int)y2 - (int)y1);
            bool d = false;
            if (dy > dx)
            {
                int t = dx;
                dx = dy;
                dy = t;
                d = true;
            }
            int p = 2 * dy - dx;
            int incerE = 2 * dy;
            int IncerNE = 2 * (dy - dx);
            b.SetPixel(x, y, c);
            for (int i = 0; i < dx; i++)
            {
                DataGridViewRow r = new DataGridViewRow();
                r.CreateCells(dgv);
                r.Cells[0].Value = i;
                r.Cells[1].Value = x;
                r.Cells[2].Value = y;
                r.Cells[3].Value = p;
                if (d)
                {
                    y++;
                    if (p < 0)

                        p += incerE;
                    else
                    {
                        p += IncerNE;
                        x++;
                    }

                }
                else
                {
                    x++;
                    if (p < 0)

                        p += incerE;
                    else
                    {
                        p += IncerNE;
                        y++;
                    }
                }
                r.Cells[4].Value = x;
                r.Cells[5].Value = y;
                dgv.Rows.Add(r);
                b.SetPixel(x, y, c);
            }
            pictureBox1.Image = b;
        }


        //Circle 
        void Draw_Circle(int r, int xc, int yc)
        {
           int x = 0, y = r, p = 1 - r, i = 0;
            b = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            circlePlotPoint(xc, yc, x, y);

            while (x < y)
            {
                DataGridViewRow D = new DataGridViewRow();
                D.CreateCells(dgv);
                D.Cells[0].Value = i;
                D.Cells[1].Value = p;

                x++;
                if (p < 0)
                {
                    p += 2 * x - 1;
                }
                else
                {
                    y--;
                    p += 2 * (x - y) - 1;
                }
                circlePlotPoint(xc, yc, x, y);
                D.Cells[2].Value = x;
                D.Cells[3].Value = y;
                D.Cells[4].Value = 2 * x;
                D.Cells[5].Value = 2 * y;
                dgv.Rows.Add(D);
                i++;
            }
            pictureBox1.Image = b;
        }
        void circlePlotPoint(int xc, int yc, int x, int y)
        {
            if (t)
            {
            
                b.SetPixel(xc + x + int.Parse(tx.Text), yc + y + int.Parse(ty.Text), c);
                b.SetPixel(xc - x + int.Parse(tx.Text), yc - y + int.Parse(ty.Text), c);
                b.SetPixel(xc - x + int.Parse(tx.Text), yc + y + int.Parse(ty.Text), c);
                b.SetPixel(xc + x + int.Parse(tx.Text), yc - y + int.Parse(ty.Text), c);
                b.SetPixel(xc + y + int.Parse(tx.Text), yc + x + int.Parse(ty.Text), c);
                b.SetPixel(xc + y + int.Parse(tx.Text), yc - x + int.Parse(ty.Text), c);
                b.SetPixel(xc - y + int.Parse(tx.Text), yc + x + int.Parse(ty.Text), c);
                b.SetPixel(xc - y + int.Parse(tx.Text), yc - x + int.Parse(ty.Text), c);
            }
            if (s)
            {
                b.SetPixel(xc + x * int.Parse(tx.Text), yc + y * int.Parse(ty.Text), c);
                b.SetPixel(xc - x * int.Parse(tx.Text), yc - y * int.Parse(ty.Text), c);
                b.SetPixel(xc - x * int.Parse(tx.Text), yc + y * int.Parse(ty.Text), c);
                b.SetPixel(xc + x * int.Parse(tx.Text), yc - y * int.Parse(ty.Text), c);
                b.SetPixel(xc + y * int.Parse(tx.Text), yc + x * int.Parse(ty.Text), c);
                b.SetPixel(xc + y * int.Parse(tx.Text), yc - x * int.Parse(ty.Text), c);
                b.SetPixel(xc - y * int.Parse(tx.Text), yc + x * int.Parse(ty.Text), c);
                b.SetPixel(xc - y * int.Parse(tx.Text), yc - x * int.Parse(ty.Text), c);
            }
            else
            {
                b.SetPixel(xc + x, yc + y, c);
                b.SetPixel(xc + x, yc - y, c);
                b.SetPixel(xc - x, yc + y, c);
                b.SetPixel(xc - x, yc - y, c); 
                b.SetPixel(xc + y, yc + x, c);
                b.SetPixel(xc + y, yc - x, c);
                b.SetPixel(xc - y, yc + x, c);
                b.SetPixel(xc - y, yc - x, c);
            }
        }
        //elipse
        void Draw_Ellipse(int rx, int ry, int xc, int yc)
        {
            b = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            int x = 0, y = ry, i = 0;
            double p = (ry * ry) - (rx * rx * ry) + ((0.25) * rx * rx);
            if (t)
            {
                b.SetPixel(xc + x + int.Parse(tx.Text), yc + y + int.Parse(ty.Text), c);
                b.SetPixel(xc - x + int.Parse(tx.Text), yc + y + int.Parse(ty.Text), c);
                b.SetPixel(xc - x + int.Parse(tx.Text), yc - y + int.Parse(ty.Text), c);
                b.SetPixel(xc + x + int.Parse(tx.Text), yc - y + int.Parse(ty.Text), c);
            }
            else if (s)
            {
                b.SetPixel(xc + (x * int.Parse(tx.Text)), yc + (y * int.Parse(ty.Text)), c);
                b.SetPixel(xc - (x * int.Parse(tx.Text)), yc + (y * int.Parse(ty.Text)), c);
                b.SetPixel(xc - (x * int.Parse(tx.Text)), yc - (y * int.Parse(ty.Text)), c);
                b.SetPixel(xc + (x * int.Parse(tx.Text)), yc - (y * int.Parse(ty.Text)), c);
            }
            else
            {
                b.SetPixel(xc + x, yc + y, c);
                b.SetPixel(xc - x, yc + y, c);
                b.SetPixel(xc - x, yc - y, c);
                b.SetPixel(xc + x, yc - y, c);
            }
            DataGridViewRow r;
            while (2 * Math.Pow(ry, 2) * x < 2 * Math.Pow(rx, 2) * y)
            {
                r = new DataGridViewRow();
                r.CreateCells(dgv);
                r.Cells[0].Value = i;
                r.Cells[1].Value = p;
                x++;
                if (p < 0)
                    p += 2 * ry * ry * x + ry * ry;
                else
                {
                    y--;
                    p += 2 * ry * ry * x - 2 * rx * rx * y + ry * ry;
                }
                if (t)
                {
                    b.SetPixel(xc + x + int.Parse(tx.Text), yc + y + int.Parse(ty.Text), c);
                    b.SetPixel(xc - x + int.Parse(tx.Text), yc + y + int.Parse(ty.Text), c);
                    b.SetPixel(xc - x + int.Parse(tx.Text), yc - y + int.Parse(ty.Text), c);
                    b.SetPixel(xc + x + int.Parse(tx.Text), yc - y + int.Parse(ty.Text), c);
                }
                else if (s)
                {
                    b.SetPixel(xc + (x * int.Parse(tx.Text)), yc + (y * int.Parse(ty.Text)), c);
                    b.SetPixel(xc - (x * int.Parse(tx.Text)), yc + (y * int.Parse(ty.Text)), c);
                    b.SetPixel(xc - (x * int.Parse(tx.Text)), yc - (y * int.Parse(ty.Text)), c);
                    b.SetPixel(xc + (x * int.Parse(tx.Text)), yc - (y * int.Parse(ty.Text)), c);
                }
                else
                {
                    b.SetPixel(xc + x, yc + y, c);
                    b.SetPixel(xc - x, yc + y, c);
                    b.SetPixel(xc - x, yc - y, c);
                    b.SetPixel(xc + x, yc - y, c);
                }
                r.Cells[2].Value = x;
                r.Cells[3].Value = y;
                r.Cells[4].Value = 2 * ry * ry * x;
                r.Cells[5].Value = 2 * rx * rx * y;
                dgv.Rows.Add(r);
                i++;
            }
            r = new DataGridViewRow();
            r.CreateCells(dgv);
            r.Cells[0].Value = "i";
            r.Cells[1].Value = "pi";
            r.Cells[2].Value = "xi+1";
            r.Cells[3].Value = "yi+1";
            r.Cells[4].Value = "2*ry2*xi+1";
            r.Cells[5].Value = "2*rx2*yi+1";
            dgv.Rows.Add(r);
            i = 0;
            int p1 = ry * (x + (1 / 2)) * (x + (1 / 2)) + rx * (y - 1) * (y - 1) - ry * rx;
            while (y > 0)
            {
                y--;
                if (p1 < 0)
                {
                    x++;
                    p1 += 2 * ry * ry * x - 2 * rx * rx * y + rx * rx;
                }
                else
                    p1 -= 2 * rx * rx * y + rx * rx;
                r = new DataGridViewRow();
                r.CreateCells(dgv);
                r.Cells[0].Value = i;
                r.Cells[1].Value = p1;
                r.Cells[2].Value = x;
                r.Cells[3].Value = y;
                r.Cells[4].Value = 2 * ry * ry * x;
                r.Cells[5].Value = 2 * rx * rx * y;
                dgv.Rows.Add(r);
                i++;
                if (t)
                {
                    b.SetPixel(xc + x + int.Parse(tx.Text), yc + y + int.Parse(ty.Text), c);
                    b.SetPixel(xc - x + int.Parse(tx.Text), yc + y + int.Parse(ty.Text), c);
                    b.SetPixel(xc - x + int.Parse(tx.Text), yc - y + int.Parse(ty.Text), c);
                    b.SetPixel(xc + x + int.Parse(tx.Text), yc - y + int.Parse(ty.Text), c);
                }
                else if (s)
                {
                    b.SetPixel(xc + (x * int.Parse(tx.Text)), yc + (y * int.Parse(ty.Text)), c);
                    b.SetPixel(xc - (x * int.Parse(tx.Text)), yc + (y * int.Parse(ty.Text)), c);
                    b.SetPixel(xc - (x * int.Parse(tx.Text)), yc - (y * int.Parse(ty.Text)), c);
                    b.SetPixel(xc + (x * int.Parse(tx.Text)), yc - (y * int.Parse(ty.Text)), c);
                }
                else
                {
                    b.SetPixel(xc + x, yc + y, c);
                    b.SetPixel(xc - x, yc + y, c);
                    b.SetPixel(xc - x, yc - y, c);
                    b.SetPixel(xc + x, yc - y, c);
                }
            }
            pictureBox1.Image = b;
        }
        //frame
        private void Drawing_Click(object sender, EventArgs e)
        {
            if (!DrawingP.Visible)
                DrawingP.Visible = true;
            else
                DrawingP.Visible = false;
        }
        private void Draw_Click(object sender, EventArgs e)
        {
            TypeofShape();
        }
        private void Transportation_Click(object sender, EventArgs e)
        {
            if (!Transp.Visible)
                Transp.Visible = true;
            else
                Transp.Visible = false;
        }
        private void SelectColor_Click(object sender, EventArgs e)
        {
            ColorDialog CD = new ColorDialog();
            CD.ShowDialog();
            c = CD.Color;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "DDA")
            {
                X1.Visible = true;
                Y2.Visible = true;
                Y1.Visible = true;
                X2.Visible = true;
                LY1.Visible = true;
                LX1.Visible = true;
                LX2.Visible = true;
                LY2.Visible = true;
                R.Visible = false;
                RY1.Visible = false;
                RX.Visible = false;
                LRY.Visible = false;
                LRX.Visible = false;
                LR.Visible = false;

            }
            else if (comboBox1.Text == "Bresenham")
            {
                X1.Visible = true;
                Y2.Visible = true;
                Y1.Visible = true;
                X2.Visible = true;
                LY1.Visible = true;
                LX1.Visible = true;
                LX2.Visible = true;
                LY2.Visible = true;
                R.Visible = false;
                RY1.Visible = false;
                RX.Visible = false;
                LRY.Visible = false;
                LRX.Visible = false;
                LR.Visible = false;

            }
            else if (comboBox1.Text == "Circle")
            {
                R.Visible = true;
                LR.Visible = true;
                X1.Visible = false;
                Y2.Visible = false;
                Y1.Visible = false;
                X2.Visible = false;
                LY1.Visible = false;
                LX1.Visible = false;
                LX2.Visible = false;
                LY2.Visible = false;
                RY1.Visible = false;
                RX.Visible = false;
                LRY.Visible = false;
                LRX.Visible = false;
            }
            else if (comboBox1.Text == "Ellipce")
            {
                RY1.Visible = true;
                RX.Visible = true;
                LRY.Visible = true;
                LRX.Visible = true;
                R.Visible = false;
                LR.Visible = false;
                X1.Visible = false;
                Y2.Visible = false;
                Y1.Visible = false;
                X2.Visible = false;
                LY1.Visible = false;
                LX1.Visible = false;
                LX2.Visible = false;
                LY2.Visible = false;
            }
        }



        //printing table values
        private void button2_Click(object sender, EventArgs e)
        {
            if (!dgv.Visible)
                dgv.Visible = true;
            else
                dgv.Visible = false;
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text == "translation")
            {
                tx.Visible = true;
                ty.Visible = true;
                Lty.Visible = true;
                Ltx.Visible = true;
                Angle.Visible = false;
                Langle.Visible = false;
                LXmax.Visible = false;
                LYmax.Visible = false;
                LYnin.Visible = false;
                lXmin.Visible = false;

            }
            else if (comboBox2.Text == "scaling")
            {
                tx.Visible = true;
                ty.Visible = true;
                Lty.Visible = true;
                Ltx.Visible = true;
                Angle.Visible = false;
                Langle.Visible = false;
                LXmax.Visible = false;
                LYmax.Visible = false;
                LYnin.Visible = false;
                lXmin.Visible = false;
                Xmax.Visible = false;
                Ymax.Visible = false;
                Ymin.Visible = false;
                Xmin.Visible = false;
            }
            else if (comboBox2.Text == "rotation")
            {
                tx.Visible = false;
                ty.Visible = false;
                Lty.Visible = false;
                Ltx.Visible = false;
                Angle.Visible = true;
                Langle.Visible = true;
                LXmax.Visible = false;
                LYmax.Visible = false;
                LYnin.Visible = false;
                lXmin.Visible = false;
                Xmax.Visible = false;
                Ymax.Visible = false;
                Ymin.Visible = false;
                Xmin.Visible = false;
            }
            else if (comboBox2.Text == "Clipping")
            {
                tx.Visible = false;
                ty.Visible = false;
                Lty.Visible = false;
                Ltx.Visible = false;
                Angle.Visible = false;
                Langle.Visible = false;
                LXmax.Visible = true;
                LYmax.Visible = true;
                LYnin.Visible = true;
                lXmin.Visible = true;
                Xmax.Visible = true;
                Ymax.Visible = true;
                Ymin.Visible = true;
                Xmin.Visible = true;

            }
        }

        //appling transformation 
        private void Excute_Click(object sender, EventArgs e)
        {
            
            if (comboBox2.Text == "translation")
            {
                t = true;
                TypeofShape();
            }
            else if(comboBox2.Text == "scaling")
            {
                s = true;
                TypeofShape();
            }
            else if (comboBox2.Text == "rotation")
            {
                ro = true;
                TypeofShape();
            }
            else if ((comboBox2.Text =="Clipping"))
            {
                clip(int.Parse(X1.Text), int.Parse(Y1.Text), int.Parse(X2.Text), int.Parse(Y2.Text), int.Parse(Xmin.Text), int.Parse(Ymin.Text), int.Parse(Xmax.Text), int.Parse(Ymax.Text));
            }

        }
        void TypeofShape()
        {
            dgv.Rows.Clear();
            if (comboBox1.Text == "DDA")
            {
                int x = int.Parse(X1.Text);
                int y = int.Parse(Y1.Text);
                int x2 = int.Parse(X2.Text);
                int y2 = int.Parse(Y2.Text);
                dgv.Columns[0].HeaderText = "i";
                dgv.Columns[1].HeaderText = "x";
                dgv.Columns[2].HeaderText = "y";
                dgv.Columns[3].HeaderText = "Round x";
                dgv.Columns[4].HeaderText = "Round y";
                dgv.Columns[5].Visible = false;
                DDA_Line(x, x2, y, y2);
                t = false;
                s = false;
                ro = false;
            }
            else if (comboBox1.Text == "Bresenham")
            {
                int x = int.Parse(X1.Text);
                int y = int.Parse(Y1.Text);
                int x2 = int.Parse(X2.Text);
                int y2 = int.Parse(Y2.Text);
                dgv.Columns[0].HeaderText = "i";
                dgv.Columns[1].HeaderText = "x";
                dgv.Columns[2].HeaderText = "y";
                dgv.Columns[3].HeaderText = "p";
                dgv.Columns[4].HeaderText ="x+1";
                dgv.Columns[5].Visible = true;
                dgv.Columns[5].HeaderText = "y+1";
                Bres_Line_Draw(x, x2, y, y2);
                t = false;
                s = false;
                
            }
            else if (comboBox1.Text == "Circle")
            {
                dgv.Columns[0].HeaderText = "i";
                dgv.Columns[1].HeaderText = "p";
                dgv.Columns[2].HeaderText = "x+1";
                dgv.Columns[3].HeaderText = "y+1";
                dgv.Columns[4].HeaderText = "2 * x+1";
                dgv.Columns[5].HeaderText = "2 * y+1";
                Draw_Circle(int.Parse(R.Text), pictureBox1.Width / 2, pictureBox1.Height / 2);
                t = false;
                s = false;
            }
            else if (comboBox1.Text == "Ellipce")
            {
                dgv.Columns[0].HeaderText = "i";
                dgv.Columns[1].HeaderText = "pi";
                dgv.Columns[2].HeaderText = "xi+1";
                dgv.Columns[3].HeaderText = "yi+1";
                dgv.Columns[4].HeaderText = "2*ry2*xi+1";
                dgv.Columns[5].HeaderText = "2*rx2*yi+1";
                Draw_Ellipse(int.Parse(RY1.Text), int.Parse(RX.Text), pictureBox1.Width / 2, pictureBox1.Height / 2);
                t = false;
                s = false;
            }
        }
      


        
        //clipping
        public int complutecode(int x, int y)
        { 
            int code = inside;
            if (x < int.Parse(Xmin.Text))
                code = code | left;
           else if (x > int.Parse(Xmax.Text))
                code = code | right;
            if (y < int.Parse(Ymin.Text))
                code = code | bottom;
           else if (y > int.Parse(Ymax.Text))
                code = code | top;
            return code;
        }



        public void clip(int x1, int y1, int x2, int y2, int xmin, int ymin, int xmax, int ymax)
        { 

            int code1 = complutecode(x1, y1);
            int code2 = complutecode(x2, y2);

            bool flag = false;

            while (true)
            {
                if (code1 == 0 & code2 == 0)
                {
                    flag = true;
                    break;
                }
                else if ((code1 & code2) != 0)
                {
                    break;
                }
                else
                {
                    int code_out;
                    int x = 0, y =0;

                    
                    if (code1 != 0)
                        code_out = code1;
                    else
                        code_out = code2;

                  
                    if (code_out == top)
                    {
                        x = x1 + ((x2 - x1) / (y2 - y1)) * (ymax - y1);
                        y = ymax;
                    }
                    else if (code_out == bottom)
                    {
                        x = x1 + ((x2 - x1) / (y2 - y1)) * (ymin - y1);
                        y = ymin;
                    }
                    else if (code_out == right)
                    {
                        y = y1 + ((y2 - y1)  / (x2 - x1)) * (xmax - x1) ;
                        x = xmax;
                    }
                    else if (code_out == left)
                    {
                        y = y1 + ((y2 - y1)  / (x2 - x1)) * (xmin-x1);
                        x = xmin;
                    }

                   
                    if (code_out == code1)
                    {
                        x1 = x;
                        y1 = y;
                        code1 = complutecode(x1, y1); 
                    }
                    else
                    {
                        x2 = x;
                        y2 = y;
                        code2 = complutecode(x2, y2);
                    }
                }
            }
   
            if (flag == true)
            {
                if (comboBox1.Text == "DDA")
                {
                    dgv.Columns[0].HeaderText = "i";
                    dgv.Columns[1].HeaderText = "x";
                    dgv.Columns[2].HeaderText = "y";
                    dgv.Columns[3].HeaderText = "Round x";
                    dgv.Columns[4].HeaderText = "Round y";
                    dgv.Columns[5].Visible = false;
                    DDA_Line(x1, x2, y1, y2);
                }
                else if (comboBox1.Text == "Bresenham")
                {
                    dgv.Columns[0].HeaderText = "i";
                    dgv.Columns[1].HeaderText = "pi";
                    dgv.Columns[2].HeaderText = "xi+1";
                    dgv.Columns[3].HeaderText = "yi+1";
                    dgv.Columns[4].HeaderText = "2*ry2*xi+1";
                    dgv.Columns[5].HeaderText = "2*rx2*yi+1";
                    Bres_Line_Draw(x1, x2, y1, y2);
                }
            }
            else
            {
                pictureBox1.Image = null;
            }
        }
    }
}
