using System.Collections.Generic;
using UnityEngine;

namespace CardGameUtils
{
    public class ShuffleOperator : MonoBehaviour
    {
        public CardGroup Group;
        public void Execute()
        {
            var tempCards = new List<Card>();
            while (Group.Cards.Count > 0)
            {
                int i = Random.Range(0, Group.Cards.Count);
                tempCards.Add(Group.Cards[i]);
                Group.Cards.RemoveAt(i);
            }
            Group.Cards = tempCards;

            Group.LayoutSetting.Apply(Group);
        }
    }
}