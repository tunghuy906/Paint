using UnityEngine;

public class HUDController : MonoBehaviour
{
	public static HUDController Instance { get; private set; }
	public GameObject penIcon;
	public GameObject penBackground;

	private void Start()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
		penIcon.SetActive(false);
		if (penBackground != null)
			penBackground.SetActive(true);
	}

	public void ShowPenIcon(bool show)
	{
		penIcon.SetActive(show);
	}

	public void ShowPenBackground(bool show)
	{
		if (penBackground != null)
			penBackground.SetActive(show);
	}
}
