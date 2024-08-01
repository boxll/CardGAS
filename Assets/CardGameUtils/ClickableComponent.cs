using DesignPattern;
using UnityEngine;
using UnityEngine.Events;

namespace CardGameUtils
{
    public class ClickableComponent : MonoBehaviour
    {
        public UnityEvent OnPress;
        public UnityEvent OnButtonClicked;

        public ConditionGroup<NoParams> ClickConditions;

        void OnMouseDown()
        {
            if (ClickConditions.AllMet(new NoParams()))
            {
                OnPress.Invoke();
            }
        }

        void OnMouseUpAsButton()
        {
            if (ClickConditions.AllMet(new NoParams()))
            {
                OnButtonClicked.Invoke();
            }
        }
    }
}