using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using DG.Tweening;

public class HandManager : MonoBehaviour
{
    [SerializeField] private int maxHandSize;
    [SerializeField] private GameObject cardPrefab; // Prefab for the card UI element
    [SerializeField] private SplineContainer splineContainer; // Reference to the SplineController
    [SerializeField] private Transform spawnPoint;
    private List<GameObject> handCards = new();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) DrawCard();
        
    }
    private void DrawCard()
    {
        if (handCards.Count >= maxHandSize) return;
        GameObject g = Instantiate(cardPrefab, spawnPoint.position, spawnPoint.rotation);
        handCards.Add(g);
        UpdateCardPosition();
    }
    private void UpdateCardPosition()
    {
        if (handCards.Count == 0) return;
        float cardSpacing = 1f/maxHandSize; // Adjust this value for spacing between cards
        float FirstCardPosition = 0.5f - (handCards.Count - 1) * cardSpacing / 2; // Center the cards around the middle of the spline
        Spline spline = splineContainer.Spline;
        for (int i = 0; i < handCards.Count; i++)
        {
            float p = FirstCardPosition + i * cardSpacing;
            Vector3 splinePosition = spline.EvaluatePosition(p);
            Vector3 forward = spline.EvaluateTangent(p);
            Vector3 up = spline.EvaluateUpVector(p);
            Quaternion rotation = Quaternion.LookRotation(up, Vector3.Cross(up, forward)).normalized;
            handCards[i].transform.DOMove(splinePosition, 0.25f);
            handCards[i].transform.DOLocalRotateQuaternion(rotation, 0.25f);
        }
    }
}
