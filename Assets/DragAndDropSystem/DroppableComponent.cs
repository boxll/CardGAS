using System;
using DesignPattern;
using UnityEngine;

namespace DragAndDropSystem
{
    public class DroppableComponent : MonoBehaviour
    {
        public ConditionGroup<DropInfo> DropConditions;
        public Action<DropInfo> OnValidDroppableEnter;
        public Action<DropInfo> OnInvalidDroppableEnter;
        public Action OnDroppableLeave;
        public Action<DropInfo> OnDraggableDropped;
    }
    
    public struct DropInfo
    {
        public DraggableComponent Dragging { get; private set; }
        public DroppableComponent Dropping { get; private set; }

        public DropInfo(DraggableComponent dragging, DroppableComponent dropping)
        {
            Dragging = dragging;
            Dropping = dropping;
        }
    }
}