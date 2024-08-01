using System;
using System.Collections.Generic;

namespace GameplayAbilitySystem
{
    public class Attribute
    {
        public AttributeName Name;
        public float BaseValue { get; set; }
        public float CurrentValue { get; set; }
        private List<Modifier> _effectModifiers = new ();
        public Action<AttributeName, float, float, GameplayEffect> OnPostAttributeChange;

        public void ApplyModifier(Modifier newModifier, GameplayEffect effect)
        {
            _effectModifiers.Add(newModifier);

            float additive = 0;
            float multiplicative = 1;
            foreach (Modifier modifier in _effectModifiers)
            {
                switch (modifier.Operation)
                {
                    case Operation.Add:
                        additive += modifier.GetValue(effect);
                        break;
                    case Operation.Multiply:
                        multiplicative *= modifier.GetValue(effect);
                        break;
                }
            }

            float oldValue = CurrentValue;
            CurrentValue = (BaseValue + additive) * multiplicative;
            
            OnPostAttributeChange.Invoke(Name, oldValue, CurrentValue, effect);
        }
    }
}