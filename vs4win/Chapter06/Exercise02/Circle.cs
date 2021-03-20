public class Circle : Square
{
  public Circle() { }

  public Circle(double radius) : base(width: radius * 2) { }

  public double Radius
  {
    get
    {
      return height / 2;
    }
    set
    {
      Height = value * 2;
    }
  }

  public override double Area
  {
    get
    {
      var radius = height / 2;
      return System.Math.PI * radius * radius ;
    }
  }
}