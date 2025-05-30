using UnityEngine;
using TMPro;
using System.Collections;
public class DamageTMP : MonoBehaviour
{
    private TextMeshProUGUI mText;
    private RectTransform rectTransform;

    private Vector2 velocity; // 초기 속도 (포물선 운동)
    private float gravity = -500f; // 중력 효과 (ui이동이므로 값 조정 필요)
    public float lifetime = 0.5f;

    private Color textColor;

    private void Awake()
    {
        mText = GetComponent<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void Initalize(Transform parent, Vector3 pos, string temp)
    {
        transform.SetParent(parent);
        
        mText.text = temp;
        
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(pos);
        rectTransform.position = screenPosition;

        velocity = new Vector2(Random.Range(-50.0f, 50.0f), Random.Range(250.0f, 350.0f));

        textColor = mText.color;

        StartCoroutine(MoveAndFade());
    }

    IEnumerator MoveAndFade()
    {
        float elapsedTime = 0.0f;

        while(elapsedTime < lifetime)
        {
            velocity.y += gravity * Time.deltaTime;

            rectTransform.anchoredPosition += velocity * Time.deltaTime;

            textColor.a = Mathf.Lerp(1.0f, 0.0f, elapsedTime / lifetime);   // 알파값을 이동시켜 점점 투명하게
            mText.color = textColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        MANAGER.POOL.m_Pool_Dictionary["DamageTMP"].Return(this.gameObject);
    }
}
