using UnitConvertor.Contract;

namespace UnitConvertor.Model.Unit;

public class CompoundUnit<T1, T2> : IUnit
    where T1 : IUnit
    where T2 : IUnit
{
}