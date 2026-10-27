namespace Transformaciones2D
{
    public struct Punto
    {
        public double X { get; }
        public double Y { get; }
        public Punto(double x, double y) { X = x; Y = y; }
        public override string ToString() { return string.Format("({0:0.###}, {1:0.###})", X, Y); }
    }
}
