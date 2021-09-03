using UnityEngine;

public class MusicPlayerBehaviour : MonoBehaviour
{
    public static MusicPlayerBehaviour instance = null;

    void Awake()
    {
        transform.parent = null;
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}