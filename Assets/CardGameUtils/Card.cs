using System;
using System.Collections.Generic;
using DesignPattern;
using DragAndDropSystem;
using GameplayAbilitySystem;
using UnityEngine;
using UnityEngine.Events;

namespace CardGameUtils
{
    public enum CardFacing
    {
        None,
        FaceUp,
        FaceDown
    }
    
    public class Card : MonoBehaviour
    {
        [InspectorReadOnly]public CardGroup Group;
        public UnityEvent OnPlay;
        
        public GameObject Front;
        public GameObject Back;
        public CardFacing Facing { get; private set; }
        [InspectorReadOnly]public AbilitySystemComponent Asc;

        protected virtual void Awake()
        {
            Asc = GetComponent<AbilitySystemComponent>();
            Asc.OnAttributeChanged += HandleAttributeChange;
            SetUpDragAndDrop();
        }

        public void SetFacing(CardFacing facing)
        {
            if(Facing == facing)
                return;

            switch (facing)
            {
                case CardFacing.None:
                    Front.SetActive(false);
                    Back.SetActive(false);
                    break;
                case CardFacing.FaceDown:
                    Front.SetActive(false);
                    Back.SetActive(true);
                    break;
                case CardFacing.FaceUp:
                default:
                    Front.SetActive(true);
                    Back.SetActive(false);
                    break;
            }
        }

        public void SetInteractable(bool isInteractable)
        {
            Collider2D component = GetComponent<Collider2D>();
            if (component)
            {
                component.enabled = isInteractable;
            }
        }

        #region DragAndDrop

        private void SetUpDragAndDrop()
        {
            var draggableComponent = GetComponent<DraggableComponent>();
            var droppableComponent = GetComponent<DroppableComponent>();
            if (draggableComponent != null)
            {
                draggableComponent.OnDropToDroppable += (info => PlayToTarget(info.Dropping.gameObject));
            }

            if (droppableComponent != null)
            {
                droppableComponent.OnDraggableDropped += (info => PlayToTarget(info.Dragging.gameObject));
            }
        }

        #endregion

        #region Gameplay

        protected virtual void HandleAttributeChange(AttributeName attributeName, float oldValue, float newValue, GameplayEffect gameplayEffect) {}

        private void PlayToTarget(GameObject target)
        {
            AbilitySystemComponent targetAsc = target.GetComponent<AbilitySystemComponent>();
            if (Asc == null | targetAsc == null)
            {
                return;
            }
            
            foreach (GameplayAbility ability in Asc.GrantedGameplayAbilities)
            {
                Asc.TryActivateAbility(ability, targetAsc);
            }
        }
        #endregion
    }
}