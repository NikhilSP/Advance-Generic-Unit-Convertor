using UnitConvertor.Model.Measurement;

namespace UnitConvertor.Contract;

public interface IUnitRange<T>  where T : IUnit
{
    public Measurement<T> Min { get; }
    public Measurement<T> Max { get; }
}