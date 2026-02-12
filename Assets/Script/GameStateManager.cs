using UnityEngine;
using Unity.Cinemachine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;
    [Header("Cameras")]
    public CinemachineCamera vcamGameplay;
    public CinemachineCamera vcamDesign;

    [Header("State")]
    public bool isDesignMode { get; private set; }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        if (isDesignMode && Input.GetKeyDown(KeyCode.E))
        {
            ExitDesignMode();
        }
    }

    public void EnterDesignMode()
    {
        isDesignMode = true;

        Time.timeScale = 0f;
        vcamDesign.Follow = null;

        vcamGameplay.Priority = 0;
        vcamDesign.Priority = 20;
    }

    public void ExitDesignMode()
    {
        isDesignMode = false;

        Time.timeScale = 1f;

        vcamGameplay.Priority = 20;
        vcamDesign.Priority = 0;
    }
}
