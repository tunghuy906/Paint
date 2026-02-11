using System.Collections;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
	public static ZombieManager Instance;

	[Header("Zombie Prefab")]
	public GameObject zombieNormalPrefab;

	[Header("Spawn Settings")]
	public Transform[] spawnPoints;
	public float spawnDelay = 2f;
	public int maxZombieAlive = 5;

	private int currentZombieCount;

	void Awake()
	{
		if (Instance == null)
			Instance = this;
		else
			Destroy(gameObject);
	}

	void Start()
	{
		StartCoroutine(SpawnZombieRoutine());
	}

	IEnumerator SpawnZombieRoutine()
	{
		while (true)
		{
			if (currentZombieCount < maxZombieAlive)
			{
				SpawnZombieNormal();
			}

			yield return new WaitForSeconds(spawnDelay);
		}
	}

	void SpawnZombieNormal()
	{
		Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

		Instantiate(
			zombieNormalPrefab,
			spawnPoint.position,
			Quaternion.identity
		);

		currentZombieCount++;
	}

	public void OnZombieDead()
	{
		currentZombieCount--;
	}
}
