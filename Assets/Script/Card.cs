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
        public bool isActivated;
        public bool isInDropZone = false;

        public static bool canUseCard = false; 
        

        [HideInInspector] public HandManager ownerHand;

        // Hook sementara untuk membuktikan "buff menambahkan damage attack x2".
        // - Saat Buff dipakai, set flag sehingga Attack berikutnya memberikan damage x2.
        public static bool NextAttackIsBuffed { get; private set; }

        void Start()
        {
            isDragging = false;
            isActivated = false;
            canUseCard = false; 
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

            if (isActivated == true)
            {
                ApplyCardEffect();
                Destroy(gameObject);
            }
        }

        public void SetOwnerHand(HandManager hand)
        {
            ownerHand = hand;
        }

    
        void OnMouseUp()
        {
            
            isDragging = false;
            // Efek kartu hanya aktif kalau kartu dilepas di panel Drop Kartu.
            // Saat ini belum ada referensi/ID untuk panel drop, jadi:
            // - jika script ini ditempel di area panel drop, kamu bisa panggil ActivateFromDrop()
            // - jika tidak, efek tidak otomatis jalan.
            if (ownerHand != null)
                ownerHand.ReturnCardToHand(this);
             
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
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("DropKartu"))
            {
                isInDropZone = true;
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("DropKartu"))
            {
                isInDropZone = false;
            }
        }

        private void ApplyCardEffect()
        {
            Dialogue dialogueSystem = FindAnyObjectByType<Dialogue>();
            
            if (dialogueSystem != null)
            {
                
                dialogueSystem.TriggerCardDialogue(cardType.ToString());
            }
            else
            {
                Debug.LogWarning("Script Dialogue tidak ditemukan di Scene! Pastikan GameObject Dialogue Box sudah aktif.");
            }

            Card[] leftCard = FindObjectsByType<Card>(FindObjectsSortMode.None);
            foreach (Card card in leftCard)
            {
    
                Collider2D col = card.GetComponent<Collider2D>();
                if (col != null)
                {
                    col.enabled = false;
                }
            }

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
                   
                    Debug.Log($"[Card] Attack: baseDamage={damage}, finalDamage={finalDamage} (buffed={NextAttackIsBuffed})");
                    break;
                }
                case CardType.Buff:
                {
                 
                    NextAttackIsBuffed = true;
                    Debug.Log($"[Card] Buff aktif: Attack berikutnya akan dikali 2. (damageBuff={damage})");
                    break;
                }
                case CardType.Debuff:
                {
                
                    Debug.Log($"[Card] Debuff dipakai. (damage={damage})");
                    break;
                }
            }

            
        }

        
    }
}


