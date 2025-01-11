using UnitConvertor.Model.Measurement;

namespace UnitConvertor.Contract;

public interface IMeasurementMath<T1> where T1 : IUnit
{
    Measurements<T1> Abs(Measurements<T1> value);
    Measurements<T1> Min(Measurements<T1> a, Measurements<T1> b);
    Measurements<T1> Max(Measurements<T1> a, Measurements<T1> b);
    Measurements<T1> Ceiling(Measurements<T1> value);
    Measurements<T1> Floor(Measurements<T1> value);
}