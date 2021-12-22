namespace Packt.Shared;

public class PersonComparer : IComparer<Person?>
{
  public int Compare(Person? x, Person? y)
  {
    if ((x is not null) && (y is not null))
    {
      if (x.Name is null)
      {
        // if both x and y person instances have null
        // Name values then they occur at the same position
        if (y.Name is null) return 0;

        // else x follows y
        return 1;
      }
      else
      {
        if (y.Name is null)
        {
          return -1;
        }
      }

      // if both Name values are not null...

      // Compare the Name lengths...
      int result = x.Name.Length
        .CompareTo(y.Name.Length);

      /// ...if they are equal... 
      if (result == 0)
      {
        // ...then compare by the Names...
        return x.Name.CompareTo(y.Name);
      }
      else
      {
        // ...otherwise compare by the lengths.
        return result;
      }
    }
    else if ((x is not null) && (y is null))
    {
      return -1; // x precedes y
    }
    else if ((x is null) && (y is not null))
    {
      return 1; // x follows y
    }
    else
    {
      return 0; // x and y are at same position
    }
  }
}
