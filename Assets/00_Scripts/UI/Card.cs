using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CardDB card;
    [SerializeField] TextMeshProUGUI Title, Description;
    [SerializeField] Image IconImage;
    [SerializeField] Image OutlineImage;

    Animator animator;

    public bool isSelected = false;

    public Color[] colors;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetAnimations("Card_PointerDown");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetAnimations("Card_PointerUp");
    }

    public void Initalize(CardDB cardDB)
    {
        card = cardDB;

        OutlineImage.color = card.state == CardState.Active ? colors[0] : colors[1];
        Title.text = card.id;
        Description.text = string.Format(card.description, card.DamagePercentage);
        IconImage.sprite = MANAGER.DB.GetSprite(card.name);
        animator.Rebind();
        isSelected = false;
    }

    public void SetAnimations(string temp)
    {
        if (isSelected) return;
        animator.Play(temp);
    }
}
