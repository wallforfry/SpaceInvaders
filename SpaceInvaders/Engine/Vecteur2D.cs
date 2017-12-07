using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    public class Vecteur2D
    {
        private double x;
        private double y;

        public Vecteur2D(Vecteur2D vecteur2D) : this(vecteur2D.X, vecteur2D.Y)
        {
            
        }  
        
        public Vecteur2D(double x = 0, double y = 0)
        {
            this.x = x;
            this.y = y;
        }

        public double X { get {return this.x; } set { this.x = value; } }
        public double Y { get { return this.y; } set { this.y = value; } }
        public double Norme
        {
            get
            {
                return Math.Sqrt(x*x+y*y);
            }

        }

        public static Vecteur2D operator+(Vecteur2D a, Vecteur2D b)
        {
            return new Vecteur2D(a.X + b.X, a.Y + b.Y);
        }

        public static Vecteur2D operator-(Vecteur2D a, Vecteur2D b)
        {
            return new Vecteur2D(a.X - b.X, a.Y - b.Y);
        }

        public static Vecteur2D operator +(Vecteur2D a)
        {
            return new Vecteur2D(a.X + a.X, a.Y+a.Y);
        }

        public static Vecteur2D operator * (Vecteur2D a, double b)
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