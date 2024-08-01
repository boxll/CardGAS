using UnityEngine;

namespace CardGameUtils
{
    [CreateAssetMenu(fileName = "StackLayoutSetting", menuName = "CardGameUtils/StackLayoutSetting", order = 0)]
    public class StackLayoutSetting : GroupLayoutSetting
    {
        public CardFacing GroupFacing;
        public bool IsInteractable = true;
        public override void Apply(CardGroup group)
        {
            var cards = group.Cards;
            if (cards.Count == 0)
            {
                return;
            }
            float width = cards[0].GetComponent<BoxCollider2D>().size.x;
            Transform transform = group.transform;
            for (int i = 0; i < cards.Count; i++)
            {
                cards[i].gameObject.transform.position = transform.position
                                                         + Vector3.back * (CardBaseDepth + i * CardDepthOffset);
                cards[i].SetFacing(i == cards.Count - 1 ? GroupFacing : CardFacing.None);
                cards[i].SetInteractable(i == cards.Count - 1 && IsInteractable);
            }
        }
    }
}