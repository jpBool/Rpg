using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(Canvas))]
public class CardDisplay : MonoBehaviour, IPointerClickHandler
{
    [Header("Referências")]
    [SerializeField] private Image cardImage;
    
    [Header("Configurações")]
    [SerializeField] private float hoverScale = 1.2f;
    [SerializeField] private float hoverYOffset = 50f;
    
    private Card currentCard;
    private Vector3 originalScale;
    private Vector3 targetPosition;
    private int originalSortingOrder;
    private bool isHovering = false;

    public void Initialize(Card card, Vector3 position)
    {
        currentCard = card;
        targetPosition = position;
        cardImage.sprite = card.cardImage;

        originalScale = transform.localScale;
        originalSortingOrder = GetComponent<Canvas>().sortingOrder;
        cardImage = GetComponent<Image>();
        
        // Garante que a Image é interativa (Raycast Target deve estar ativado no Inspector)
        if (cardImage != null)
        {
            cardImage.raycastTarget = true;
        }
    }
    
    private void Update()
    {
        if (!isHovering)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, CardManager.Instance.CardMoveSpeed * Time.deltaTime);
        }
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        transform.localScale = originalScale * hoverScale;
        GetComponent<Canvas>().sortingOrder = originalSortingOrder + 5;
        transform.position = targetPosition + new Vector3(0, hoverYOffset, 0);
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        transform.localScale = originalScale;
        GetComponent<Canvas>().sortingOrder = originalSortingOrder;
    }
    
    public void HandleCardClick()
    {
        Debug.Log("Carta clicada!");
        CardManager.Instance.RemoveCardFromHand(this);
    }

    // Mantenha também a implementação da interface
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            HandleCardClick();
    }
    
    
    public Card GetCard() => currentCard;
}