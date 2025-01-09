using UnitConvertor.Contract;

namespace UnitConvertor.Model.Measurement;

public class NotDefinedMeasurement<T>() : Measurement<T>(0)
    where T : IUnit;