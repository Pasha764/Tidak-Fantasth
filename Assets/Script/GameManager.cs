using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject dropKartuUi;

    void Start()
    {
        dropKartuUi.SetActive(false);
    }

    void Update()
    {
        // Card kamu ada di namespace TidakFantasth.
        if (TidakFantasth.Card2.isDragging == true)
        {
            dropKartuUi.SetActive(true);
        }
        
        if (TidakFantasth.Card2.isDragging == false){
            dropKartuUi.SetActive(false);
        }
    }
}

