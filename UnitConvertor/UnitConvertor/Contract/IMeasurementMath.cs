using UnitConvertor.Model.Measurement;

namespace UnitConvertor.Contract;

public interface IMeasurementMath<T1> where T1 : IUnit
{
    Measurement<T1> Abs(Measurement<T1> value);
    Measurement<T1> Min(Measurement<T1> a, Measurement<T1> b);
    Measurement<T1> Max(Measurement<T1> a, Measurement<T1> b);
    Measurement<T1> Ceiling(Measurement<T1> value);
    Measurement<T1> Floor(Measurement<T1> value);
}