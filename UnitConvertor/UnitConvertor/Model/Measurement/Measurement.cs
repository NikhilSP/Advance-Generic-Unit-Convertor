using UnitConvertor.Contract;

namespace UnitConvertor.Model.Measurement;

public class Measurement<T> where T:IUnit
{
    public double Value { get; }

    public Measurement(double value)
    {
        Value = value;
    }

    public static Measurement<T> operator +(Measurement<T> a ,Measurement<T> b)
    {
        return new Measurement<T>(a.Value + b.Value);
    }
    
    public static Measurement<T> operator -(Measurement<T> a ,Measurement<T> b)
    {
        return new Measurement<T>(a.Value - b.Value);
    }
    
    public static Measurement<T> operator *(Measurement<T> a ,double b)
    {
        return new Measurement<T>(a.Value * b);
    }
    
    public static Measurement<T> operator /(Measurement<T> a ,double b)
    {
        if (b == 0)
        {
            return new NotDefinedMeasurement<T>();
        }
        
        return new Measurement<T>(a.Value * b);
    }
}
