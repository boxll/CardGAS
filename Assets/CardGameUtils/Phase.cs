using System;
using System.Collections.Generic;
using System.Linq;
using DragAndDropSystem;
using UnityEngine.Events;

namespace CardGameUtils
{
    [Serializable]
    public class Phase
    {
        public List<UnityEvent> OnPhaseStartEventChain;
        public List<UnityEvent> OnPhaseEndEventChain;
        public List<ValidPhaseDrop> ValidPhaseDrops;

        public void Start()
        {
            foreach (UnityEvent e in OnPhaseStartEventChain)
            {
                e.Invoke();
            }
        }
        
        public void End()
        {
            foreach (UnityEvent e in OnPhaseEndEventChain)
            {
                e.Invoke();
            }
        }
        
        public bool IsValidDrop(DropInfo dropInfo)
        {
            return ValidPhaseDrops.Any(drop =>
                drop.Source == dropInfo.Dragging.GetComponent<Card>().Group &
                drop.Target == dropInfo.Dropping.GetComponent<Card>().Group);
        }
    }

    [Serializable]
    public struct ValidPhaseDrop
    {
        public CardGroup Source;
        public CardGroup Target;
    }
}