using UnityEngine;

public class HUDController : MonoBehaviour
{
	public GameObject penIcon;

	private void Start()
	{
		penIcon.SetActive(false);
	}

	public void ShowPenIcon(bool show)
	{
		penIcon.SetActive(show);
	}
}
