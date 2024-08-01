using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CardGameUtils
{
    public enum GetCardsMethod
    {
        FirstN,
        LastN,
        RandomN
    }
    
    public class CardGroup : MonoBehaviour
    {
        public CardGroupName Name;
        public List<Card> Cards;
        public int CardLimit = -1;
        

        #region GroupSetup
        [Serializable]
        public struct CardPrefabInfo
        {
            public GameObject CardPrefab;
            public int CardCount;
        };
        
        public List<CardPrefabInfo> InitialCardPrefabList;
        
        public void SetupInitCards()
        {
            foreach (CardPrefabInfo info in InitialCardPrefabList)
            {
                for (int i = 0; i < info.CardCount; i++)
                {
                    GameObject cardPrefab = Instantiate(info.CardPrefab, transform.position, Quaternion.identity);
                    Add(cardPrefab.GetComponent<Card>());
                }
            }
        }
        #endregion

        #region Add/Remove Card
        public bool HasRoom()
        {
            return CardLimit < 0 || Cards.Count < CardLimit;
        }

        public bool Add(Card card, int? index = null)
        {
            if (!HasRoom())
            {
                return false;
            }
                
            CardGroup group = card.Group;
            if (group != null)
            {
                group.Remove(card);
            }
            
            if (index == null || index >= Cards.Count)
            {
                Cards.Add(card);
            }
            else if (index < 0)
            {
                Cards.Insert(0, card);
            }
            else
            {
                Cards.Insert((int)index, card);
            }
            
            card.Group = this;

            UpdateLayout();
            return true;
        }

        public bool Remove(Card card)
        {
            if (!Cards.Contains(card))
            {
                return false;
            }

            Cards.Remove(card);
            card.Group = null;

            UpdateLayout();
            return true;
        }
        #endregion

        public GroupLayoutSetting LayoutSetting;

        public void UpdateLayout()
        {
            LayoutSetting.Apply(this);
        }

        public List<Card> Get(GetCardsMethod method, int count)
        {
            var output = new List<Card>();
            if (count <= 0 || Cards.Count <= 0)
            {
                return output;
            }
            count = Math.Min(count, Cards.Count);
            switch (method)
            {
                case GetCardsMethod.FirstN:
                    output = Cards.GetRange(0, count);
                    break;
                case GetCardsMethod.LastN:
                    output = Cards.GetRange(Cards.Count - count, count);
                    break;
                case GetCardsMethod.RandomN:
                    var pool = new List<Card>(Cards);
                    for (int i = 0; i < count; i++)
                    {
                        Card cardToAdd = pool[UnityEngine.Random.Range(0, pool.Count - 1)];
                        pool.Remove(cardToAdd);
                        output.Add(cardToAdd);
                    }
                    break;
            }
            return output;
        }
    }
}