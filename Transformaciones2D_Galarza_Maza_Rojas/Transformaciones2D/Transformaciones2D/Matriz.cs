using System;
namespace Transformaciones2D
{
    public sealed class Matriz
    {
        private readonly double[,] datos;
        public double this[int fila, int columna] { get { return datos[fila, columna]; } }
        public Matriz(double[,] valores) { datos = (double[,])valores.Clone(); }
        public static Matriz Traslacion(double x, double y)
        { return new Matriz(new double[,] { {1,0,x}, {0,1,y}, {0,0,1} }); }
        public static Matriz Rotacion(double grados)
        {
            double a = grados * Math.PI / 180.0;
            double c = Math.Cos(a), s = Math.Sin(a);
            return new Matriz(new double[,] { {c,-s,0}, {s,c,0}, {0,0,1} });
        }
        public static Matriz Escalamiento(double x, double y)
        { return new Matriz(new double[,] { {x,0,0}, {0,y,0}, {0,0,1} }); }
        public Matriz Multiplicar(Matriz b)
        {
            double[,] c = new double[3,3];
            for (int i=0;i<3;i++)
                for (int j=0;j<3;j++)
                    for (int k=0;k<3;k++) c[i,j] += datos[i,k]*b[k,j];
            return new Matriz(c);
        }
        public Punto Aplicar(Punto punto)
        {
            double[] p = {punto.X,punto.Y,1};
            double[] q = new double[3];
            for (int i=0;i<3;i++)
                for (int j=0;j<3;j++) q[i] += datos[i,j]*p[j];
            return new Punto(q[0],q[1]);
        }
    }
}
