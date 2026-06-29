using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

        // Hook sementara untuk membuktikan "buff menambahkan damage attack x2".
        // - Saat Buff dipakai, set flag sehingga Attack berikutnya memberikan damage x2.
        public static bool NextAttackIsBuffed { get; private set; }

        void Start()
        {
            isDragging = false;
        }

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
            isDragging = true;
            transform.position = GetMouseWorldPos() + offset;
        }

        void OnMouseUp()
        {
            // Efek kartu hanya aktif kalau kartu dilepas di panel Drop Kartu.
            // Saat ini belum ada referensi/ID untuk panel drop, jadi:
            // - jika script ini ditempel di area panel drop, kamu bisa panggil ActivateFromDrop()
            // - jika tidak, efek tidak otomatis jalan.
            isDragging = false;
            if (ownerHand != null)
                ownerHand.ReturnCardToHand(this);
        }


        private void ApplyCardEffect()
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
        }

        Vector3 GetMouseWorldPos()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.WorldToScreenPoint(transform.position).z;
            return Camera.main.ScreenToWorldPoint(mousePos);
        }
    }
}


