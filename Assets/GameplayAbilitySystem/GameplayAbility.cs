using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameplayAbilitySystem
{
    [CreateAssetMenu(fileName = "GameplayAbility", menuName = "GAS/GameplayAbility", order = 0)]
    public class GameplayAbility: ScriptableObject
    {
        public string Name = Guid.NewGuid().ToString();
        public List<Modifier> Cost;
        public List<GameplayEffect> GameplayEffects;  
        [HideInInspector]public AbilitySystemComponent Owner;
        
        public GameplayAbility CreateInstance(GameplayAbility original, AbilitySystemComponent owner)
        {
            GameplayAbility copy = ScriptableObject.CreateInstance<GameplayAbility>();
            copy.Initialize(original, owner);
            return copy;
        }

        protected virtual void Initialize(GameplayAbility original, AbilitySystemComponent owner)
        {
            Name = original.Name;
            Cost = original.Cost;
            GameplayEffects = original.GameplayEffects;
            Owner = owner;
        }
        
        public bool CanActivateAbility(AbilitySystemComponent abilitySystemComponent, AbilitySystemComponent target)
        {
            //Check Cost
            if (Cost == null || Cost.Count == 0)
            {
                return true;
            }

            List<Modifier> costModifiers = new ();
            foreach (Modifier modifier in Cost)
            {
                if (modifier.Operation == Operation.Add)
                {
                    costModifiers.Add(modifier);
                }
            }
            
            foreach (Modifier cost in costModifiers)
            {
                Attribute targetAttribute =
                    Owner.Attributes.Find((targetAttribute) => targetAttribute.Name == cost.AttributeToModify);

                if (targetAttribute == null)
                {
                    // TODO: Raise cost attribute didn't find event
                    return false;
                }

                if (targetAttribute.CurrentValue + cost.Magnitude < 0)
                {
                    // TODO: Raise cost is not enough event
                    return false;
                }
            }
            return true;
        }

        public void CommitAbility(AbilitySystemComponent abilitySystemComponent, AbilitySystemComponent target)
        {
            PreActivate(abilitySystemComponent, target);
            Activate(abilitySystemComponent, target);
            PostActivate(abilitySystemComponent, target);
        }
        
        private void PreActivate(AbilitySystemComponent abilitySystemComponent, AbilitySystemComponent target)
        {
            //Apply Cooldown
            //Apply Cost
            //ApplyGameplayEffect(Definition.CostEffect, Owner);
            //Apply tags from this GA
        }

        protected virtual void Activate(AbilitySystemComponent abilitySystemComponent, AbilitySystemComponent target)
        {
            //This is default implementation. Override this for more complicated logic.
            //Apply Gameplay Effect
            InstantApplyAllGameplayEffect(target);
        }
        
        private void PostActivate(AbilitySystemComponent abilitySystemComponent, AbilitySystemComponent target)
        {
            //Apply gameplay cue tags, etc.
        }
        
        private void InstantApplyAllGameplayEffect(AbilitySystemComponent target)
        {
            foreach (GameplayEffect effect in GameplayEffects)
            {
                ApplyGameplayEffect(effect.CreateInstance(effect, Owner, target), target);
            }
        }

        private void ApplyGameplayEffect(GameplayEffect effect, AbilitySystemComponent target)
        {
            foreach (Modifier modifier in effect.Modifiers)
            {
                foreach (Attribute attribute in target.Attributes)
                {
                    if (attribute.Name == modifier.AttributeToModify)
                    {
                        attribute.ApplyModifier(modifier, effect);
                    }
                }
            }
        }
    }
}