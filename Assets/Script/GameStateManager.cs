using UnityEngine;

public class GameStateManager : MonoBehaviour
{
	public static GameStateManager Instance;

	public bool isDesignMode = false;

	private void Awake()
	{
		if (Instance == null)
			Instance = this;
		else
			Destroy(gameObject);
	}

	public void EnterDesignMode()
	{
		isDesignMode = true;
	}

	public void ExitDesignMode()
	{
		isDesignMode = false;
	}
}
