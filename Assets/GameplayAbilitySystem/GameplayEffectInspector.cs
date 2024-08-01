using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace GameplayAbilitySystem
{
#if UNITY_EDITOR
    [CustomEditor(typeof(GameplayEffect))]
    public class GameplayEffectInspector : Editor
    {
        public ModifierType ModifierType;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            ModifierType = (ModifierType) EditorGUILayout.EnumPopup("ModifierType", ModifierType);

            if (GUILayout.Button("Add Modifier"))
            {
                GameplayEffect effect = (GameplayEffect) target; 
                switch (ModifierType)
                {
                    case ModifierType.AttributeBased:
                        effect.Modifiers.Add(new AttributeBasedModifier());
                        break;                    
                    case ModifierType.Basic:
                    default:
                        effect.Modifiers.Add(new BasicModifier());
                        break;
                }
            }
        }
    }
#endif
}