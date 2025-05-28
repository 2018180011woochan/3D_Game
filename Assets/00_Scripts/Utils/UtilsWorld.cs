using UnityEngine;

public class UtilsWorld : MonoBehaviour
{
    public static Vector3 GetRandomCircleOffset(float radius)
    {
        Vector2 offset2D = Random.insideUnitCircle * radius;

        return new Vector3(offset2D.x, 0f, offset2D.y);
    }
}
