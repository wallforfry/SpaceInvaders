using System;

namespace SpaceInvaders.EngineFiles
{
    public class Vecteur2D
    {
        public Vecteur2D(Vecteur2D vecteur2D) : this(vecteur2D.X, vecteur2D.Y)
        {
        }

        public Vecteur2D(double x = 0, double y = 0)
        {
            X = x;
            Y = y;
        }

        public double X { get; set; }

        public double Y { get; set; }

        public double Norme => Math.Sqrt(X * X + Y * Y);

        public static Vecteur2D operator +(Vecteur2D a, Vecteur2D b)
        {
            return new Vecteur2D(a.X + b.X, a.Y + b.Y);
        }

        public static Vecteur2D operator -(Vecteur2D a, Vecteur2D b)
        {
            return new Vecteur2D(a.X - b.X, a.Y - b.Y);
        }

        public static Vecteur2D operator +(Vecteur2D a)
        {
            return new Vecteur2D(a.X + a.X, a.Y + a.Y);
        }

        public static Vecteur2D operator *(Vecteur2D a, double b)
        {
            return new Vecteur2D(a.X * b, a.Y * b);
        }

        public static Vecteur2D operator *(double b, Vecteur2D a)
        {
            return new Vecteur2D(b * a.X, b * a.Y);
        }

        public static Vecteur2D operator /(Vecteur2D a, double b)
        {
            return new Vecteur2D(a.X / b, a.Y / b);
        }
    }
}