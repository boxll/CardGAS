using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPattern
{
    [Serializable]
    public class ConditionGroup<T>
    {
        public List<Condition<T>> Conditions;

        public bool AllMet(T argObject)
        {
            return Conditions.Count == 0 || Conditions.All(x => x.IsMet(argObject));
        }

        public bool AnyMet(T argObject)
        {
            return Conditions.Count == 0 || Conditions.Any(x => x.IsMet(argObject));
        }
    }
}