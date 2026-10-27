namespace Transformaciones2D
{
    public sealed class Figura
    {
        public Punto[] Vertices { get; }
        public Figura(params Punto[] vertices) { Vertices = vertices; }
        public Figura Transformar(Matriz m)
        {
            Punto[] nuevos = new Punto[Vertices.Length];
            for (int i=0;i<nuevos.Length;i++) nuevos[i]=m.Aplicar(Vertices[i]);
            return new Figura(nuevos);
        }
        public static Figura Crear(int indice)
        {
            if (indice==0) return new Figura(new Punto(2,3));
            if (indice==1) return new Figura(new Punto(1,1),new Punto(4,1),new Punto(4,3),new Punto(2.5,5),new Punto(1,3));
            if (indice==2) return new Figura(new Punto(1,1),new Punto(4,1),new Punto(4,3),new Punto(1,3));
            return new Figura(new Punto(1,1),new Punto(4,1),new Punto(5,2),new Punto(4,3),new Punto(1,3));
        }
    }
}
