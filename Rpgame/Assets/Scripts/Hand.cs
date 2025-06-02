using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] private Transform cardsParent;
    [SerializeField] private float cardSpacing = 150f;
    
    private List<CardDisplay> cardsInHand = new List<CardDisplay>();
    
    public void AddCard(Card card, GameObject cardPrefab)
    {
        GameObject cardObj = Instantiate(cardPrefab, cardsParent);
        CardDisplay cardDisplay = cardObj.GetComponent<CardDisplay>();
        
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
            cardsInHand[i].transform.position = CalculateCardPosition(i);
        }
    }
    
    private Vector3 CalculateCardPosition(int index)
    {
        float xPos = (index - (cardsInHand.Count - 1) / 2f) * cardSpacing;
        return cardsParent.position + new Vector3(xPos, 0, 0);
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