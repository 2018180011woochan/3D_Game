using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Animator animator;

    public bool isSelected = false;

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

    public void Initalize()
    {
        animator.Rebind();
        isSelected = false;
    }

    public void SetAnimations(string temp)
    {
        if (isSelected) return;
        animator.Play(temp);
    }
}
