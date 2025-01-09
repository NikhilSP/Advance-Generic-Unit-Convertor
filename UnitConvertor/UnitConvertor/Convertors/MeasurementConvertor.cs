
using UnitConvertor.Contract;
using UnitConvertor.Model.Measurement;
using UnitConvertor.Model.Unit;

namespace UnitConvertor.Convertors;

public class MeasurementConvertor
{
    private readonly Dictionary<(Type,Type),Func<double,double>> _lengthConverters = new()
    {
        {(typeof(Meter), typeof(Feet)), v => v * 3.28084},
        {(typeof(Feet), typeof(Meter)), v => v * 0.3048},
    };
    
    private readonly Dictionary<(Type,Type),Func<double,double>> _temperatureConverters = new()
    {
        {(typeof(Celsius), typeof(Fahrenheit)), v => (v * 9/5) + 32},
        {(typeof(Fahrenheit), typeof(Celsius)), v => (v - 32) * 5/9}
    };
    
    private readonly Dictionary<(Type,Type),Func<double,double>> _weightConverters = new()
    {
        {(typeof(Gram), typeof(Pound)), v => v * 2.20462},
        {(typeof(Pound), typeof(Gram)), v => v * 0.453592}
    };
    
    public Measurement<T2> ConvertTo<T1, T2>(Measurement<T1> measurement)
        where T1 :  IUnit 
        where T2 :  IUnit
    {
        var convertors = GetConvertors<T1,T2>();

        if (convertors.Any())
        {
            if (convertors.TryGetValue((typeof(T1), typeof(T2)), out var func))
            {
                return new Measurement<T2>(func(measurement.Value));
            }
        }
        
        return new NotDefinedMeasurement<T2>();
    }

    private Dictionary<(Type, Type), Func<double, double>> GetConvertors<T1,T2>()
    {
        if (typeof(ILength).IsAssignableFrom(typeof(T1)) && typeof(ILength).IsAssignableFrom(typeof(T2)))
        {
            return _lengthConverters;
        }
        else  if (typeof(IWeight).IsAssignableFrom(typeof(T1)) && typeof(IWeight).IsAssignableFrom(typeof(T2)))
        {
            return _weightConverters;
        }
        else  if (typeof(ITemperature).IsAssignableFrom(typeof(T1)) && typeof(ITemperature).IsAssignableFrom(typeof(T2)))
        {
            return _temperatureConverters;
        }

        return new();
    }
}