using System.Linq;
using UnityEngine;

namespace CardGameUtils
{
    [RequireComponent(typeof(Card))]
    public class TransferSelfOperator : MonoBehaviour
    {
        public CardGroupName DestinationCardGroupName;
        
        public void Execute()
        {
            FindObjectsOfType<CardGroup>().First(group => group.Name == DestinationCardGroupName).Add(GetComponent<Card>());
        }
    }
}