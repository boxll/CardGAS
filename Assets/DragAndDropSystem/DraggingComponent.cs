using System;
using System.Collections.Generic;
using UnityEngine;

namespace DragAndDropSystem
{
    public class DraggingComponent : MonoBehaviour
    {
        public DroppableComponent PotentialDropTarget;
        public Action<Vector2> OnDragMove;
        private DraggableComponent _draggable;

        private void Awake()
        {
            _draggable = gameObject.GetComponent<DraggableComponent>();
        }

        private void Update()
        {
            if ((Input.GetAxis("Mouse X") == 0) & (Input.GetAxis("Mouse Y") == 0))
            {
                return;
            }

            if (OnDragMove != null)
            {
                OnDragMove.Invoke(_draggable.GrabOffset);
            }
            else
            {
                MoveToMouse(_draggable.GrabOffset);
            }
        }

        private void OnMouseUp()
        {
            if (PotentialDropTarget)
            {
                _draggable.OnDropToDroppable?.Invoke(new DropInfo(_draggable, PotentialDropTarget));
                PotentialDropTarget.OnDraggableDropped?.Invoke(new DropInfo(_draggable, PotentialDropTarget));
            }
            else
            {
                _draggable.OnDragExit?.Invoke();
            }
            
            _draggable.PostDropToDroppable?.Invoke();
            // Remove this component from attached game object
            Destroy(this);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            DroppableComponent droppable = col.gameObject.GetComponent<DroppableComponent>();
            if (droppable == null)
            {
                return;
            }
            DropInfo dropInfo = new(_draggable, droppable);
            if (droppable.DropConditions.AllMet(dropInfo))
            {
                PotentialDropTarget = droppable;
                droppable.OnValidDroppableEnter?.Invoke(dropInfo);
            }
            else
            {
                droppable.OnInvalidDroppableEnter?.Invoke(dropInfo);
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            DroppableComponent droppable = col.gameObject.GetComponent<DroppableComponent>();
            if (droppable == null)
            {
                return;
            }
            if (PotentialDropTarget == droppable)
            {
                PotentialDropTarget = null;
            }
            droppable.OnDroppableLeave?.Invoke();
        }

        private void MoveToMouse(Vector2 offset)
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // ReSharper disable once Unity.InefficientPropertyAccess
            transform.position = new Vector3(mouseWorldPosition.x + offset.x,
                mouseWorldPosition.y + offset.y,
                transform.position.z);
        }
    }
}