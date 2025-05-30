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
        MANAGER.SESSION.onMonsterCountChanged += M_CountText;
    }

    private void OnDestroy()
    {
        MANAGER.SESSION.onExpChanaged -= EXPChange;
        MANAGER.SESSION.onMonsterCountChanged -= M_CountText;
    }

    public Image EXPFill;
    public GameObject CardObject;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI monsterCountText;
    public TextMeshProUGUI TimerText;

    private void Update()
    {
        TimerText.text = UtilsUI.FormatTime(MANAGER.SESSION.GameTime);
    }

    public void SelectCard()
    {
        CardObject.SetActive(true);
    }

    private void M_CountText(int value) => monsterCountText.text = value.ToString();

    public void EXPChange(float exp)
    {
        float expPercentage = exp / MANAGER.SESSION.GetRequiredExp();
        EXPFill.fillAmount = expPercentage;
        LevelText.text = string.Format(
            "Lv.{0} <color=#FFFF00>{1:0.0}</color>%",
            (MANAGER.SESSION.Level + 1),
            expPercentage * 100.0f);
    }
}
