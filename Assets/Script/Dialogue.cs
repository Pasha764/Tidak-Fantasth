using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float textSpeed;
    private bool isTyping = false; 

    [Header("Daftar Dialog Per Kategori Kartu")]
    public string[] attackLines;
    public string[] buffLines;
    public string[] debuffLines;

    [Header("Daftar Dialog Story (Berurutan)")]
    public string[] storyLines;

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
            //return;  
        }

        StopAllCoroutines();
        textComponent.text = string.Empty;
        
        string[] selectedPool = null;
        switch (cardType.ToLower())
        {
            case "attack": selectedPool = attackLines; break;
            case "buff": selectedPool = buffLines; break;
            case "debuff": selectedPool = debuffLines; break;
        }

        if (selectedPool != null && selectedPool.Length > 0)
        {
            int randomIndex = Random.Range(0, selectedPool.Length);
            currentLine = selectedPool[randomIndex];
            StartCoroutine(TypeLine());
        }
    }

   
    public bool IsPlayerTyping()
    {
        return isTyping;
    }
}
