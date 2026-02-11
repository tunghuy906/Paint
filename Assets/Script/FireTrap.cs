using UnityEngine;

public class FireTrap : MonoBehaviour
{
	private bool isUsed = false; 

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (isUsed) return;

		if (other.CompareTag("Zombie"))
		{
			isUsed = true;

			// Gọi hàm chết của zombie
			ZombieController zombie = other.GetComponent<ZombieController>();
			if (zombie != null)
			{
				zombie.Die();
			}

			// Huỷ đống lửa
			Destroy(gameObject);
		}
	}
}
