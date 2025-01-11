using UnitConvertor.Contract;
using UnitConvertor.Model.Measurement;

namespace UnitConvertor.Other;

public class UnitRange<T> : IUnitRange<T>
   where T : IUnit
{
   public Measurements<T> Min { get; }
   public Measurements<T> Max { get; }

   public UnitRange(Measurements<T> min, Measurements<T> max)
   {
      Min = min;
      Max = max;
   }
   
   public UnitRange(double min, double max)
   {
      Min = new Measurements<T>(min);
      Max = new Measurements<T>(max);
   }

   public bool Contains(Measurements<T> value)
   {
      return Min.Value <= value.Value && value.Value<= Max.Value;
   }
   
   public bool Overlaps(UnitRange<T> value)
   {
      return Contains(value.Min) || Contains(value.Max);
   }
}