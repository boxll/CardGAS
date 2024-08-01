using UnityEngine;

namespace DragAndDropSystem
{
    [CreateAssetMenu(fileName = "BlockAllDrag", menuName = "Condition/BlockAllDrag", order = 0)]
    public class BlockAllDrag : DragCondition
    {
        public override bool IsMet(DraggableComponent argObject)
        {
            return false;
        }
    }
}