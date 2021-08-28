using UnityEngine;

public class SetFrameRate : MonoBehaviour
{
    void Awake()
    {
        Application.targetFrameRate = 60;
    }
}
