using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseCanvas : MonoBehaviour
{
    public static BaseCanvas instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        EXPChange(0);
        MANAGER.SESSION.onExpChanaged += EXPChange;
    }

    private void OnDestroy()
    {
        MANAGER.SESSION.onExpChanaged -= EXPChange;
    }

    public Image EXPFill;
    public TextMeshProUGUI LevelText;

    public void EXPChange(float exp)
    {
        float expPercentage = exp / 100.0f;
        EXPFill.fillAmount = expPercentage;
        LevelText.text = string.Format(
            "Lv.{0} <color=#FFFF00>{1:0.0}</color>%",
            (MANAGER.SESSION.Level + 1),
            exp);
    }
}
