using System;
using DesignPattern;
using Unity.Collections;
using UnityEngine;

namespace GameplayAbilitySystem
{
    [Serializable]
    public class Modifier
    {
        [SerializeField][InspectorReadOnly] private string Name;
        public AttributeName AttributeToModify;
        public Operation Operation;
        public float Magnitude;
        
        public Modifier() { Name = GetType().Name; }

        public virtual float GetValue(GameplayEffect effect)
        {
            return 0;
        }
        
        public Modifier Clone()
        {
            return MemberwiseClone() as Modifier;
        }
    }

    [Serializable]
    public class BasicModifier: Modifier
    { 
        public override float GetValue(GameplayEffect effect)
        {
            return Magnitude;
        }
    }
    
    [Serializable]
    public class AttributeBasedModifier: Modifier
    {
        [Tooltip("Will use target value when false")]
        public bool UseSourceValue = true;
        public AttributeName BaseAttribute;
        [Tooltip("Will use current value when false")]
        public bool UseBaseValue = true;
        
        public override float GetValue(GameplayEffect effect)
        {
            AbilitySystemComponent asc = UseSourceValue ? effect.Source : effect.Target;
            Attribute attribute = asc.Attributes.Find(attribute => attribute.Name == BaseAttribute);
            if (attribute != null)
            {
                float value = UseBaseValue ? attribute.BaseValue : attribute.CurrentValue;
                return value * Magnitude;
            }
            Debug.LogWarning($"No Attribute named{BaseAttribute.Name}");
            return 0;
        }
    }
    
    [Serializable]
    public enum Operation
    {
        Add,
        Multiply
    }
    
    [Serializable]
    public enum ModifierType
    {
        Basic,
        AttributeBased
    }
}