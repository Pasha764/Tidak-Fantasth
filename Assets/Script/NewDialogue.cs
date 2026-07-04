using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewDialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float textSpeed;
    private bool isTyping = false;  
    public string[] lines;                
    
    private int index = 0;                
    [SerializeField] private EnemyDialogue scriptEnemy;

    private string currentLine;

    [Header("Referensi Dialog Enemy")]
    public EnemyDialogue enemyDialogue;

    void Start()
    {
        textComponent.text = string.Empty;
      
    }

    void Update()
    {
      
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        foreach(char c in currentLine.ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        isTyping = false;


        yield return new WaitForSeconds(1.5f);

      
        textComponent.text = string.Empty;

    
        if (enemyDialogue != null)
        {
            RoundManager.Instance.NextRound();
            enemyDialogue.LanjutKeStorylineBerikutnya();
        }
    }

    public void TriggerCardDialogue(string cardType)
    {
   
        if (enemyDialogue != null && enemyDialogue.IsEnemyTyping())
        {
            Debug.LogWarning("[Dialogue] Kartu diabaikan karena musuh sedang mengetik!");
            textSpeed = 0f;
            enemyDialogue.textSpeed = 0f;
        }
        StopAllCoroutines();
        textComponent.text = string.Empty;

        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            SelesaiStoryline();
        }
    }

    private void SelesaiStoryline()
    {
        textComponent.text = string.Empty;
    
    if (RoundManager.Instance != null) RoundManager.Instance.NextRound();
        TidakFantasth.Card[] allCard = FindObjectsByType<TidakFantasth.Card>(FindObjectsSortMode.None);
        foreach (TidakFantasth.Card card in allCard)
        {
            Collider2D col = card.GetComponent<Collider2D>();
            if (col != null) col.enabled = true; 
        }
    }


   
    public bool IsPlayerTyping()
    {
        return isTyping;
    }
}
