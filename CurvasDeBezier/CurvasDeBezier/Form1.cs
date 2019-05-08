using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

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
        Point l7 = new Point();
        Point l8 = new Point();
        Point l9 = new Point();
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
            if (Contador == Convert.ToInt32(comboBox1.Text))
            {
                DibujarLineas(points, Contador - 1);
                Contador = 0;
            }
        }/* Agrega los puntos insertados en el plano en un agrego*/

        void DibujarLineas(Point[] points, int np)
        {
            
            
            
            


            panel1.CreateGraphics().Clear(Color.White);
            if (Convert.ToInt32(comboBox1.Text) == 3)
            {
                CalcularCurvas2doGrado(points, DistanciaMayor(points, np));
                CalcularCurvas2doGradoMatricial(points, DistanciaMayor(points, np));

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                CalcularCurvas2doGradoTiempo(points, DistanciaMayor(points, np));
                stopwatch.Stop();
                TimeSpan ts = stopwatch.Elapsed;
                tempecu.Text = String.Format("{0}", ts.TotalMilliseconds);

            }
            else if (Convert.ToInt32(comboBox1.Text) == 4)
            {
                CalcularCurvas3doGrado(points, DistanciaMayor(points, np));
                CalcularCurvas3doGradoMatricial(points, DistanciaMayor(points, np));
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                CalcularCurvas3doGradoTiempo(points, DistanciaMayor(points, np));
                stopwatch.Stop();
                TimeSpan ts = stopwatch.Elapsed;
                tempecu.Text = String.Format("{0}", ts.TotalMilliseconds);
            }

            else if (Convert.ToInt32(comboBox1.Text) == 5)
            {
                CalcularCurvas4doGrado(points, DistanciaMayor(points, np));
                CalcularCurvas4doGradoMatricial(points, DistanciaMayor(points, np));

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                CalcularCurvas4doGradoTiempo(points, DistanciaMayor(points, np));
                stopwatch.Stop();
                TimeSpan ts = stopwatch.Elapsed;
                tempecu.Text = String.Format("{0}", ts.TotalMilliseconds);
            }

        }/*Dibuja las diferentes lineas de bezier segun el grado que se selecciono*/





        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            points = new Point[Convert.ToInt32(comboBox1.Text)];
            Contador = 0;
        }
        /*   cambia el numero de puntos que se insertaran en el plano    */



        /// Calculos para sacar las curvas de bezier


        void CalcularCurvas2doGrado(Point[] cp, int np)
        {
            Point[] curve = new Point[np];
            float t;
            t = (float)1 / (float)(np - 1);

            for (int i = 0; i < np; i++)
            {
                panel1.CreateGraphics().DrawLine(Returnpen(Color.White), l1.X, l1.Y, l2.X, l2.Y);
                curve[i] = GetPuntoCurva2doGrado(cp, (float)i * t);
                panel1.CreateGraphics().DrawLines(Returnpen(Color.Black), points);
                dibujarCurvaActual(curve, i, Color.Blue);

            }
        }/* Realiza la calculacion de cada punto iterando desde 0 hasta n segun la distancia entre el punto inicial y final
            en cada iteracion pasa el valor de i * t y los puntos en la curva
         */

        Point GetPuntoCurva2doGrado(Point[] cp, float t)
        {
            Point resultado = new Point();
            int rx1, ry1, rx2, ry2;

            rx1 = (int)Math.Round((cp[1].X - cp[0].X) * t) + cp[0].X;
            ry1 = (int)Math.Round((cp[1].Y - cp[0].Y) * t) + cp[0].Y;
            rx2 = (int)Math.Round((cp[2].X - cp[1].X) * t) + cp[1].X;
            ry2 = (int)Math.Round((cp[2].Y - cp[1].Y) * t) + cp[1].Y;
            resultado.X = (int)Math.Round(((rx2 - rx1) * t) + rx1);
            resultado.Y = (int)Math.Round(((ry2 - ry1) * t) + ry1);

            panel1.CreateGraphics().DrawLine(Returnpen(Color.Red), rx1, ry1, rx2, ry2);
            l1 = new Point(rx1, ry1);
            l2 = new Point(rx2, ry2);
            return resultado;
        }/*
            Realiza el calculo de la curva mediante el ingreso de 3 puntos y retorna el valor que s eencuentra en la curva
         */

        void dibujarCurvaActual(Point[] curva, int n, Color x)
        {
            for (int i = 0; i < n - 1; i++)
            {
                panel1.CreateGraphics().DrawLine(Returnpen(x), curva[i].X, curva[i].Y, curva[i + 1].X, curva[1 + i].Y);
            }
        }/*
            dibuja la curva actual que se genera
         */

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
                curve[i] = GetPuntoCurva3doGrado(cp, (float)i * t);

                panel1.CreateGraphics().DrawLines(Returnpen(Color.Black), points);
                dibujarCurvaActual(curve, i, Color.Blue);

            }
        }/*
            se realiza las iteraciones para el grado 3
         */

        Point GetPuntoCurva3doGrado(Point[] cp, float t)
        {
            Point resultado = new Point();
            int rx1, ry1, rx2, ry2, rx3, ry3, rx4, ry4, rx5, ry5;

            rx1 = (int)Math.Round((cp[1].X - cp[0].X) * t) + cp[0].X;
            ry1 = (int)Math.Round((cp[1].Y - cp[0].Y) * t) + cp[0].Y;
            rx2 = (int)Math.Round((cp[2].X - cp[1].X) * t) + cp[1].X;
            ry2 = (int)Math.Round((cp[2].Y - cp[1].Y) * t) + cp[1].Y;
            rx3 = (int)Math.Round((cp[3].X - cp[2].X) * t) + cp[2].X;
            ry3 = (int)Math.Round((cp[3].Y - cp[2].Y) * t) + cp[2].Y;
            rx4 = (int)Math.Round((rx2 - rx1) * t) + rx1;
            ry4 = (int)Math.Round((ry2 - ry1) * t) + ry1;
            rx5 = (int)Math.Round((rx3 - rx2) * t) + rx2;
            ry5 = (int)Math.Round((ry3 - ry2) * t) + ry2;

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
        } /*
            Realiza el calculo del punto en la curva recibiendo 4 puntos
          */



        void CalcularCurvas4doGrado(Point[] cp, int np)
        {
            //List<Point> curve = new List<Point>();
            Point[] curve = new Point[np];
            float t;
            t = (float)1 / (float)(np - 1);

            for (int i = 0; i < np; i++)
            {
                //panel1.CreateGraphics().Clear(Color.White);
                panel1.CreateGraphics().DrawLine(Returnpen(Color.White), l1.X, l1.Y, l2.X, l2.Y);
                panel1.CreateGraphics().DrawLine(Returnpen(Color.White), l2.X, l2.Y, l3.X, l3.Y);
                panel1.CreateGraphics().DrawLine(Returnpen(Color.White), l3.X, l3.Y, l4.X, l4.Y);
                panel1.CreateGraphics().DrawLine(Returnpen(Color.White), l5.X, l5.Y, l6.X, l6.Y);
                panel1.CreateGraphics().DrawLine(Returnpen(Color.White), l6.X, l6.Y, l7.X, l7.Y);
                panel1.CreateGraphics().DrawLine(Returnpen(Color.White), l8.X, l8.Y, l9.X, l9.Y);

                curve[i] = (GetPuntoCurva4doGrado(cp, (float)i * t));

                panel1.CreateGraphics().DrawLines(Returnpen(Color.Black), points);


                dibujarCurvaActual(curve, i, Color.Blue);

            }
        }     /*
             se realiza el calculo de  los puntos que iran en la linea
        */



        Point GetPuntoCurva4doGrado(Point[] cp, float t)
        {
            Point resultado = new Point();
            int rx1, ry1, rx2, ry2, rx3, ry3, rx4, ry4, rx5, ry5, rx6, ry6, rx7, ry7, rx8, ry8, rx9, ry9;

            rx1 = (int)Math.Round((cp[1].X - cp[0].X) * t) + cp[0].X;
            ry1 = (int)Math.Round((cp[1].Y - cp[0].Y) * t) + cp[0].Y;
            rx2 = (int)Math.Round((cp[2].X - cp[1].X) * t) + cp[1].X;
            ry2 = (int)Math.Round((cp[2].Y - cp[1].Y) * t) + cp[1].Y;
            rx3 = (int)Math.Round((cp[3].X - cp[2].X) * t) + cp[2].X;
            ry3 = (int)Math.Round((cp[3].Y - cp[2].Y) * t) + cp[2].Y;
            rx4 = (int)Math.Round((cp[4].X - cp[3].X) * t) + cp[3].X;
            ry4 = (int)Math.Round((cp[4].Y - cp[3].Y) * t) + cp[3].Y;
            rx5 = (int)Math.Round((rx2 - rx1) * t) + rx1;
            ry5 = (int)Math.Round((ry2 - ry1) * t) + ry1;
            rx6 = (int)Math.Round((rx3 - rx2) * t) + rx2;
            ry6 = (int)Math.Round((ry3 - ry2) * t) + ry2;
            rx7 = (int)Math.Round((rx4 - rx3) * t) + rx3;
            ry7 = (int)Math.Round((ry4 - ry3) * t) + ry3;
            rx8 = (int)Math.Round((rx6 - rx5) * t) + rx5;
            ry8 = (int)Math.Round((ry6 - ry5) * t) + ry5;
            rx9 = (int)Math.Round((rx7 - rx6) * t) + rx6;
            ry9 = (int)Math.Round((ry7 - ry6) * t) + ry6;


            resultado.X = (int)Math.Round(((rx9 - rx8) * t) + rx8);
            resultado.Y = (int)Math.Round(((ry9 - ry8) * t) + ry8);
            panel1.CreateGraphics().DrawLine(Returnpen(Color.Green), rx1, ry1, rx2, ry2);
            panel1.CreateGraphics().DrawLine(Returnpen(Color.Green), rx2, ry2, rx3, ry3);
            panel1.CreateGraphics().DrawLine(Returnpen(Color.Green), rx3, ry3, rx4, ry4);
            panel1.CreateGraphics().DrawLine(Returnpen(Color.Blue), rx5, ry5, rx6, ry6);
            panel1.CreateGraphics().DrawLine(Returnpen(Color.Blue), rx6, ry6, rx7, ry7);
            panel1.CreateGraphics().DrawLine(Returnpen(Color.Red), rx8, ry8, rx9, ry9);
            l1 = new Point(rx1, ry1);
            l2 = new Point(rx2, ry2);
            l3 = new Point(rx3, ry3);
            l4 = new Point(rx4, ry4);
            l5 = new Point(rx5, ry5);
            l6 = new Point(rx6, ry6);
            l7 = new Point(rx7, ry7);
            l8 = new Point(rx8, ry8);
            l9 = new Point(rx9, ry9);
            return resultado;
        }
        /*
             el calculo del punto en la curva recibiendo 5 puntos
         */



        /// Generacion de curvas de bezier mediante el metodo del uso de matrices ///
        Point GetPuntoCurvas2doGradoMatricial(Point[] cp, float u)
        {
            Point Resultado = new Point();
            float ax, bx, cx, ay, by, cy, x1, x2, x3, y1, y2, y3, u2;
            u2 = u * u;
            ax = cp[0].X;
            bx = cp[1].X;
            cx = cp[2].X;
            ay = cp[0].Y;
            by = cp[1].Y;
            cy = cp[2].Y;
            x1 = ax + (-2 * bx) + cx;
            x2 = (-2 * ax) + (2 * bx);
            x3 = ax;
            y1 = ay + (-2 * by) + cy;
            y2 = (-2 * ay) + (2 * by);
            y3 = ay;

            Resultado.X = (int)Math.Round((x1 * u2) + (x2 * u) + x3);
            Resultado.Y = (int)Math.Round((y1 * u2) + (y2 * u) + y3);
            return Resultado;


        }     /*
             recibe 3 puntos para retornar el valor  en la curva
        */


        /// Generacion de curvas de bezier mediante el metodo del uso de matrices ///
        /// ///////////////////////////////////////////////////////////////////////////



        void CalcularCurvas2doGradoMatricial(Point[] cp, int np)
        {
            //List<Point> curve = new List<Point>();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Point[] curve = new Point[np];
            float t;
            t = (float)1 / (float)(np - 1);

            for (int i = 0; i < np; i++)
            {
                //panel1.CreateGraphics().Clear(Color.White);
                curve[i] = (GetPuntoCurvas2doGradoMatricial(cp, (float)i * t));
            }
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            tempmat.Text = String.Format("{0}", ts.TotalMilliseconds);
            dibujarCurvaActual(curve, np - 1, Color.Red);
        } /* pasa por referencia los puntos y retorna el valor la curva */


        Point GetPuntoQ(Point cp, Point cp2, float u)
        {
            Point Resultado;
            int Qx, Qy;
            Qx = (int)Math.Round(cp.X + u * (cp2.X - cp.X));

            Qy = (int)Math.Round(cp.Y + u * (cp2.Y - cp.Y));
            Resultado = new Point(Qx, Qy);
            return Resultado;
        }/* retorna el punto Q entre dos punt os*/


        void CalcularCurvas3doGradoMatricial(Point[] cp, int np)
        {
            //List<Point> curve = new List<Point>();
            Point[] curve = new Point[np];
            Point[] curved2 = new Point[2];
            Point[] curved3 = new Point[2];
            Point[] curved4 = new Point[2];
            Point[] curvepuntos = new Point[3];
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            float t;
            t = (float)1 / (float)(np - 1);

            for (int i = 0; i < np; i++)
            {

                curvepuntos[0] = GetPuntoQ(cp[0], cp[1], (float)i * t);
                curvepuntos[1] = GetPuntoQ(cp[1], cp[2], (float)i * t);
                curvepuntos[2] = GetPuntoQ(cp[2], cp[3], (float)i * t);

                curve[i] = (GetPuntoCurvas2doGradoMatricial(curvepuntos, (float)i * t));

            }
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            tempmat.Text = String.Format("{0}", ts.TotalMilliseconds);
            dibujarCurvaActual(curve, np - 1, Color.Red);
        }



        void CalcularCurvas4doGradoMatricial(Point[] cp2, int np)
        {
            //List<Point> curve = new List<Point>();
            Point[] curve = new Point[np];
            Point[] curved2 = new Point[2];
            Point[] curved3 = new Point[2];
            Point[] curved4 = new Point[2];
            Point[] curvepuntos = new Point[3];
            Point[] cp = new Point[4];
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();


            float t;
            t = (float)1 / (float)(np - 1);

            for (int i = 0; i < np; i++)
            {
                cp[0] = GetPuntoQ(cp2[0], cp2[1], (float)i * t);
                cp[1] = GetPuntoQ(cp2[1], cp2[2], (float)i * t);
                cp[2] = GetPuntoQ(cp2[2], cp2[3], (float)i * t);
                cp[3] = GetPuntoQ(cp2[3], cp2[4], (float)i * t);


                curvepuntos[0] = GetPuntoQ(cp[0], cp[1], (float)i * t);

                curvepuntos[1] = GetPuntoQ(cp[1], cp[2], (float)i * t);

                curvepuntos[2] = GetPuntoQ(cp[2], cp[3], (float)i * t);


                curve[i] = (GetPuntoCurvas2doGradoMatricial(curvepuntos, (float)i * t));





            }
            
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            tempmat.Text = String.Format("{0}", ts.TotalMilliseconds);

            dibujarCurvaActual(curve, np - 1, Color.Red);
        }


        /*Utilidades*/


        int DistanciaMayor(Point[] p, int n)
        {
            int resultado;
            int distancia = 0;
            resultado = 0;

            resultado = (int)(Math.Sqrt(Math.Pow((p[n - 1].X - p[0].X), 2) + Math.Pow((p[n - 1].Y - p[0].Y), 2)));


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

        Pen Returnpen(Color color)
        {
            Pen pen1 = new Pen(color, 1);
            return pen1;
        }//* retorna el pen del color ingresado *//

        double GetDistanciaEuclineana(Point a, Point b)
        {
            double resultado;
            resultado = Math.Sqrt(Math.Pow((b.X - a.X), 2) + Math.Pow((b.Y - a.Y), 2));
            return resultado;
        } /* Retorna la distancia euclineana entre dos puntos*/


        ///Ecuaciones independientes tiempo de ejecucion ///
        void CalcularCurvas2doGradoTiempo(Point[] cp, int np)
        {
            Point[] curve = new Point[np];
            float t;
            t = (float)1 / (float)(np - 1);

            for (int i = 0; i < np; i++)
            {
                //panel1.CreateGraphics().DrawLine(Returnpen(Color.White), l1.X, l1.Y, l2.X, l2.Y);
                curve[i] = GetPuntoCurva2doGradoTiempo(cp, (float)i * t);
                //panel1.CreateGraphics().DrawLines(Returnpen(Color.Black), points);
               // dibujarCurvaActual(curve, i, Color.Blue);

            }
        }/* Realiza la calculacion de cada punto iterando desde 0 hasta n segun la distancia entre el punto inicial y final
            en cada iteracion pasa el valor de i * t y los puntos en la curva
         */

        Point GetPuntoCurva2doGradoTiempo(Point[] cp, float t)
        {
            Point resultado = new Point();
            int rx1, ry1, rx2, ry2;

            rx1 = (int)Math.Round((cp[1].X - cp[0].X) * t) + cp[0].X;
            ry1 = (int)Math.Round((cp[1].Y - cp[0].Y) * t) + cp[0].Y;
            rx2 = (int)Math.Round((cp[2].X - cp[1].X) * t) + cp[1].X;
            ry2 = (int)Math.Round((cp[2].Y - cp[1].Y) * t) + cp[1].Y;
            resultado.X = (int)Math.Round(((rx2 - rx1) * t) + rx1);
            resultado.Y = (int)Math.Round(((ry2 - ry1) * t) + ry1);

            //panel1.CreateGraphics().DrawLine(Returnpen(Color.Red), rx1, ry1, rx2, ry2);
          
            return resultado;
        }/*
            Realiza el calculo de la curva mediante el ingreso de 3 puntos y retorna el valor que s eencuentra en la curva
         */

       /*
            dibuja la curva actual que se genera
         */

        void CalcularCurvas3doGradoTiempo(Point[] cp, int np)
        {
            Point[] curve = new Point[np];
            float t;
            t = (float)1 / (float)(np - 1);

            for (int i = 0; i < np; i++)
            {
                //panel1.CreateGraphics().Clear(Color.White);
                //panel1.CreateGraphics().DrawLine(Returnpen(Color.White), l1.X, l1.Y, l2.X, l2.Y);
                //panel1.CreateGraphics().DrawLine(Returnpen(Color.White), l2.X, l2.Y, l3.X, l3.Y);
                //panel1.CreateGraphics().DrawLine(Returnpen(Color.White), l4.X, l4.Y, l5.X, l5.Y);
                curve[i] = GetPuntoCurva3doGradoTiempo(cp, (float)i * t);

                //panel1.CreateGraphics().DrawLines(Returnpen(Color.Black), points);
                //dibujarCurvaActual(curve, i, Color.Blue);

            }
        }/*
            se realiza las iteraciones para el grado 3
         */

        Point GetPuntoCurva3doGradoTiempo(Point[] cp, float t)
        {
            Point resultado = new Point();
            int rx1, ry1, rx2, ry2, rx3, ry3, rx4, ry4, rx5, ry5;

            rx1 = (int)Math.Round((cp[1].X - cp[0].X) * t) + cp[0].X;
            ry1 = (int)Math.Round((cp[1].Y - cp[0].Y) * t) + cp[0].Y;
            rx2 = (int)Math.Round((cp[2].X - cp[1].X) * t) + cp[1].X;
            ry2 = (int)Math.Round((cp[2].Y - cp[1].Y) * t) + cp[1].Y;
            rx3 = (int)Math.Round((cp[3].X - cp[2].X) * t) + cp[2].X;
            ry3 = (int)Math.Round((cp[3].Y - cp[2].Y) * t) + cp[2].Y;
            rx4 = (int)Math.Round((rx2 - rx1) * t) + rx1;
            ry4 = (int)Math.Round((ry2 - ry1) * t) + ry1;
            rx5 = (int)Math.Round((rx3 - rx2) * t) + rx2;
            ry5 = (int)Math.Round((ry3 - ry2) * t) + ry2;

            resultado.X = (int)Math.Round(((rx5 - rx4) * t) + rx4);
            resultado.Y = (int)Math.Round(((ry5 - ry4) * t) + ry4);
            //panel1.CreateGraphics().DrawLine(Returnpen(Color.Green), rx1, ry1, rx2, ry2);
            //panel1.CreateGraphics().DrawLine(Returnpen(Color.Green), rx2, ry2, rx3, ry3);
            //panel1.CreateGraphics().DrawLine(Returnpen(Color.Blue), rx4, ry4, rx5, ry5);
         

            return resultado;
        } /*
            Realiza el calculo del punto en la curva recibiendo 4 puntos
          */



        void CalcularCurvas4doGradoTiempo(Point[] cp, int np)
        {
            //List<Point> curve = new List<Point>();
            Point[] curve = new Point[np];
            float t;
            t = (float)1 / (float)(np - 1);

            for (int i = 0; i < np; i++)
            {
                //panel1.CreateGraphics().Clear(Color.White);
                //panel1.CreateGraphics().DrawLine(Returnpen(Color.White), l1.X, l1.Y, l2.X, l2.Y);
                //panel1.CreateGraphics().DrawLine(Returnpen(Color.White), l2.X, l2.Y, l3.X, l3.Y);
                //panel1.CreateGraphics().DrawLine(Returnpen(Color.White), l3.X, l3.Y, l4.X, l4.Y);
                //panel1.CreateGraphics().DrawLine(Returnpen(Color.White), l5.X, l5.Y, l6.X, l6.Y);
                //panel1.CreateGraphics().DrawLine(Returnpen(Color.White), l6.X, l6.Y, l7.X, l7.Y);
                //panel1.CreateGraphics().DrawLine(Returnpen(Color.White), l8.X, l8.Y, l9.X, l9.Y);

                curve[i] = (GetPuntoCurva4doGradoTiempo(cp, (float)i * t));

                //panel1.CreateGraphics().DrawLines(Returnpen(Color.Black), points);


                //dibujarCurvaActual(curve, i, Color.Blue);

            }
        }     /*
             se realiza el calculo de  los puntos que iran en la linea
        */



        Point GetPuntoCurva4doGradoTiempo(Point[] cp, float t)
        {
            Point resultado = new Point();
            int rx1, ry1, rx2, ry2, rx3, ry3, rx4, ry4, rx5, ry5, rx6, ry6, rx7, ry7, rx8, ry8, rx9, ry9;

            rx1 = (int)Math.Round((cp[1].X - cp[0].X) * t) + cp[0].X;
            ry1 = (int)Math.Round((cp[1].Y - cp[0].Y) * t) + cp[0].Y;
            rx2 = (int)Math.Round((cp[2].X - cp[1].X) * t) + cp[1].X;
            ry2 = (int)Math.Round((cp[2].Y - cp[1].Y) * t) + cp[1].Y;
            rx3 = (int)Math.Round((cp[3].X - cp[2].X) * t) + cp[2].X;
            ry3 = (int)Math.Round((cp[3].Y - cp[2].Y) * t) + cp[2].Y;
            rx4 = (int)Math.Round((cp[4].X - cp[3].X) * t) + cp[3].X;
            ry4 = (int)Math.Round((cp[4].Y - cp[3].Y) * t) + cp[3].Y;
            rx5 = (int)Math.Round((rx2 - rx1) * t) + rx1;
            ry5 = (int)Math.Round((ry2 - ry1) * t) + ry1;
            rx6 = (int)Math.Round((rx3 - rx2) * t) + rx2;
            ry6 = (int)Math.Round((ry3 - ry2) * t) + ry2;
            rx7 = (int)Math.Round((rx4 - rx3) * t) + rx3;
            ry7 = (int)Math.Round((ry4 - ry3) * t) + ry3;
            rx8 = (int)Math.Round((rx6 - rx5) * t) + rx5;
            ry8 = (int)Math.Round((ry6 - ry5) * t) + ry5;
            rx9 = (int)Math.Round((rx7 - rx6) * t) + rx6;
            ry9 = (int)Math.Round((ry7 - ry6) * t) + ry6;


            resultado.X = (int)Math.Round(((rx9 - rx8) * t) + rx8);
            resultado.Y = (int)Math.Round(((ry9 - ry8) * t) + ry8);
            //panel1.CreateGraphics().DrawLine(Returnpen(Color.Green), rx1, ry1, rx2, ry2);
            //panel1.CreateGraphics().DrawLine(Returnpen(Color.Green), rx2, ry2, rx3, ry3);
            //panel1.CreateGraphics().DrawLine(Returnpen(Color.Green), rx3, ry3, rx4, ry4);
            //panel1.CreateGraphics().DrawLine(Returnpen(Color.Blue), rx5, ry5, rx6, ry6);
            //panel1.CreateGraphics().DrawLine(Returnpen(Color.Blue), rx6, ry6, rx7, ry7);
            //panel1.CreateGraphics().DrawLine(Returnpen(Color.Red), rx8, ry8, rx9, ry9);
            
            return resultado;
        }

    }
}
/*
 *          Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            timeBresenham.Text = String.Format("{0}", ts.TotalMilliseconds);

     
     */
