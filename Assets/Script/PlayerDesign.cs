using UnityEngine;

public class PlayerDesign : MonoBehaviour
{
	public bool hasPen = false;
	private HUDController hud;

	private void Start()
	{
		hud = FindFirstObjectByType<HUDController>();

		hasPen = false;

		if (hud != null)
			hud.ShowPenIcon(false);
	}

	public void PickupPen()
	{
		hasPen = true;

		if (hud != null)
			hud.ShowPenIcon(true);

		Debug.Log("Đã nhặt được bút");
	}
}
