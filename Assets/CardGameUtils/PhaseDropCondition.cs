using DesignPattern;
using DragAndDropSystem;
using UnityEngine;

namespace CardGameUtils
{
    [CreateAssetMenu(fileName = "PhaseDropCondition", menuName = "CardGameUtils/PhaseDropCondition", order = 0)]
    public class PhaseDropCondition: Condition<DropInfo>
    {
        public override bool IsMet(DropInfo dropInfo)
        {
            return PhaseManager.Instance.IsValidDrop(dropInfo);
        }
    }
}