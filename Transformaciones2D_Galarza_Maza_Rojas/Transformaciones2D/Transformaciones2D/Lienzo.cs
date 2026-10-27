using System;
using System.Drawing;
namespace Transformaciones2D
{
    public class Lienzo
    {
        public Figura Original { get; set; }
        public Figura Resultado { get; set; }
        public void Dibujar(Graphics g, int ancho, int alto, Font fuente)
        {
            if (Original==null || Resultado==null || ancho<100 || alto<100) return;
            double mx=8,my=8;
            foreach (Figura f in new[] {Original,Resultado})
                foreach (Punto p in f.Vertices) { mx=Math.Max(mx,Math.Abs(p.X)); my=Math.Max(my,Math.Abs(p.Y)); }
            double escala=Math.Min((ancho-110)/(2*mx),(alto-130)/(2*my));
            double paso=Math.Max(1,Math.Ceiling(30/escala));
            float cx=ancho/2f,cy=alto/2f;
            Func<Punto,PointF> pantalla=p=>new PointF((float)(cx+p.X*escala),(float)(cy-p.Y*escala));
            g.SmoothingMode=System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using(Pen rejilla=new Pen(Color.FromArgb(230,235,240)))
            using(Pen eje=new Pen(Color.SlateGray,1.5f))
            {
                float d=(float)(paso*escala);
                for(float x=cx;x<ancho;x+=d) g.DrawLine(rejilla,x,0,x,alto);
                for(float x=cx-d;x>0;x-=d) g.DrawLine(rejilla,x,0,x,alto);
                for(float y=cy;y<alto;y+=d) g.DrawLine(rejilla,0,y,ancho,y);
                for(float y=cy-d;y>0;y-=d) g.DrawLine(rejilla,0,y,ancho,y);
                g.DrawLine(eje,cx,0,cx,alto); g.DrawLine(eje,0,cy,ancho,cy);
            }
            DibujarFigura(g,Original,pantalla,Color.RoyalBlue,"P",-22,fuente);
            DibujarFigura(g,Resultado,pantalla,Color.OrangeRed,"Q",8,fuente);
            g.DrawString("Azul: original    Rojo: transformado",fuente,Brushes.Black,12,12);
            g.DrawString("Cuadrícula: "+paso+" unidades",fuente,Brushes.Black,12,34);
            g.DrawString("0",fuente,Brushes.DimGray,cx+4,cy+4);
            g.DrawString("X",fuente,Brushes.Black,ancho-22,cy+5);
            g.DrawString("Y",fuente,Brushes.Black,cx+5,58);
        }
        private void DibujarFigura(Graphics g,Figura f,Func<Punto,PointF> convertir,Color color,string prefijo,int dy,Font fuente)
        {
            PointF[] ps=Array.ConvertAll(f.Vertices,p=>convertir(p));
            using(Pen pen=new Pen(color,2.5f))
            using(SolidBrush brush=new SolidBrush(color))
            {
                if(ps.Length>2) g.DrawPolygon(pen,ps);
                for(int i=0;i<ps.Length;i++)
                {
                    g.FillEllipse(brush,ps[i].X-4,ps[i].Y-4,8,8);
                    g.DrawString(prefijo+(i+1),fuente,brush,ps[i].X+7,ps[i].Y+dy);
                }
            }
        }
    }
}
