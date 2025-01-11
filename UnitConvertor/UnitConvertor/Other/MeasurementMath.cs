using UnitConvertor.Contract;
using UnitConvertor.Model.Measurement;

namespace UnitConvertor.Other;

public class MeasurementMath<T1>:IMeasurementMath<T1> 
    where T1:IUnit
{
    public Measurements<T1> Abs(Measurements<T1> value)
    {
        return new Measurements<T1>(Math.Abs(value.Value));
    }

    public Measurements<T1> Min(Measurements<T1> a, Measurements<T1> b)
    {
        return new Measurements<T1>(Math.Min(a.Value,b.Value));
    }

    public Measurements<T1> Max(Measurements<T1> a, Measurements<T1> b)
    {
        return new Measurements<T1>(Math.Max(a.Value,b.Value));
    }

    public Measurements<T1> Ceiling(Measurements<T1> value)
    {
        return new Measurements<T1>(Math.Ceiling(value.Value));
    }

    public Measurements<T1> Floor(Measurements<T1> value)
    {
        return new Measurements<T1>(Math.Floor(value.Value));
    }
}