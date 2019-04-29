using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CurvasDeBezier
{
    public partial class Form1 : Form
    {
        int Contador;
        Point[] points = new Point[3];
        Point l1 = new Point();
        Point l2 = new Point();
        Point l3 = new Point();
        Point l4 = new Point();
        Point l5 = new Point();
        Point l6 = new Point();
        public Form1()
        {
            Contador = 0;
            InitializeComponent();
           
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            points[Contador] = new Point(e.X, e.Y);
            Contador++;
            if(Contador==Convert.ToInt32(comboBox1.Text))
            {
                DibujarLineas(points,Contador-1);
                Contador = 0;
            }
        }

        void DibujarLineas(Point[] points, int np)
        {
            panel1.CreateGraphics().Clear(Color.White);
            if (Convert.ToInt32(comboBox1.Text)==3)
                CalcularCurvas2doGrado(points, DistanciaMayor(points, np));
            else if(Convert.ToInt32(comboBox1.Text)==4)
                CalcularCurvas3doGrado(points, DistanciaMayor(points, np));
            
            //panel1.CreateGraphics().DrawBeziers(Returnpen(Color.Black), points);
                
        }

        Pen Returnpen(Color color)
        {
            Pen pen1 = new Pen(color, 1);
            return pen1;
        }

        double GetDistanciaEuclineana(Point a, Point b)
        {
            double resultado;
            resultado = Math.Sqrt(Math.Pow((b.X-a.X),2)+Math.Pow((b.Y-a.Y),2)); 
            return resultado;
        }

        /* Para curvas de 2do y 3er grado*/
        ////////
        Point GetPuntoCurva(Point[] cp, double t, int np)
        {
            double ax, bx, cx;
            double ay, by, cy;
            Double tSquared, tCubed;
            Point result = new Point();

            /* cálculo de los coeficientes polinomiales */

            cx = 3.0 * (cp[1].X - cp[0].X);
            bx = 3.0 * (cp[2].X - cp[1].X) - cx;
            ax = cp[np].X - cp[0].X - cx - bx;

            cy = 3.0 * (cp[1].Y - cp[0].Y);
            by = 3.0 * (cp[2].Y - cp[1].Y) - cy;
            ay = cp[np].Y - cp[0].Y - cy - by;

            /* calculo de los parametros evaluados en t */
            
            tSquared = t * t;
            tCubed = tSquared * t;

            result.X = Convert.ToInt32((ax * tCubed) + (bx * tSquared) + (cx * t) + cp[0].X);
            result.Y = Convert.ToInt32((ay * tCubed) + (by * tSquared) + (cy * t) + cp[0].Y);
            panel1.CreateGraphics().DrawLine(Returnpen(Color.Yellow), (float)(cp[0].X + (ax * tCubed)), (float)(cp[0].Y+ (ay * tCubed)), (float)(cp[2].X), (float)(cp[2].Y ));

            return result;
        }

        void CalcularCurva(Point[] cp, int n ,int np)
        {
            Point[] curve = new Point[n];
            double dt;
            int i;

            dt = 1.0 / (n - 1.0);

            for (i = 0; i <n; i++)
            {
                curve[i] = GetPuntoCurva(cp, i * dt,np);
                panel1.CreateGraphics().DrawRectangle(Returnpen(Color.Red), curve[i].X, curve[i].Y, 1, 1);
            }
        }

        //////////

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            points = new Point[Convert.ToInt32(comboBox1.Text)];
        }



        void CalcularCurvas2doGrado(Point[] cp, int np)
        {
            Point[] curve = new Point[np];
            float t;
            t = (float)1 / (float)(np - 1);
        
            for(int i=0;i<np;i++)
            {
                //panel1.CreateGraphics().Clear(Color.White);
                panel1.CreateGraphics().DrawLine(Returnpen(Color.White), l1.X,l1.Y,l2.X,l2.Y);
                curve[i] = GetPuntoCurva2doGrado(cp, i * t);
                panel1.CreateGraphics().DrawLines(Returnpen(Color.Black), points);
                dibujarCurvaActual(curve,i);
                //System.Threading.Thread.Sleep(1);
            }
        }

        Point GetPuntoCurva2doGrado(Point[] cp, double t )
        {
            Point resultado = new Point();
            int rx1, ry1,rx2,ry2;

            rx1 = (int)((cp[1].X - cp[0].X) * t) + cp[0].X;
            ry1 = (int)((cp[1].Y - cp[0].Y) * t) + cp[0].Y;
            rx2 = (int)((cp[2].X - cp[1].X) * t) + cp[1].X;
            ry2 = (int)((cp[2].Y - cp[1].Y) * t) + cp[1].Y;
            resultado.X = (int)Math.Round(((rx2- rx1) * t) + rx1);
            resultado.Y = (int)Math.Round(((ry2 - ry1) * t) + ry1);
            
            panel1.CreateGraphics().DrawLine(Returnpen(Color.Red), rx1,ry1,rx2,ry2);
            l1 = new Point(rx1,ry1);
            l2 = new Point(rx2, ry2);
            return resultado;
        }

        void dibujarCurvaActual(Point [] curva, int n)
        {
            for(int i=0;i<n;i++)
            {
                panel1.CreateGraphics().DrawRectangle(Returnpen(Color.Blue), curva[i].X, curva[i].Y, 1, 1);
            }
        }

        void CalcularCurvas3doGrado(Point[] cp, int np)
        {
            Point[] curve = new Point[np];
            float t;
            t = (float)1 / (float)(np - 1);
    
            for (int i = 0; i < np; i++)
            {
                //panel1.CreateGraphics().Clear(Color.White);
                panel1.CreateGraphics().DrawLine(Returnpen(Color.White), l1.X, l1.Y, l2.X, l2.Y);
                panel1.CreateGraphics().DrawLine(Returnpen(Color.White), l2.X, l2.Y, l3.X, l3.Y);
                panel1.CreateGraphics().DrawLine(Returnpen(Color.White), l4.X, l4.Y, l5.X, l5.Y);
                curve[i] = GetPuntoCurva3doGrado(cp, i * t);

                panel1.CreateGraphics().DrawLines(Returnpen(Color.Black), points);
                dibujarCurvaActual(curve, i);
                
            }
        }

        Point GetPuntoCurva3doGrado(Point[] cp, double t)
        {
            Point resultado = new Point();
            int rx1, ry1, rx2, ry2,rx3,ry3,rx4,ry4,rx5,ry5;

            rx1 = (int)((cp[1].X - cp[0].X) * t) + cp[0].X;
            ry1 = (int)((cp[1].Y - cp[0].Y) * t) + cp[0].Y;
            rx2 = (int)((cp[2].X - cp[1].X) * t) + cp[1].X;
            ry2 = (int)((cp[2].Y - cp[1].Y) * t) + cp[1].Y;
            rx3 = (int)((cp[3].X - cp[2].X) * t) + cp[2].X;
            ry3 = (int)((cp[3].Y - cp[2].Y) * t) + cp[2].Y;
            rx4 = (int)((rx2 - rx1) * t) + rx1;
            ry4 = (int)((ry2 - ry1) * t) + ry1;
            rx5 = (int)((rx3 - rx2) * t) + rx2;
            ry5 = (int)((ry3 - ry2) * t) + ry2;
          
            resultado.X = (int)Math.Round(((rx5 - rx4) * t) + rx4);
            resultado.Y = (int)Math.Round(((ry5 - ry4) * t) + ry4);
            panel1.CreateGraphics().DrawLine(Returnpen(Color.Green), rx1, ry1, rx2, ry2);
            panel1.CreateGraphics().DrawLine(Returnpen(Color.Green), rx2, ry2, rx3, ry3);
            panel1.CreateGraphics().DrawLine(Returnpen(Color.Blue), rx4, ry4, rx5, ry5);
            l1 = new Point(rx1, ry1);
            l2 = new Point(rx2, ry2);
            l3 = new Point(rx3, ry3);
            l4 = new Point(rx4, ry4);
            l5 = new Point(rx5, ry5);
  
            return resultado;
        }

        int DistanciaMayor(Point[] p, int n)
        {
            int resultado;
            int distancia = 0;
            resultado = 0;

            resultado = (int)(Math.Sqrt(Math.Pow((p[n-1].X - p[0].X), 2) + Math.Pow((p[n-1].Y - p[0].Y), 2)));


            /*for (int i = 0; i < n-1; i++)
            {
                distancia = (int)(Math.Sqrt(Math.Pow((p[i + 1].X - p[i].X), 2) + Math.Pow((p[i + 1].Y - p[i].Y), 2)));

                if(distancia>resultado)
                {
                    resultado = distancia;
                }
            }*/

            return resultado;
        }

   


    }
}
