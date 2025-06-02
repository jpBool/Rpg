using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] private List<Card> cards = new List<Card>();
    
    public void Initialize(List<Card> startingCards)
    {
        cards = new List<Card>(startingCards);
        Shuffle();
    }
    
    public void Shuffle()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            Card temp = cards[i];
            int randomIndex = Random.Range(i, cards.Count);
            cards[i] = cards[randomIndex];
            cards[randomIndex] = temp;
        }
    }
    
    public Card DrawCard()
    {
        if (cards.Count == 0) return null;
        
        Card drawnCard = cards[0];
        cards.RemoveAt(0);
        return drawnCard;
    }
    
    public void AddCard(Card card) => cards.Add(card);
    public void AddCards(List<Card> newCards) => cards.AddRange(newCards);
    public int CardCount => cards.Count;
}