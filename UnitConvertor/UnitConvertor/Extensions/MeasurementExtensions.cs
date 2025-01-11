using UnitConvertor.Contract;
using UnitConvertor.Model.Measurement;
using UnitConvertor.Model.Unit;

namespace UnitConvertor.Extensions;

public static class MeasurementExtensions
{
    public static Measurement<CompoundUnit<T1, T2>> Multiply<T1, T2>(
        this Measurement<T1> m1,
        Measurement<T2> m2) 
        where T1 : IUnit 
        where T2 : IUnit
    {
        var value = m1.Value * m2.Value;
        return new Measurement<CompoundUnit<T1, T2>>(value);
    }

    public static Measurement<CompoundUnit<T1, T2>> Divide<T1, T2>(
        this Measurement<T1> m1,
        Measurement<T2> m2) 
        where T1 : IUnit 
        where T2 : IUnit
    {
        var value = m1.Value / m2.Value;
        return new Measurement<CompoundUnit<T1, T2>>(value);
    }
}