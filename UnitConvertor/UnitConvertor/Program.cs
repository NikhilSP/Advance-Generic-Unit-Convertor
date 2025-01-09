using UnitConvertor.Convertors;
using UnitConvertor.Model.Measurement;
using UnitConvertor.Model.Unit;

namespace UnitConvertor;

internal class Program
{
    static void Main(string[] args)
    {
        var first = new Measurement<Meter>(10);

        var convertor = new MeasurementConvertor();

        var result = convertor.ConvertTo<Meter, Feet>(first);

    }
}
