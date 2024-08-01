using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace GameplayAbilitySystem
{
    [CreateAssetMenu(fileName = "GameplayEffect", menuName = "GAS/GameplayEffect", order = 0)]
    public class GameplayEffect: ScriptableObject
    {
        [SerializeReference]public List<Modifier> Modifiers = new ();
        [HideInInspector]public AbilitySystemComponent Source, Target;
        
        public GameplayEffect CreateInstance(GameplayEffect original, AbilitySystemComponent source, AbilitySystemComponent target)
        {
            GameplayEffect copy = ScriptableObject.CreateInstance<GameplayEffect>();
            copy.Initialize(original, source, target);
            return copy;
        }
        
        protected virtual void Initialize(GameplayEffect original, AbilitySystemComponent source, AbilitySystemComponent target)
        {
            foreach (var modifier in original.Modifiers)
            {
                this.Modifiers.Add(modifier.Clone());
            }
            Source = source;
            Target = target;
        }
    }
}