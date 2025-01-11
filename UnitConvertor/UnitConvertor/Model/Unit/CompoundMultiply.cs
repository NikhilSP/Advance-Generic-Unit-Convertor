using UnitConvertor.Contract;
using UnitConvertor.Enum;

namespace UnitConvertor.Model.Unit;

public class CompoundUnitMultiply<T1, T2> : CompoundUnit<T1,T2>
    where T1 : IUnit
    where T2 : IUnit
{
    public CompoundUnitMultiply()
    {
        
    }
}