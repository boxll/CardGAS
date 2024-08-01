using System.Collections.Generic;
using System.Linq;
using CardGameUtils;
using GameplayAbilitySystem;
using UnityEngine.Events;

namespace MahjongRoguelike.Scripts
{
    public class EncounterCard : Card
    {
        public List<AttributeName> RequiredAttributesToComplete;
        public UnityEvent OnEncounterComplete;

        protected override void HandleAttributeChange(AttributeName attributeName, float oldValue, float newValue, GameplayEffect gameplayEffect)
        {
            if (newValue <= 0 && RequiredAttributesToComplete.Any(x => x == attributeName) && IsEncounterComplete())
            {
                OnEncounterComplete.Invoke();
            }
        }

        private bool IsEncounterComplete()
        {
            foreach (AttributeName requiredAttributeName in RequiredAttributesToComplete)
            {
                Attribute requiredAttribute = Asc.GetAttribute(requiredAttributeName);
                if (requiredAttribute is { CurrentValue: > 0 })
                {
                    return false;
                }
            }
            return true;
        }
    }
}