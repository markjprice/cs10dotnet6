using System;

namespace Packt.Shared
{
    public class Circle : Shape
    {
        public override double Area
        {
            get
            {
                return Math.PI * Radius * Radius;
            }
        }

        public double Radius { get; set; }
    }
}