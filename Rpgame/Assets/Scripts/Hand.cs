using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] private Transform cardsParent;
    [SerializeField] private float cardSpacing = 150f;
    
    private List<CardDisplay> cardsInHand = new List<CardDisplay>();
    private float currentY = 0;
    public void AddCard(Card card, GameObject cardPrefab)
    {
        GameObject cardObj = Instantiate(cardPrefab, cardsParent);
        CardDisplay cardDisplay = cardObj.GetComponent<CardDisplay>();
        RectTransform rectTransform = cardDisplay.GetComponent<RectTransform>();
        currentY = 0;
        Vector3 cardPosition = CalculateCardPosition(cardsInHand.Count);
        cardDisplay.Initialize(card, cardPosition);
        
        cardsInHand.Add(cardDisplay);
    }
    
    public void RemoveCard(CardDisplay card)
    {
        cardsInHand.Remove(card);
        Destroy(card.gameObject);
        UpdateCardPositions();
    }
    
    private void UpdateCardPositions()
    {
        for (int i = 0; i < cardsInHand.Count; i++)
        {
            //RectTransform rectTransform = GetComponent<RectTransform>();
            cardsInHand[i].transform.position = CalculateCardPosition(i);
        }
    }

    private Vector3 CalculateCardPosition(int index)
    {
        float xPos = (index - (cardsInHand.Count - 1) / 2f) * cardSpacing;
        return cardsParent.position + new Vector3(xPos, currentY, 0);
    }
    
    public void ClearHand()
    {
        foreach (CardDisplay card in cardsInHand)
        {
            Destroy(card.gameObject);
        }
        cardsInHand.Clear();
    }
}