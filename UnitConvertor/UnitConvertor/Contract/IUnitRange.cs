using UnitConvertor.Model.Measurement;

namespace UnitConvertor.Contract;

public interface IUnitRange<T>  where T : IUnit
{
    public Measurements<T> Min { get; }
    public Measurements<T> Max { get; }
}