using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D;

public class DBManager : MonoBehaviour
{
    public PartDB Monster;
    public PartDB Bullet;
    public List<CardDB> ActiveCards = new List<CardDB>();
    public List<CardDB> PassiveCards = new List<CardDB>();

    SpriteAtlas atlas;

    private void Start()
    {
        Monster = GetDB("Monster");
        //Bullet = GetDB("Bullet");

        atlas = Resources.Load<SpriteAtlas>("Atlas");

        ActiveCards = new List<CardDB>(Resources.LoadAll<CardDB>("DB/Card/Active"));
        PassiveCards = new List<CardDB>(Resources.LoadAll<CardDB>("DB/Card/Passive"));

    }

    public Sprite GetSprite(string temp)
    {
        return atlas.GetSprite(temp);
    }

    public List<CardDB> GetRandomCardSet()
    {
        List<CardDB> result = new List<CardDB> ();

        var activeCard = ActiveCards[Random.Range(0, ActiveCards.Count)];
        result.Add(activeCard);

        var passiveCard = PassiveCards[Random.Range(0, PassiveCards.Count)];
        result.Add(passiveCard);

        bool pickActive = Random.value < 0.5f;

        var thirdPool = pickActive ? ActiveCards : PassiveCards;

        CardDB thirdCard = null;

        do
        {
            thirdCard = thirdPool[Random.Range(0, thirdPool.Count)];
        }
        while (result.Contains(thirdCard) && thirdPool.Count > 1);

        result.Add(thirdCard);

        return result.OrderBy(x => Random.value).ToList();
    }

    private PartDB GetDB(string path)
    {
        return Resources.Load<PartDB>("DB/Part/" + path);
    }
}
