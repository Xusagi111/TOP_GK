using UnityEngine;

public class TestGlobal : MonoBehaviour
{
    public static float s_TimeMoveResourse = 0f;
    public float TimeMoveResourse;

    public void Awake()
    {
        s_TimeMoveResourse = TimeMoveResourse;
    }
}
