using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameplayAbilitySystem
{
    public class AbilitySystemComponent : MonoBehaviour
    {
        public List<AttributeValue> InitialAttribute;
        public List<Attribute> Attributes = new();
        public Action<AttributeName, float, float, GameplayEffect> OnAttributeChanged;
        
        public List<GameplayAbility> InitialGameplayAbilities;
        [HideInInspector]public List<GameplayAbility> GrantedGameplayAbilities = new();

        private void Awake()
        {
            foreach (AttributeValue attribute in InitialAttribute)
            {
                Attributes.Add(new Attribute { Name = attribute.Name, BaseValue = attribute.Value, CurrentValue = attribute.Value});
            }
            foreach (GameplayAbility ability in InitialGameplayAbilities)
            {
                GiveAbility(ability);
            }
            
            Attributes.ForEach(x => x.OnPostAttributeChange += (attributeName, oldValue, newValue, ge) => { OnAttributeChanged?.Invoke(attributeName, oldValue, newValue, ge); });
        }

        public Attribute GetAttribute(AttributeName name)
        {
            return Attributes.Find((attribute) => attribute.Name == name);
        }
        
        public void GiveAbility(GameplayAbility ability) {
            GrantedGameplayAbilities.Add(ability.CreateInstance(ability, this));
            //OnGameplayAbilityGiven?.Invoke();
        }
        
        public void RemoveAbility(GameplayAbility ability) {
            //ga.DeactivateAbility(null);
            GrantedGameplayAbilities.Remove(ability);
            //OnGameplayAbilityRemoved?.Invoke(ability);
        }

        public void TryActivateAbility(string abilityName, AbilitySystemComponent target)
        {
            GameplayAbility ability = GrantedGameplayAbilities.Find(ability => ability.name == abilityName);

            if (ability == null)
            {
                ability = GrantedGameplayAbilities.Find(ability => ability.name.Contains(abilityName));
            }
            
            if (ability == null) {

                Debug.Log($"No granted Ability named {abilityName}");
                return;
            }
            TryActivateAbility(ability, target);
        }
        public void TryActivateAbility(GameplayAbility ability, AbilitySystemComponent target) 
        {
            if (!ability.CanActivateAbility(this, target)) return;

            ability.CommitAbility(this, target);
        }
    }
    
    [Serializable]
    public struct AttributeValue
    {
        public AttributeName Name;
        public float Value;
    }
}