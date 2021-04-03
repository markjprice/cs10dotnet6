using System.Xml.Serialization;

namespace Packt.Shared
{
    [XmlInclude(typeof(Circle))]
    [XmlInclude(typeof(Rectangle))]
    public abstract class Shape
    {
        public string Colour { get; set; }
        public abstract double Area { get; }
    }
}