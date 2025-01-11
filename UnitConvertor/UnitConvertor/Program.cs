using UnitConvertor.Convertors;
using UnitConvertor.Model.Measurement;
using UnitConvertor.Model.Unit;

namespace UnitConvertor;

internal class Program
{
    static void Main(string[] args)
    {
        var first = new Measurements<Meter>(10);

        var convertor = new MeasurementConvertor();

        var result = convertor.ConvertTo<Meter, Feet>(first);

        var speedA = new Measurements<CompoundUnit<Meter, Second>>(10);
        var speedB = new Measurements<CompoundUnit<Meter, Second>>(10);

        var acceleration = new Measurements<CompoundUnit<CompoundUnit<Meter, Second>, Second>>(5);

        var meter = new Meter();
        var second = new Second();

        var value1 = new Measurements<CompoundUnitDivide<CompoundUnitDivide<CompoundUnitDivide<Meter, Second>, Second>, Second>>(
            10,
            new CompoundUnitDivide<CompoundUnitDivide<CompoundUnitDivide<Meter, Second>, Second>, Second>());

        convertor
            .ConvertCompoundUnitTo<CompoundUnitDivide<CompoundUnitDivide<Meter, Second>, Second>, Second,
                CompoundUnitDivide<CompoundUnitDivide<Feet, Second>, Second>, Second>(value1);
    }
}

// first : CompoundUnitDivide<Meter, Second> , second: Second>