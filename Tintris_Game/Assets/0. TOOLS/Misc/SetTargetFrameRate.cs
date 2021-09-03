using UnityEngine;

public class SetTargetFrameRate : MonoBehaviour
{
	public int desiredFrameRate = 60;

    void Awake()
    {
        Application.targetFrameRate = desiredFrameRate;
    }
}
