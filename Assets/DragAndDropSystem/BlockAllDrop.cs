using UnityEngine;

namespace DragAndDropSystem
{
    [CreateAssetMenu(fileName = "BlockAllDrop", menuName = "Condition/BlockAllDrop", order = 0)]
    public class BlockAllDrop : DropCondition
    {
        public override bool IsMet(DropInfo argObject)
        {
            return false;
        }
    }
}