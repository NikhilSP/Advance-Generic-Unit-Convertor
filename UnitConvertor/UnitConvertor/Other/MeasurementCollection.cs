using UnitConvertor.Contract;
using UnitConvertor.Convertors;
using UnitConvertor.Model.Measurement;

namespace UnitConvertor.Other;

public class MeasurementCollection<T> where T : IUnit
{
    private readonly List<Measurements<T>> _collection = new();

    public IReadOnlyList<Measurements<T>> Values => _collection;

    public MeasurementCollection(Measurements<T> value)
    {
        _collection.Add(value);
    }

    public MeasurementCollection(List<Measurements<T>> values)
    {
        _collection.AddRange(values);
    }

    public void Add(Measurements<T> value)
    {
        _collection.Add(value);
    }

    public void AddRange(IEnumerable<Measurements<T>> values)
    {
        _collection.AddRange(values);
    }

    public void Add<TOther>(Measurements<TOther> other, MeasurementConvertor convertor)
        where TOther : IUnit
    {
        Add(convertor.ConvertTo<TOther, T>(other));
    }

    public void AddRange<TOther>(IEnumerable<Measurements<TOther>> other, MeasurementConvertor convertor)
        where TOther : IUnit
    {
        AddRange(other.Select(convertor.ConvertTo<TOther, T>));
    }

    public void Clear()
    {
        _collection.Clear();
    }

    public Measurements<T> Average()
    {
        return new Measurements<T>(_collection.Select(x => x.Value).Average());
    }

    public Measurements<T> Sum()
    {
        return new Measurements<T>(_collection.Select(x => x.Value).Sum());
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