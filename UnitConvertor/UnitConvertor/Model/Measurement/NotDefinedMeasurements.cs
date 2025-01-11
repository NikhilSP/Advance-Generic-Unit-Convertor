using UnitConvertor.Contract;

namespace UnitConvertor.Model.Measurement;

public class NotDefinedMeasurements<T>() : Measurements<T>(0)
    where T : IUnit;