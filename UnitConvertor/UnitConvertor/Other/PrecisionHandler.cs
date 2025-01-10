using UnitConvertor.Contract;
using UnitConvertor.Model.Measurement;

namespace UnitConvertor.Other;

public class PrecisionHandler<TUnit> where TUnit : IUnit
{
    public Measurement<TUnit> Round(Measurement<TUnit> value, int decimals)
    {
        return new Measurement<TUnit>(Math.Round(value.Value, decimals));
    }

    public Measurement<TUnit> Truncate(Measurement<TUnit> value, int decimals)
    {
        var multiplier = Math.Pow(10, decimals);
        return new Measurement<TUnit>(Math.Truncate(value.Value * multiplier) / multiplier);
    }

    public bool IsApproximatelyEqual(Measurement<TUnit> a, Measurement<TUnit> b, double tolerance)
    {
        return Math.Abs(a.Value - b.Value) < tolerance;
    }
}