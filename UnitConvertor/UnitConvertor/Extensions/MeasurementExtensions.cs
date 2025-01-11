using UnitConvertor.Contract;
using UnitConvertor.Model.Measurement;
using UnitConvertor.Model.Unit;

namespace UnitConvertor.Extensions;

public static class MeasurementExtensions
{
    public static Measurements<CompoundUnit<T1, T2>> Multiply<T1, T2>(
        this Measurements<T1> m1,
        Measurements<T2> m2) 
        where T1 : IUnit 
        where T2 : IUnit
    {
        var value = m1.Value * m2.Value;
        return new Measurements<CompoundUnit<T1, T2>>(value);
    }

    public static Measurements<CompoundUnit<T1, T2>> Divide<T1, T2>(
        this Measurements<T1> m1,
        Measurements<T2> m2) 
        where T1 : IUnit 
        where T2 : IUnit
    {
        var value = m1.Value / m2.Value;
        return new Measurements<CompoundUnit<T1, T2>>(value);
    }
}