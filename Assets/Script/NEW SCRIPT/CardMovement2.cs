using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMovement2 : MonoBehaviour
{
   
    [HideInInspector] public Vector3 offset;
    void OnMouseDown()
        {
            offset = transform.position - GetMouseWorldPos();
        }

    void OnMouseDrag()
    {
        {
            TidakFantasth.Card2.isDragging = true;
            transform.position = GetMouseWorldPos() + offset;
        }


        
    }
    Vector3 GetMouseWorldPos()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.WorldToScreenPoint(transform.position).z;
            return Camera.main.ScreenToWorldPoint(mousePos);
        }
}
