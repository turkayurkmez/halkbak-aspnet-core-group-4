using System;

namespace Demo.Solid;

public interface IMath
{
    double Sum(int x, int y);
    double Subtract(int x, int y);
    double Multiply(int x, int y);
    double Divide(int x, int y);
}

public interface IAdvancedMath : IMath
{
    double Pow(int x, int y);
    double SqrRoot(int x);
}

public class SimpleMath : IMath
{
    public double Divide(int x, int y)
    {
        return x / y;
    }

    public double Multiply(int x, int y)
    {
        return x * y;
    } 

    public double Subtract(int x, int y)
    {
        return x - y;
    }

    public double Sum(int x, int y)
    {
        return x + y;
    }
}

public class ComplexMath : IAdvancedMath
{
    public double Divide(int x, int y)
    {
        throw new NotImplementedException();
    }

    public double Multiply(int x, int y)
    {
        throw new NotImplementedException();
    }

    public double Pow(int x, int y)
    {
        throw new NotImplementedException();
    }

    public double SqrRoot(int x)
    {
        throw new NotImplementedException();
    }

    public double Subtract(int x, int y)
    {
        throw new NotImplementedException();
    }

    public double Sum(int x, int y)
    {
        throw new NotImplementedException();
    }
}

public class Calculator(IMath math)
{
    //public double SqrRoot(double value) => math.SqrRoot((int)value);
}