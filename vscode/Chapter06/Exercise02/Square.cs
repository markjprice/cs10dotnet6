public class Square : Rectangle
{
  public Square() { }

  public Square(double width) : base(height: width, width: width) { }

  public override double Height
  {
    set
    {
      height = value;
      width = value;
    }
  }

  public override double Width
  {
    set
    {
      height = value;
      width = value;
    }
  }
}