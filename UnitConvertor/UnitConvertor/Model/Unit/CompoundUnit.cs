using UnitConvertor.Contract;
using UnitConvertor.Enum;

namespace UnitConvertor.Model.Unit;

public class CompoundUnit<T1, T2> : IUnit
    where T1 : IUnit
    where T2 : IUnit
{
    public CompoundUnit(CompoundUnitOperation operation)
    {
        Operation = operation;
    }

    public CompoundUnitOperation Operation { get; }
    
}