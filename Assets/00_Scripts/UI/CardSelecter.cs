using System.Collections;
using UnityEngine;

public class CardSelecter : MonoBehaviour
{
    public Card[] cards;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Initilaze()
    {
        animator.Play("Selecter_Open");

        for (int i = 0; i < cards.Length; ++i)
        {
            cards[i].Initalize();
        }
    }

    public void SelectCard(int value)
    {
        for (int i = 0; i < cards.Length; ++i)
        {
            if (i == value)
            {
                cards[i].SetAnimations("Card_Select");
            }
            else
            {
                cards[i].SetAnimations("Card_NonSelect");
            }
            cards[i].isSelected = true;
        }
        StartCoroutine(GameStartCoroutine());
    }

    IEnumerator GameStartCoroutine()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        animator.Play("Selecter_Close");
        Time.timeScale = 1.0f;
    }
}
