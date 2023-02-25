using UnityEngine;

public class Global : MonoBehaviour
{
    public static float s_TimeMoveResourse;

    public float TimeMoveResourse;

    [ContextMenu("ChangesTime")]
    public void ChangesTime()
    {
        s_TimeMoveResourse = TimeMoveResourse;
    }
}
