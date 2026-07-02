using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject dropKartuUi;
    public GameObject dialogueBox;

    void Start()
    {
        dropKartuUi.SetActive(false);
        dialogueBox.SetActive(true);
    }

    void Update()
    {
        // Card kamu ada di namespace TidakFantasth.
        if (TidakFantasth.Card.isDragging == true)
        {
            dropKartuUi.SetActive(true);
        }
        
        if (TidakFantasth.Card.isDragging == false){
            dropKartuUi.SetActive(false);
        }
    }
}

