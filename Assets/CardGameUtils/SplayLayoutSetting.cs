using UnityEngine;

namespace CardGameUtils
{
    [CreateAssetMenu(fileName = "SplayLayoutSetting", menuName = "CardGameUtils/SplayLayoutSetting", order = 0)]
    public class SplayLayoutSetting : GroupLayoutSetting
    {
        public CardFacing GroupFacing;
        public float HorizontalMargin;
        public bool IsInteractable = true;

        public override void Apply(CardGroup group)
        {
            var cards = group.Cards;
            if (cards.Count == 0)
            {
                return;
            }
            float width = cards[0].GetComponent<BoxCollider2D>().size.x;
            Transform groupTransform = group.transform;
            Vector3 right = groupTransform.right;
            for (int i = 0; i < cards.Count; i++)
            {
                cards[i].gameObject.transform.position = groupTransform.position
                                                         + Vector3.forward * (CardBaseDepth + i * CardDepthOffset)
                                                         + right * (i - (cards.Count - 1) / 2f) *
                                                         (HorizontalMargin + width);
                cards[i].SetFacing(GroupFacing);
                cards[i].SetInteractable(IsInteractable);
            }
        }
    }
}