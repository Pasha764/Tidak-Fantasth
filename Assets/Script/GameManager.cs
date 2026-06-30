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
        if (TidakFantasth.Card.isDragging == true)
        {
            dropKartuUi.SetActive(true);
        }
        
        if (TidakFantasth.Card.isDragging == false){
            dropKartuUi.SetActive(false);
        }
    }
}

