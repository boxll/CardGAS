using UnityEngine;

namespace DesignPattern
{
    public abstract class Condition<T> : ScriptableObject
    {
        public abstract bool IsMet(T argObject);
    }
    
    public struct NoParams
    {
    }
}