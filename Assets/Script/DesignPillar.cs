using UnityEngine;

public class DesignPillar : MonoBehaviour
{
	public GameObject designUI;

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

		if (playerDesign.hasPen)
		{
			OpenDesignUI();
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (!other.CompareTag("Player")) return;

		CloseDesignUI();
	}

	private void Update()
	{
		if (!isUIOpen) return;

		if (Input.GetKeyDown(KeyCode.E))
		{
			CloseDesignUI();
		}
	}

	void OpenDesignUI()
	{
		if (designUI == null) return;

		isUIOpen = true;
		designUI.SetActive(true);

		if (HUDController.Instance != null)
		{
			HUDController.Instance.ShowPenIcon(false);
			HUDController.Instance.ShowPenBackground(false);
		}

		GameStateManager.Instance.EnterDesignMode();
	}

	void CloseDesignUI()
	{
		if (!isUIOpen) return;
		if (designUI == null) return;

		isUIOpen = false;
		designUI.SetActive(false);

		if (HUDController.Instance != null)
		{
			HUDController.Instance.ShowPenIcon(true);
			HUDController.Instance.ShowPenBackground(true);
		}

		GameStateManager.Instance.ExitDesignMode();
	}
}
