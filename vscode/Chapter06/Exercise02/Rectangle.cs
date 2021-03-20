public class Rectangle : Shape
{
  public Rectangle() { }

  public Rectangle(double height, double width) 
  { 
    this.height = height;
    this.width = width;
  }

  public override double Area
  {
    get
    {
      return height * width;
    }
  }
}