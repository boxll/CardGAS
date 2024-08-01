using System;
using System.Globalization;
using GameplayAbilitySystem;
using TMPro;
using UnityEngine;

namespace MahjongRoguelike.Scripts
{
    
public class AttributeValueDisplay : MonoBehaviour
    {
        public TextMeshPro ValueLabel;
        public AttributeName AttributeName;

        private void Start()
        {
            AbilitySystemComponent asc = GetComponentInParent<AbilitySystemComponent>();
            if (asc != null)
            {
                asc.OnAttributeChanged += HandleAttributeChange;
                foreach (var attribute in asc.Attributes)
                {
                    if (attribute.Name == AttributeName)
                    {
                        UpdateValueLabel(attribute.CurrentValue);
                    }
                }
            }
        }

        private void HandleAttributeChange(AttributeName attributeName, float oldValue, float newValue,
            GameplayEffect gameplayEffect)
        {
            if (AttributeName != attributeName)
            {
                return;
            }

            UpdateValueLabel(newValue);
        }

        private void UpdateValueLabel(float newValue)
        {
            ValueLabel.text = newValue.ToString(CultureInfo.CurrentCulture);
        }
    }
}