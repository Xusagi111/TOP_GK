using Resource;
using UnityEngine;

public class Global : MonoBehaviour
{
    public static float s_TimeMoveResourse = 2f;

    public float TimeMoveResourse;
    [ContextMenu("ChangesTime")]
    public void ChangesTime()
    {
        s_TimeMoveResourse = TimeMoveResourse;
    }
}
