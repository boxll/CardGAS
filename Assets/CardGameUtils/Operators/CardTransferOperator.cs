using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CardGameUtils
{
    public class CardTransferOperator : MonoBehaviour
    {
        public CardGroup Source;
        public CardGroup Destination;
        public GetCardsMethod GrabFromMethod = GetCardsMethod.LastN;
        public GetCardsMethod SendToMethod = GetCardsMethod.LastN;
        public bool TryAgainAfterSourceDepleted;

        public void Execute(int numberToTransfer)
        {
            var cardsToMove = numberToTransfer > 0
                ? Source.Get(GrabFromMethod, numberToTransfer)
                : Source.Cards.ToList();
            foreach (Card card in cardsToMove)
            {
                Destination.Add(card,
                    SendToMethod == GetCardsMethod.FirstN ? -1 :
                    SendToMethod == GetCardsMethod.LastN ? null :
                    Random.Range(0, Destination.Cards.Count + 1));
            }
            // TODO: handle situation where the source is depleted before transfer finished
        }
    }
}