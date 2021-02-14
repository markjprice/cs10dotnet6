public abstract class Shape
{
  // fields
  protected double height;
  protected double width;

  // properties
  public virtual double Height
  {
    get
    {
      return height;
    }
    set
    {
      height = value;
    }
  }

  public virtual double Width
  {
    get
    {
      return width;
    }
    set
    {
      width = value;
    }
  }

  // Area must be implemented by derived classes
  // as a read-only property
  public abstract double Area { get; }
}