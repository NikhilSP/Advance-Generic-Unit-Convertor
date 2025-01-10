using UnitConvertor.Contract;
using UnitConvertor.Convertors;
using UnitConvertor.Model.Measurement;

namespace UnitConvertor.Other;

public class MeasurementCollection<T> where T : IUnit
{
    private readonly List<Measurement<T>> _collection = new();

    public IReadOnlyList<Measurement<T>> Values => _collection;

    public MeasurementCollection(Measurement<T> value)
    {
        _collection.Add(value);
    }

    public MeasurementCollection(List<Measurement<T>> values)
    {
        _collection.AddRange(values);
    }

    public void Add(Measurement<T> value)
    {
        _collection.Add(value);
    }

    public void AddRange(IEnumerable<Measurement<T>> values)
    {
        _collection.AddRange(values);
    }

    public void Add<TOther>(Measurement<TOther> other, MeasurementConvertor convertor)
        where TOther : IUnit
    {
        Add(convertor.ConvertTo<TOther, T>(other));
    }

    public void AddRange<TOther>(IEnumerable<Measurement<TOther>> other, MeasurementConvertor convertor)
        where TOther : IUnit
    {
        AddRange(other.Select(convertor.ConvertTo<TOther, T>));
    }

    public void Clear()
    {
        _collection.Clear();
    }

    public Measurement<T> Average()
    {
        return new Measurement<T>(_collection.Select(x => x.Value).Average());
    }

    public Measurement<T> Sum()
    {
        return new Measurement<T>(_collection.Select(x => x.Value).Sum());
    }

    public IUnitRange<T> Range()
    {
        var min = _collection.MinBy(x => x.Value);
        var max = _collection.MaxBy(x => x.Value);

        if (min is null || max is null)
        {
            return new NotDefinedRange<T>();
        }

        return new UnitRange<T>(min, max);
    }
}