using UnityEngine;
using System.Collections.Generic;

public class CardManager : MonoBehaviour
{
    // Singleton (já existente)
    public static CardManager Instance { get; private set; }

    [Header("Referências")]
    [SerializeField] private Deck playerDeck;
    [SerializeField] private Hand playerHand;
    [SerializeField] private DiscardPile discardPile;
    [SerializeField] private GameObject cardPrefab;

    [Header("Configurações")]
    [SerializeField] private float cardMoveSpeed = 10f;
    [SerializeField] private List<Card> possibleCards = new List<Card>(); // Todas as cartas possíveis no jogo

    public float CardMoveSpeed => cardMoveSpeed;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        // Adiciona carta quando Q é pressionado
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AddRandomCardToHand();
        }
    }

    public void AddRandomCardToHand()
    {
        if (possibleCards.Count == 0)
        {
            Debug.LogWarning("Nenhuma carta disponível em possibleCards!");
            return;
        }

        // Seleciona uma carta aleatória
        Card randomCard = possibleCards[Random.Range(0, possibleCards.Count)];
        
        // Adiciona à mão
        playerHand.AddCard(randomCard, cardPrefab);
    }

    public void RemoveCardFromHand(CardDisplay cardDisplay)
    {
        // Remove a carta da mão
        playerHand.RemoveCard(cardDisplay);
        
        // Opcional: adiciona ao descarte
        discardPile.AddCard(cardDisplay.GetCard());
        
        // Destroi o objeto da carta
        Destroy(cardDisplay.gameObject);
    }
    
    public void DrawCards(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (playerDeck.CardCount == 0)
            {
                ReshuffleDiscardIntoDeck();
                if (playerDeck.CardCount == 0) return;
            }
            
            Card drawnCard = playerDeck.DrawCard();
            playerHand.AddCard(drawnCard, cardPrefab);
        }
    }
    
    public void UseCard(CardDisplay cardDisplay)
    {
        Card usedCard = cardDisplay.GetCard();
        discardPile.AddCard(usedCard);
        playerHand.RemoveCard(cardDisplay);
    }
    
    public void DiscardHand()
    {
        // Implemente conforme necessário
    }
    
    private void ReshuffleDiscardIntoDeck()
    {
        List<Card> discardedCards = discardPile.GetAllCards();
        playerDeck.AddCards(discardedCards);
        playerDeck.Shuffle();
        discardPile.Clear();
    }
}