using System.Reflection;
using UnitConvertor.Contract;
using UnitConvertor.Model.Measurement;
using UnitConvertor.Model.Unit;
using Meter = UnitConvertor.Model.Unit.Meter;

namespace UnitConvertor.Convertors;

// Uses reflection for nested compound units
public class MeasurementConvertor
{
    private readonly MethodInfo _convertCompoundDivideUnitTo;
    private readonly MethodInfo _convertCompoundMultiplyUnitTo;

    public MeasurementConvertor()
    {
        _convertCompoundDivideUnitTo = typeof(MeasurementConvertor).GetMethod("ConvertCompoundDivideUnitTo") ?? throw new Exception();
        _convertCompoundMultiplyUnitTo = typeof(MeasurementConvertor).GetMethod("ConvertCompoundMultiplyUnitTo") ?? throw new Exception();
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
    
    public Measurements<CompoundUnitDivide<T3, T4>> ConvertCompoundDivideUnitTo<T1, T2, T3, T4>(
        Measurements<CompoundUnitDivide<T1, T2>> measurement)
        where T1 : IUnit
        where T2 : IUnit
        where T3 : IUnit
        where T4 : IUnit
    {
        var firstFactor = GetConversionFactor<T1, T3>();
        var secondFactor = GetConversionFactor<T2, T4>();

        if (firstFactor is NotDefinedMeasurements<T3> || secondFactor is NotDefinedMeasurements<T4>)
        {
            return new NotDefinedMeasurements<CompoundUnitDivide<T3, T4>>();
        }

        return new Measurements<CompoundUnitDivide<T3, T4>>(measurement.Value *
            firstFactor.Value / secondFactor.Value);
    }
    
    public Measurements<CompoundUnitMultiply<T3, T4>> ConvertCompoundMultiplyUnitTo<T1, T2, T3, T4>(
        Measurements<CompoundUnitMultiply<T1, T2>> measurement)
        where T1 : IUnit
        where T2 : IUnit
        where T3 : IUnit
        where T4 : IUnit
    {
        var firstFactor = GetConversionFactor<T1, T3>();
        var secondFactor = GetConversionFactor<T2, T4>();

        if (firstFactor is NotDefinedMeasurements<T3> || secondFactor is NotDefinedMeasurements<T4>)
        {
            return new NotDefinedMeasurements<CompoundUnitMultiply<T3, T4>>();
        }

        return new Measurements<CompoundUnitMultiply<T3, T4>>(measurement.Value *
            firstFactor.Value * secondFactor.Value);
    }

    public Measurements<CompoundUnit<T3, T4>> ConvertCompoundUnitTo<T1, T2, T3, T4>(
        Measurements<CompoundUnit<T1, T2>> measurement)
        where T1 : IUnit
        where T2 : IUnit
        where T3 : IUnit
        where T4 : IUnit
    {
        var firstFactor = GetConversionFactor<T1, T3>();
        var secondFactor = GetConversionFactor<T2, T4>();

        if (firstFactor is NotDefinedMeasurements<T3> || secondFactor is NotDefinedMeasurements<T4>)
        {
            return new NotDefinedMeasurements<CompoundUnit<T3, T4>>();
        }

        return new Measurements<CompoundUnit<T3, T4>>(measurement.Value *
                                                              firstFactor.Value / secondFactor.Value);
    }

    private Measurements<TUnit2> GetConversionFactor<TUnit1, TUnit2>()
        where TUnit1 : IUnit
        where TUnit2 : IUnit
    {
        if (typeof(TUnit1).IsGenericType && typeof(TUnit1).GetGenericTypeDefinition() == typeof(CompoundUnitDivide<,>))
        {
            if (typeof(TUnit2).IsGenericType &&
                typeof(TUnit2).GetGenericTypeDefinition() == typeof(CompoundUnitDivide<,>))
            {
                Type[] innerTypesT1 = typeof(TUnit1).GetGenericArguments();
                Type firstTypeT1 = innerTypesT1[0];
                Type secondTypeT1 = innerTypesT1[1];

                Type[] innerTypesT3 = typeof(TUnit2).GetGenericArguments();
                Type firstTypeT3 = innerTypesT3[0];
                Type secondTypeT3 = innerTypesT3[1];

                Type[] typeArgs = { firstTypeT1, secondTypeT1, firstTypeT3, secondTypeT3 };

                MethodInfo genericConvertToMethod = _convertCompoundDivideUnitTo.MakeGenericMethod(typeArgs);

                object compoundUnitT1 = Activator.CreateInstance(
                    typeof(CompoundUnitDivide<,>).MakeGenericType(firstTypeT1, secondTypeT1)) ?? throw new Exception();

                // Get the type of compoundUnitT1
                Type compoundUnitT1Type = compoundUnitT1.GetType();

                object measurementsInstance = Activator.CreateInstance(
                    typeof(Measurements<>).MakeGenericType(compoundUnitT1Type),
                    new object[] { 1, compoundUnitT1 })!;

                var result = genericConvertToMethod.Invoke(this, new object[]
                {
                    measurementsInstance
                })!;

                double value = (double)result.GetType().GetProperty("Value")!.GetValue(result)!;

                return new Measurements<TUnit2>(value);
            }

            return new NotDefinedMeasurements<TUnit2>();
        }

        if (typeof(TUnit1).IsGenericType &&
            typeof(TUnit1).GetGenericTypeDefinition() == typeof(CompoundUnitMultiply<,>))
        {
            if (typeof(TUnit2).IsGenericType &&
                typeof(TUnit2).GetGenericTypeDefinition() == typeof(CompoundUnitMultiply<,>))
            {
                Type[] innerTypesT1 = typeof(TUnit1).GetGenericArguments();
                Type firstTypeT1 = innerTypesT1[0];
                Type secondTypeT1 = innerTypesT1[1];

                Type[] innerTypesT3 = typeof(TUnit2).GetGenericArguments();
                Type firstTypeT3 = innerTypesT3[0];
                Type secondTypeT3 = innerTypesT3[1];

                Type[] typeArgs = { firstTypeT1, secondTypeT1, firstTypeT3, secondTypeT3 };

                MethodInfo genericConvertToMethod = _convertCompoundMultiplyUnitTo.MakeGenericMethod(typeArgs);

                object compoundUnitT1 = Activator.CreateInstance(
                                            typeof(CompoundUnitMultiply<,>).MakeGenericType(firstTypeT1,
                                                secondTypeT1)) ??
                                        throw new Exception();

                // Get the type of compoundUnitT1
                Type compoundUnitT1Type = compoundUnitT1.GetType();

                object measurementsInstance = Activator.CreateInstance(
                    typeof(Measurements<>).MakeGenericType(compoundUnitT1Type),
                    new object[] { 1, compoundUnitT1 })!;

                var result = genericConvertToMethod.Invoke(this, new object[]
                {
                    measurementsInstance
                })!;

                double value = (double)result.GetType().GetProperty("Value")!.GetValue(result)!;

                return new Measurements<TUnit2>(value);
            }

            return new NotDefinedMeasurements<TUnit2>();
        }

        return ConvertTo<TUnit1, TUnit2>(new Measurements<TUnit1>(1));
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