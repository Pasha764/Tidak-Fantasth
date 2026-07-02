using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyDialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    private int index;
    private bool isTyping = false; 

    [Header("Referensi Dialog Player")]
    public Dialogue playerDialogue; 

    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }

    void Update()
    {
        // KOSONG - Agar tidak ada bentrokan frame paksa
    }

    void StartDialogue()
    {
        index = 0;
        if (lines == null || lines.Length == 0) return;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        foreach(char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
            TidakFantasth.Card.isDragging = false;
        }
        isTyping = false;

        yield return new WaitForSeconds(1.5f);

        if (index == 0)
        {
            Debug.Log("[EnemyDialogue] Storyline 1 selesai. Menunggu Player drop kartu ke dropzone...");
            TidakFantasth.Card.isDragging = true;

        }
        
          
    }

    public void LanjutKeStorylineBerikutnya()
    {
        
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StopAllCoroutines();
            StartCoroutine(TypeLine());
        }
        else
        {
            textComponent.text = string.Empty;
            // if (RoundManager.Instance != null) RoundManager.Instance.NextRound();
            TidakFantasth.Card[] allCard = FindObjectsByType<TidakFantasth.Card>(FindObjectsSortMode.None);
            foreach (TidakFantasth.Card card in allCard)
            {
                Collider2D col = card.GetComponent<Collider2D>();
                if (col != null) col.enabled = true; 
            }
        }
    }

 
    public bool IsEnemyTyping()
    {
        return isTyping;
    }
}
