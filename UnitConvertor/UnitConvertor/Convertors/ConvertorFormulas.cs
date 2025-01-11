using UnitConvertor.Model.Unit;

namespace UnitConvertor.Convertors;

public static class ConvertorFormulas
{
    public static readonly Dictionary<(Type, Type), Func<double, double>> LengthConverters = new()
    {
        { (typeof(Meter), typeof(Feet)), v => v * 3.28084 },
        { (typeof(Feet), typeof(Meter)), v => v * 0.3048 },
        { (typeof(Meter), typeof(KiloMeter)), v => v / 1000 },
        { (typeof(KiloMeter), typeof(Meter)), v => v * 1000 },
        { (typeof(Feet), typeof(Inch)), v => v * 12 },
        { (typeof(Inch), typeof(Feet)), v => v / 12 },
        { (typeof(Meter), typeof(Mile)), v => v / 1609.34 },
        { (typeof(Mile), typeof(Meter)), v => v * 1609.34 },
        // ... add  if needed ...
    };

    public static readonly Dictionary<(Type, Type), Func<double, double>> TemperatureConverters = new()
    {
        { (typeof(Celsius), typeof(Fahrenheit)), v => (v * 9 / 5) + 32 },
        { (typeof(Fahrenheit), typeof(Celsius)), v => (v - 32) * 5 / 9 },
        { (typeof(Celsius), typeof(Kelvin)), v => v + 273.15 },
        { (typeof(Kelvin), typeof(Celsius)), v => v - 273.15 },
        { (typeof(Fahrenheit), typeof(Kelvin)), v => (v - 32) * 5 / 9 + 273.15 },
        { (typeof(Kelvin), typeof(Fahrenheit)), v => (v - 273.15) * 9 / 5 + 32 },
        // ... add  if needed ...
    };

    public static readonly Dictionary<(Type, Type), Func<double, double>> WeightConverters = new()
    {
        { (typeof(Gram), typeof(Pound)), v => v * 0.00220462 }, 
        { (typeof(Pound), typeof(Gram)), v => v / 0.00220462 }, 
        { (typeof(Gram), typeof(KiloGram)), v => v / 1000 },
        { (typeof(KiloGram), typeof(Gram)), v => v * 1000 },
        { (typeof(KiloGram), typeof(Pound)), v => v * 2.20462 },
        { (typeof(Pound), typeof(KiloGram)), v => v / 2.20462 },
        { (typeof(Gram), typeof(Ounce)), v => v * 0.035274 },
        { (typeof(Ounce), typeof(Gram)), v => v / 0.035274 },
        // ... add  if needed ...
    };
}