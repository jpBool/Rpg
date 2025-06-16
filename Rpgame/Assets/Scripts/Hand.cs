using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Transform cardsParent;
    [SerializeField] private float cardSpacing = 120f;
    [SerializeField] private float yOffsetFromBottom = 300f;
    
    private List<CardDisplay> cardsInHand = new List<CardDisplay>();
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void AddCard(Card card, GameObject cardPrefab)
    {
        GameObject cardObj = Instantiate(cardPrefab, cardsParent);
        CardDisplay cardDisplay = cardObj.GetComponent<CardDisplay>();
        cardDisplay.Initialize(card);
        cardsInHand.Add(cardDisplay);
        UpdateCardPositions();
    }

    public void RemoveCard(CardDisplay card)
    {
        if (cardsInHand.Contains(card))
        {
            cardsInHand.Remove(card);
            Destroy(card.gameObject);
            UpdateCardPositions();
        }
    }
    
    private void UpdateCardPositions()
    {
        if (cardsInHand.Count == 0) return;
        
        // Pega as coordenadas da câmera
        float screenBottomCenterX = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0f, 10f)).x;
        float screenBottomY = mainCamera.ViewportToWorldPoint(new Vector3(0f, 0f, 10f)).y + yOffsetFromBottom;
        
        // Calcula o posicionamento centralizado
        float totalWidth = (cardsInHand.Count - 1) * cardSpacing;
        float startX = screenBottomCenterX - (totalWidth / 2f) + 750;
        
        for (int i = 0; i < cardsInHand.Count; i++)
        {
            Vector3 targetPosition = new Vector3(
                startX + (i * cardSpacing),
                screenBottomY,
                0);
            
            cardsInHand[i].transform.position = targetPosition;
        }
    }

    public void ClearHand()
    {
        foreach (CardDisplay card in cardsInHand)
        {
            if (card != null && card.gameObject != null)
                Destroy(card.gameObject);
        }
        cardsInHand.Clear();
    }
}