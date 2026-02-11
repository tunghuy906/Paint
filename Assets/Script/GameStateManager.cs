using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    public bool isDesignMode { get; private set; }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void EnterDesignMode()
    {
        isDesignMode = true;
        Time.timeScale = 0f;
    }

    public void ExitDesignMode()
    {
        isDesignMode = false;
        Time.timeScale = 1f;
    }
}
