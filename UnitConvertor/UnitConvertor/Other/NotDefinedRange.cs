using UnitConvertor.Contract;
using UnitConvertor.Model.Measurement;

namespace UnitConvertor.Other;

public class NotDefinedRange<T>:IUnitRange<T>
           where T:IUnit
{
    public Measurements<T> Min => new Measurements<T>(double.NaN);
    public Measurements<T> Max => new Measurements<T>(double.NaN);
}