using System;
using System.Text;
using System.Windows.Forms;
namespace Transformaciones2D
{
    public partial class Form1 : Form
    {
        private readonly Lienzo dibujo = new Lienzo();

        public Form1()
        {
            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            lienzo.Paint += DibujarLienzo;
            lienzo.Resize += (s, e) => lienzo.Invalidate();
            cmbFigura.Items.AddRange(new object[] {"Punto de prueba","Estación de comunicación","Instalación","Vehículo"});
            cmbOperacion.Items.AddRange(new object[] {"Traslación","Rotación","Escalamiento","Rotar y después trasladar","Trasladar y después rotar"});
            Configurar(numTx,-100,100,5); Configurar(numTy,-100,100,-1); Configurar(numAngulo,-360,360,90); Configurar(numSx,0.1m,10,2); Configurar(numSy,0.1m,10,3);
            cmbFigura.SelectedIndex=0; cmbOperacion.SelectedIndex=0;
            btnAplicar.Click+=(s,e)=>Aplicar();
            cmbOperacion.SelectedIndexChanged+=(s,e)=>ActivarCampos();
            ActivarCampos(); Aplicar();
        }
        private void Configurar(NumericUpDown n,decimal min,decimal max,decimal valor)
        { n.Minimum=min; n.Maximum=max; n.DecimalPlaces=2; n.Increment=0.1m; n.Value=valor; }
        private void ActivarCampos()
        {
            int op=cmbOperacion.SelectedIndex;
            numTx.Enabled=numTy.Enabled=op==0 || op>=3;
            numAngulo.Enabled=op==1 || op>=3;
            numSx.Enabled=numSy.Enabled=op==2;
        }
        private Matriz ObtenerMatriz()
        {
            Matriz t=Matriz.Traslacion((double)numTx.Value,(double)numTy.Value);
            Matriz r=Matriz.Rotacion((double)numAngulo.Value);
            switch(cmbOperacion.SelectedIndex)
            {
                case 0:return t;
                case 1:return r;
                case 2:return Matriz.Escalamiento((double)numSx.Value,(double)numSy.Value);
                case 3:return t.Multiplicar(r);
                default:return r.Multiplicar(t);
            }
        }
        private void Aplicar()
        {
            ValidateChildren();
            Matriz m=ObtenerMatriz();
            dibujo.Original=Figura.Crear(cmbFigura.SelectedIndex);
            dibujo.Resultado=dibujo.Original.Transformar(m);
            StringBuilder b=new StringBuilder();
            b.AppendLine(cmbOperacion.Text); b.AppendLine(); b.AppendLine("MATRIZ APLICADA");
            for(int i=0;i<3;i++) { for(int j=0;j<3;j++) b.AppendFormat("{0,10:0.###}",Math.Abs(m[i,j])<1e-12?0:m[i,j]); b.AppendLine(); }
            b.AppendLine(); b.AppendLine("ORIGINAL              TRANSFORMADO");
            for(int i=0;i<dibujo.Original.Vertices.Length;i++) b.AppendLine("P"+(i+1)+" "+dibujo.Original.Vertices[i]+"  ->  Q"+(i+1)+" "+dibujo.Resultado.Vertices[i]);
            b.AppendLine(); b.AppendLine("P' = M · P");
            txtResultados.Text=b.ToString(); lienzo.Invalidate();
        }
        private void DibujarLienzo(object sender, PaintEventArgs e)
        {
            dibujo.Dibujar(e.Graphics, lienzo.Width, lienzo.Height, lienzo.Font);
        }
    }
}
