using UnitConvertor.Contract;
using UnitConvertor.Model.Measurement;

namespace UnitConvertor.Other;

public class NotDefinedRange<T>:IUnitRange<T>
           where T:IUnit
{
    public Measurement<T> Min => new Measurement<T>(double.NaN);
    public Measurement<T> Max => new Measurement<T>(double.NaN);
}