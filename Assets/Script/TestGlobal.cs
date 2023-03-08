using UnityEngine;

public class TestGlobal : MonoBehaviour
{
    public static float s_TimeMoveResourse = 1f;
    public float TimeMoveResourse;

    [ContextMenu("ChangesTime")]
    public void ChangesTime()
    {
        s_TimeMoveResourse = TimeMoveResourse;
    }
}
