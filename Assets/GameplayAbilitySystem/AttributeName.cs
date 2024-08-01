using DesignPattern;
using UnityEngine;

namespace GameplayAbilitySystem
{
    [CreateAssetMenu(fileName = "Name", menuName = "GAS/Attribute", order = 0)]
    public class AttributeName : ScriptableObject
    {
        [InspectorReadOnly]public string Name;
#if UNITY_EDITOR
        private void OnValidate() {
            if (!string.IsNullOrEmpty(name)) 
            {
                string assetPath = UnityEditor.AssetDatabase.GetAssetPath(this.GetInstanceID());
                Name = System.IO.Path.GetFileNameWithoutExtension(assetPath);
            }
        }
#endif
    }
}