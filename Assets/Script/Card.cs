using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TidakFantasth
{
    public class Card : MonoBehaviour
    {
        public GameObject atkPrefab;
        public GameObject buffPrefab;
        public GameObject debuffPrefab;
        public string cardName;
        public CardType cardType;
        public int damage;
        public static bool isDragging;

        [HideInInspector] public Vector3 offset;

        [HideInInspector] public HandManager ownerHand;

        public enum CardType
        {
            Attack,
            Buff,
            Debuff
        }

        void Update()
        {
            if (Input.GetKeyDown("q"))
            {
                Destroy(gameObject);
            }
        }

        public void SetOwnerHand(HandManager hand)
        {
            ownerHand = hand;
        }

        void OnMouseDown()
        {
            offset = transform.position - GetMouseWorldPos();
        }

        void OnMouseDrag()
        {
            transform.position = GetMouseWorldPos() + offset;
        }

        void OnMouseUp()
        {
            if (ownerHand != null)
                ownerHand.ReturnCardToHand(this);
        }

        Vector3 GetMouseWorldPos()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.WorldToScreenPoint(transform.position).z;
            return Camera.main.ScreenToWorldPoint(mousePos);
        }
    
        
    }
}

