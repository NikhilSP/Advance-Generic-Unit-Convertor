using UnitConvertor.Contract;
using UnitConvertor.Model.Measurement;

namespace UnitConvertor.Other;

public class PrecisionHandler<TUnit> where TUnit : IUnit
{
    public Measurements<TUnit> Round(Measurements<TUnit> value, int decimals)
    {
        return new Measurements<TUnit>(Math.Round(value.Value, decimals));
    }

    public Measurements<TUnit> Truncate(Measurements<TUnit> value, int decimals)
    {
        var multiplier = Math.Pow(10, decimals);
        return new Measurements<TUnit>(Math.Truncate(value.Value * multiplier) / multiplier);
    }

    public bool IsApproximatelyEqual(Measurements<TUnit> a, Measurements<TUnit> b, double tolerance)
    {
        return Math.Abs(a.Value - b.Value) < tolerance;
    }
}