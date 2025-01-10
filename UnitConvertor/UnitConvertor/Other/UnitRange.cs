using UnitConvertor.Contract;
using UnitConvertor.Model.Measurement;

namespace UnitConvertor.Other;

public class UnitRange<T> : IUnitRange<T>
   where T : IUnit
{
   public Measurement<T> Min { get; }
   public Measurement<T> Max { get; }

   public UnitRange(Measurement<T> min, Measurement<T> max)
   {
      Min = min;
      Max = max;
   }
   
   public UnitRange(double min, double max)
   {
      Min = new Measurement<T>(min);
      Max = new Measurement<T>(max);
   }

   public bool Contains(Measurement<T> value)
   {
      return Min.Value <= value.Value && value.Value<= Max.Value;
   }
   
   public bool Overlaps(UnitRange<T> value)
   {
      return Contains(value.Min) || Contains(value.Max);
   }
}