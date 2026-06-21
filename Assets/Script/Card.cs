using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TidakFantasth
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Card")]
    public class Card : ScriptableObject
    {
        public string cardName;
        public CardType cardType;
        public int damage;


        public enum CardType
        {
            Attack,
            Buff,
            Debuff
        }

        // Additional properties and methods can be added here
    }
}
