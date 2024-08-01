using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DesignPattern
{
    public class InspectorReadOnlyAttribute : PropertyAttribute { }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(InspectorReadOnlyAttribute))]
#endif
    public class ReadOnlyDrawer 
#if UNITY_EDITOR
        : PropertyDrawer
#endif    
    {
#if UNITY_EDITOR
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
#endif  
    }
}