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
            isDragging = false;
            // Efek kartu hanya aktif kalau kartu dilepas di panel Drop Kartu.
            // Saat ini belum ada referensi/ID untuk panel drop, jadi:
            // - jika script ini ditempel di area panel drop, kamu bisa panggil ActivateFromDrop()
            // - jika tidak, efek tidak otomatis jalan.
            if (ownerHand != null)
                ownerHand.ReturnCardToHand(this);
                // Mengecek apakah kartu berada di atas DropKartu
            Collider2D hit = Physics2D.OverlapPoint(transform.position);

            if (hit != null)
            {
                DropKartu drop = hit.GetComponent<DropKartu>();

                if (drop != null)
                {
                    drop.OnCardDropped(gameObject);
                }
                

                if (isInDropZone)
                {
                    isActivated = true;
                }
            }
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
            switch (cardType)
            {
                case CardType.Attack:
                {
                    int finalDamage = damage;
                    if (NextAttackIsBuffed)
                    {
                        finalDamage *= 2;
                        NextAttackIsBuffed = false;
                    }

                    BarRage.Instance.AddRage(finalDamage);
                    // TODO: ganti dengan sistem damage ke target/health kamu.
                    Debug.Log($"[Card] Attack: baseDamage={damage}, finalDamage={finalDamage} (buffed={NextAttackIsBuffed})");
                    break;
                }
                case CardType.Buff:
                {
                    // Buff ini berlaku untuk Attack berikutnya.
                    NextAttackIsBuffed = true;
                    Debug.Log($"[Card] Buff aktif: Attack berikutnya akan dikali 2. (damageBuff={damage})");
                    break;
                }
                case CardType.Debuff:
                {
                    // Placeholder sesuai kebutuhan.
                    Debug.Log($"[Card] Debuff dipakai. (damage={damage})");
                    break;
                }
            }

            RoundManager.Instance.NextRound();
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.WorldToScreenPoint(transform.position).z;
            return Camera.main.ScreenToWorldPoint(mousePos);
        }
    
        
    }
}

