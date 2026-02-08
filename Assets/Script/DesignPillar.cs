using UnityEngine;

public class DesignPillar : MonoBehaviour
{
	public GameObject designUI;

	private bool playerInRange = false;
	private bool isUIOpen = false;
	private PlayerDesign playerDesign;

	private void Start()
	{
		if (designUI != null)
			designUI.SetActive(false);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!other.CompareTag("Player")) return;

		playerDesign = other.GetComponent<PlayerDesign>();
		playerInRange = true;
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (!other.CompareTag("Player")) return;

		playerInRange = false;
		CloseDesignUI();
	}

	private void Update()
	{
		if (!playerInRange || playerDesign == null) return;

		if (Input.GetKeyDown(KeyCode.E))
		{
			if (playerDesign.hasPen)
			{
				ToggleDesignUI();
			}
			else
			{
				Debug.Log("Cần bút mới mở được bản thiết kế!");
			}
		}
	}

	void ToggleDesignUI()
	{
		if (designUI == null) return;

		if (isUIOpen)
			CloseDesignUI();
		else
			OpenDesignUI();
	}

	void OpenDesignUI()
	{
		if (designUI == null) return;

		isUIOpen = true;
		designUI.SetActive(true);

		GameStateManager.Instance.EnterDesignMode();
	}

	void CloseDesignUI()
	{
		if (!isUIOpen) return;
		if (designUI == null) return;

		isUIOpen = false;
		designUI.SetActive(false);

		GameStateManager.Instance.ExitDesignMode();
	}
}
