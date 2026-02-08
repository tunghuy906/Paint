using UnityEngine;

public class PenPickup : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			PlayerDesign playerDesign = other.GetComponent<PlayerDesign>();
			if (playerDesign != null)
			{
				playerDesign.PickupPen();
				Destroy(gameObject); 
			}
		}
	}
}
