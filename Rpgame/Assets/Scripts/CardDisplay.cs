using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(Canvas))]
public class CardDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
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
    
    public void OnPointerClick(PointerEventData eventData)
    {
        CardManager.Instance.UseCard(this);
    }
    
    public Card GetCard() => currentCard;
}