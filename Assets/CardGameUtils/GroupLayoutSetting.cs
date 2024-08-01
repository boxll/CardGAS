using UnityEngine;

namespace CardGameUtils
{
    public abstract class GroupLayoutSetting : ScriptableObject
    {
        protected const float CardDepthOffset = -0.1f;
        protected const float CardBaseDepth = -0.1f;
        public virtual void Apply(CardGroup group){}
    }
}