using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Animator animator;

    public void OnPointerEnter(PointerEventData eventData)
    {
        //animator.Play("Card_PointerDown");
        animator.Play("Card_PointerUp");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //animator.Play("Card_PointerUp");
        animator.Play("Card_PointerDown");
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
}
