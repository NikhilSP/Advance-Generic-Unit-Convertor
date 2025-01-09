
using UnitConvertor.Contract;
using UnitConvertor.Model.Measurement;

namespace UnitConvertor.Convertors;

public class MeasurementConvertor
{
    public Measurement<T2> ConvertTo<T1, T2>(Measurement<T1> measurement)
        where T1 :  IUnit 
        where T2 :  IUnit
    {
        return null;
    }
}