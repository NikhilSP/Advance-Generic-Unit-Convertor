using UnitConvertor.Contract;
using UnitConvertor.Enum;
using UnitConvertor.Model.Unit;

namespace UnitConvertor.Model.Measurement;

public class Measurements<T> where T:IUnit
{
    public T? Unit = default;
    public double Value { get; }
    
    public Measurements(double value)
    {
        Value = value;
    }
    
    public Measurements(double value,T unit)
    {
        Value = value;
        Unit = unit;
    }

    public static Measurements<T> operator +(Measurements<T> a ,Measurements<T> b)
    {
        return new Measurements<T>(a.Value + b.Value);
    }
    
    public static Measurements<T> operator -(Measurements<T> a ,Measurements<T> b)
    {
        return new Measurements<T>(a.Value - b.Value);
    }
    
    public static Measurements<T> operator *(Measurements<T> a ,double b)
    {
        return new Measurements<T>(a.Value * b);
    }
    
    public static Measurements<T> operator /(Measurements<T> a ,double b)
    {
        if (b == 0)
        {
            return new NotDefinedMeasurements<T>();
        }
        
        return new Measurements<T>(a.Value * b);
    }
}
