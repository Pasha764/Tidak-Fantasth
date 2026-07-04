using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float textSpeed;
    private bool isTyping = false; 

    [Header("Alur Cerita Utama (Berurutan)")]
    [Tooltip("Masukkan semua baris dialog cerita di sini secara berurutan.")]
    public string[] lines;

    // 🔥 SATU INDEKS UNTUK SEMUA: Berjalan berurutan sesuai story saat kartu apa pun di-drop
    private int answerIndex = 0;
    private string currentLine;

    [Header("Referensi Dialog Enemy")]
    public EnemyDialogue enemyDialogue;

    void Start()
    {
        if (textComponent != null)
        {
            textComponent.text = string.Empty;
        }
    }

    IEnumerator TypeLine()
    {
        if (textComponent == null) yield break;

        isTyping = true;
        textComponent.text = string.Empty;

        // Pengaman jika teks kosong agar tidak memicu NullReferenceException
        if (string.IsNullOrEmpty(currentLine))
        {
            currentLine = "...";
        }

        foreach(char c in currentLine.ToCharArray())
        {
            if (textComponent == null) yield break;
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        isTyping = false;

        yield return new WaitForSeconds(1.5f);
      
        if (textComponent != null) textComponent.text = string.Empty;

        if (enemyDialogue != null)
        {
            if (RoundManager.Instance != null) RoundManager.Instance.NextRound();
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
        if (textComponent != null) textComponent.text = string.Empty;

        // Pengaman jika array lines di Inspector belum diisi sama sekali
        if (lines == null || lines.Length == 0)
        {
            Debug.LogError("[Dialogue] Array 'Lines' masih kosong di Inspector! Tolong isi teks ceritanya.");
            LanjutTanpaTeks();
            return;
        }

        // 🔥 LOGIKA STORY BERURUTAN: Ambil teks berdasarkan urutan cerita saat ini
        if (answerIndex < lines.Length)
        {
            currentLine = lines[answerIndex];
            answerIndex++; // Maju ke baris cerita selanjutnya untuk kartu berikutnya
            StartCoroutine(TypeLine());
        }
        else
        {
            // Jika semua baris cerita di array 'lines' sudah habis dibaca
            SelesaiStoryline();
        }
    }

    private void SelesaiStoryline()
    {
        if (textComponent != null) textComponent.text = string.Empty;
        
        // Aktifkan kembali interaksi kartu jika game masih berlanjut
        TidakFantasth.Card[] allCard = FindObjectsByType<TidakFantasth.Card>(FindObjectsSortMode.None);
        foreach (TidakFantasth.Card card in allCard)
        {
            Collider2D col = card.GetComponent<Collider2D>();
            if (col != null) col.enabled = true; 
        }

        Debug.Log("[Dialogue] Semua alur cerita pada array 'lines' telah selesai.");
    }

    private void LanjutTanpaTeks()
    {
        if (RoundManager.Instance != null) RoundManager.Instance.NextRound();
        if (enemyDialogue != null) enemyDialogue.LanjutKeStorylineBerikutnya();
    }

    public bool IsPlayerTyping()
    {
        return isTyping;
    }
}
