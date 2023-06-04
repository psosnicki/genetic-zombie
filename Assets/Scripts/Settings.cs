using UnityEngine;

public class Settings : MonoBehaviour
{
    private static Settings _instance;
    public static Settings Instance { get { return _instance; } }
    public float Speed { get; set; } = 30f;

    public bool EnableObstacleRaycast { get; set; }

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(gameObject);
        else _instance = this;
    }
}
