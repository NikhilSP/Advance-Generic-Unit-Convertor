using UnitConvertor.Contract;
using UnitConvertor.Model.Measurement;

namespace UnitConvertor.Other;

public class MeasurementMath<T1>:IMeasurementMath<T1> 
    where T1:IUnit
{
    public Measurement<T1> Abs(Measurement<T1> value)
    {
        return new Measurement<T1>(Math.Abs(value.Value));
    }

    public Measurement<T1> Min(Measurement<T1> a, Measurement<T1> b)
    {
        return new Measurement<T1>(Math.Min(a.Value,b.Value));
    }

    public Measurement<T1> Max(Measurement<T1> a, Measurement<T1> b)
    {
        return new Measurement<T1>(Math.Max(a.Value,b.Value));
    }

    public Measurement<T1> Ceiling(Measurement<T1> value)
    {
        return new Measurement<T1>(Math.Ceiling(value.Value));
    }

    public Measurement<T1> Floor(Measurement<T1> value)
    {
        return new Measurement<T1>(Math.Floor(value.Value));
    }
}