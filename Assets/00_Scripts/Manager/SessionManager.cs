using UnityEngine;

public delegate void OnExpChanaged(float exp);
public class SessionManager : MonoBehaviour
{
    public OnExpChanaged onExpChanaged;
    public int CurrentWave;
    public int Level;
    public int Damage;

    public float magnetRadius;
    public float EXP;
    public float GameTime;

    public bool isGameOver = false;

    public void AddEXP(float exp)
    {
        EXP += exp;
        if (EXP >= 100)
        {
            Level++;
            EXP = 0;
        }
        onExpChanaged?.Invoke(EXP);
    }
}
