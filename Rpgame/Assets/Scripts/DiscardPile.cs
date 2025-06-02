using System.Collections.Generic;

public class DiscardPile
{
    private List<Card> discardedCards = new List<Card>();
    
    public void AddCard(Card card) => discardedCards.Add(card);
    public void AddCards(List<Card> cards) => discardedCards.AddRange(cards);
    public List<Card> GetAllCards() => new List<Card>(discardedCards);
    public void Clear() => discardedCards.Clear();
    public int CardCount => discardedCards.Count;
}