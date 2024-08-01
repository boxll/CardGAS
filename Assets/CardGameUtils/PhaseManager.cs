using System;
using System.Collections.Generic;
using DesignPattern;
using DragAndDropSystem;

namespace CardGameUtils
{
    public class PhaseManager : Singleton<PhaseManager>
    {
        public List<Phase> Phases;
        private Phase CurrentPhase => Phases[_currentPhaseIndex];
        private int _currentPhaseIndex = 0;

        private void Start()
        {
            CurrentPhase.Start();
        }

        public void PhaseTransition()
        {
            CurrentPhase.End();
            _currentPhaseIndex = (_currentPhaseIndex + 1) % Phases.Count;
            CurrentPhase.Start();
        }
        
        public bool IsValidDrop(DropInfo dropInfo)
        {
            return CurrentPhase.IsValidDrop(dropInfo);
        }
    }
}