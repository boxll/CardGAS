using System;
using DesignPattern;
using UnityEngine;
using UnityEngine.Events;

namespace DragAndDropSystem
{
    public class DraggableComponent : MonoBehaviour
    {
        public ConditionGroup<DraggableComponent> DragConditions;
        public Action OnDragBegin;
        public Action<DropInfo> OnDropToDroppable;
        public UnityEvent PostDropToDroppable;
        public UnityEvent OnDragExit;

        public Vector2 GrabOffset;
        private Vector3 _dragStartPosition;

        private void OnMouseDown()
        {
            if (!DragConditions.AllMet(this))
            {
                return;
            }

            _dragStartPosition = transform.position;
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GrabOffset = transform.position - mouseWorldPosition;
            OnDragBegin?.Invoke();
            gameObject.AddComponent<DraggingComponent>();
        }

        public void ReturnToStartPosition()
        {
            transform.position = _dragStartPosition;
        }
    }
}