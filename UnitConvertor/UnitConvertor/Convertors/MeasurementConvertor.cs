using System.Reflection;
using UnitConvertor.Contract;
using UnitConvertor.Model.Measurement;
using UnitConvertor.Model.Unit;
using Meter = UnitConvertor.Model.Unit.Meter;

namespace UnitConvertor.Convertors;

// Uses reflection for nested compound units
public class MeasurementConvertor
{
    private MethodInfo _convertToMethod;

    public MeasurementConvertor()
    {
        var convertToMethod = typeof(MeasurementConvertor).GetMethod("ConvertCompoundUnitTo");
        _convertToMethod = convertToMethod ?? throw new Exception();
    }

    public Measurements<T2> ConvertTo<T1, T2>(Measurements<T1> measurement)
        where T1 : IUnit
        where T2 : IUnit
    {
        var convertor = GetConvertor<T1, T2>();
        return convertor is null
            ? new NotDefinedMeasurements<T2>()
            : new Measurements<T2>(convertor(measurement.Value));
    }

    public Measurements<CompoundUnit<T3, T4>> ConvertCompoundUnitTo<T1, T2, T3, T4>(
        Measurements<CompoundUnitDivide<T1, T2>> measurement)
        where T1 : IUnit
        where T2 : IUnit
        where T3 : IUnit
        where T4 : IUnit
    {
        if (typeof(T1).IsGenericType && typeof(T1).GetGenericTypeDefinition() == typeof(CompoundUnitDivide<,>))
        {
            if (typeof(T3).IsGenericType && typeof(T3).GetGenericTypeDefinition() == typeof(CompoundUnitDivide<,>))
            {
                Type[] innerTypesT1 = typeof(T1).GetGenericArguments();
                Type firstTypeT1 = innerTypesT1[0];
                Type secondTypeT1 = innerTypesT1[1];

                Type[] innerTypesT3 = typeof(T3).GetGenericArguments();
                Type firstTypeT3 = innerTypesT3[0];
                Type secondTypeT3 = innerTypesT3[1];

                Type[] typeArgs = { firstTypeT1, secondTypeT1, firstTypeT3, secondTypeT3 };

                MethodInfo genericConvertToMethod = _convertToMethod.MakeGenericMethod(typeArgs);

                // Create instances of the inner unit types
                object firstTypeT1Instance = Activator.CreateInstance(firstTypeT1) ?? throw new Exception();
                object secondTypeT1Instance = Activator.CreateInstance(secondTypeT1) ?? throw new Exception();

                object compoundUnitT1 = Activator.CreateInstance(
                    typeof(CompoundUnitDivide<,>).MakeGenericType(firstTypeT1, secondTypeT1)) ?? throw new Exception();

                // Get the type of compoundUnitT1
                Type compoundUnitT1Type = compoundUnitT1.GetType();

                object measurementsInstance = Activator.CreateInstance(
                    typeof(Measurements<>).MakeGenericType(compoundUnitT1Type),
                    new object[] { 1, compoundUnitT1 })!;

                object result = genericConvertToMethod.Invoke(this, new object[]
                {
                    measurementsInstance
                })!;
            }
            else
            {
                // type mismatch
            }
        }

        var firstFactor = ConvertTo<T1, T3>(new Measurements<T1>(1));
        var secondFactor = ConvertTo<T2, T4>(new Measurements<T2>(1));

        if (firstFactor is NotDefinedMeasurements<T3> || secondFactor is NotDefinedMeasurements<T4>)
        {
            return new NotDefinedMeasurements<CompoundUnit<T3, T4>>();
        }

        return new Measurements<CompoundUnit<T3, T4>>(measurement.Value *
            firstFactor.Value / secondFactor.Value);

        // return operation switch
        // {
        //     CompoundUnitOperation.Divide => new Measurement<CompoundUnit<T3, T4>>(measurement.Value *
        //         firstFactor.Value / secondFactor.Value),
        //     CompoundUnitOperation.Multiply => new Measurement<CompoundUnit<T3, T4>>(measurement.Value *
        //         firstFactor.Value * secondFactor.Value),
        //     _ => new NotDefinedMeasurement<CompoundUnit<T3, T4>>()
        // };
    }


    private Func<double, double>? GetConvertor<T1, T2>()
        where T1 : IUnit
        where T2 : IUnit
    {
        if (typeof(T1) == typeof(T2))
        {
            return v => 1;
        }

        var convertors = GetConvertors<T1, T2>();

        if (convertors.Any())
        {
            if (convertors.TryGetValue((typeof(T1), typeof(T2)), out var func))
            {
                return func;
            }
        }

        return null;
    }

    private Dictionary<(Type, Type), Func<double, double>> GetConvertors<T1, T2>()
    {
        if (typeof(ILength).IsAssignableFrom(typeof(T1)) && typeof(ILength).IsAssignableFrom(typeof(T2)))
        {
            return ConvertorFormulas.LengthConverters;
        }

        if (typeof(IWeight).IsAssignableFrom(typeof(T1)) && typeof(IWeight).IsAssignableFrom(typeof(T2)))
        {
            return ConvertorFormulas.WeightConverters;
        }

        if (typeof(ITemperature).IsAssignableFrom(typeof(T1)) && typeof(ITemperature).IsAssignableFrom(typeof(T2)))
        {
            return ConvertorFormulas.TemperatureConverters;
        }

        return new();
    }
}