using UnitConvertor.Contract;
using UnitConvertor.Enum;
using UnitConvertor.Model.Unit;

namespace UnitConvertor.Model.Measurement;

public class Measurement<T> where T:IUnit
{
    public T? Unit = default;
    public double Value { get; }
    
    public Measurement(double value)
    {
        Value = value;
    }
    
    private Measurement(double value,T unit)
    {
        Value = value;
        Unit = unit;
    }

    public static Measurement<CompoundUnit<T1, T2>> CreateCompoundUnit<T1,T2>(double value, CompoundUnit<T1,T2> unit)
        where T1:IUnit
        where T2:IUnit
    {
        return new Measurement<CompoundUnit<T1, T2>>(value, unit);
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
