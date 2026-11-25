using System;
using System.Net.Http.Headers;

namespace Demo.Solid;

/*
  Bir sınıfı miras alan sınıflar, o sınıfın ya da birbirlierinin yerine istisnasız
  kullanılabilir olmalıdır.
*/


public interface IArea
{
    double GetArea();
}
public class Rectangle : IArea
{
    public virtual int Width { get; set; }
    public virtual int Height { get; set; }
    public double GetArea() => Width * Height;
}

public class Square : IArea//: Rectangle
{
    // public override int Width { get => base.Width; set {base.Height = value; base.Width=value;} }
    // public override int Height { get => base.Height; set {base.Height = value; base.Width=value;} }
    public int EdgeLength { get; set; }
    public double GetArea() => Math.Pow(EdgeLength, 2);
}

public class Geometry
{
    public static IArea CreateRect(int unit1, int? unit2 = null)
    {
        if (unit2 is null)
        {
            return new Square { EdgeLength = unit1 };
        }
        return new Rectangle { Width = unit1, Height = unit2.Value };
    }
}
