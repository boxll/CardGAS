using System;
using UnityEngine;
using UnityEngine.Events;

namespace DragAndDropSystem
{
    [RequireComponent(typeof(DroppableComponent))]
    public class TestDrop : MonoBehaviour
    {
        public UnityEvent MyOnValidDroppableEnter;
        public UnityEvent MyOnInvalidDroppableEnter;
        public UnityEvent MyOnDroppableLeave;
        private SpriteRenderer _sprite;
        private DroppableComponent _droppable;
        private void Start()
        {
            _sprite = gameObject.GetComponent<SpriteRenderer>();
            _droppable = gameObject.GetComponent<DroppableComponent>();

            _droppable.OnValidDroppableEnter = HandleValidDroppableEnter;
            _droppable.OnInvalidDroppableEnter = HandleInvalidDroppableEnter;
            _droppable.OnDroppableLeave = HandleDroppableLeave;
        }

        private void HandleValidDroppableEnter(DropInfo info)
        {
            MyOnValidDroppableEnter.Invoke();
        }
        
        private void HandleInvalidDroppableEnter(DropInfo info)
        {
            MyOnInvalidDroppableEnter.Invoke();
        }
        
        private void HandleDroppableLeave()
        {
            MyOnDroppableLeave.Invoke();
        }
        
        public void ChangeColorToRed()
        {
            _sprite.color = Color.red;
        }
        
        public void ChangeColorToGreen()
        {
            _sprite.color = Color.green;
        }
        
        public void ChangeColorToWhite()
        {
            _sprite.color = Color.white;
        }
    }
}