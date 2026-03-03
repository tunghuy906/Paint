using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class TilemapPainter : MonoBehaviour
{
	[Header("References")]
	public Tilemap designTilemap;
	public TileBase paintTile;
	public Camera mainCamera;

	[Header("Settings")]
	public bool allowOverwrite = true;

	void Update()
	{

		if (!GameStateManager.Instance.isDesignMode)
			return;

		HandlePaint();
	}

	void HandlePaint()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (EventSystem.current.IsPointerOverGameObject())
				return;

			var resource = ResourceManager.Instance.currentSelected;

			if (resource == null)
				return;

			if (!ResourceManager.Instance.CanPlace())
				return;

			Vector3 worldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
			Vector3Int cellPos = designTilemap.WorldToCell(worldPos);
			Vector3 snappedPos = designTilemap.GetCellCenterWorld(cellPos);

			if (resource.resourceType == ResourceType.Tile)
			{
				designTilemap.SetTile(cellPos, resource.tile);
			}
			else if (resource.resourceType == ResourceType.Prefab)
			{
				Instantiate(resource.prefab, snappedPos, Quaternion.identity);
			}

			ResourceManager.Instance.UseResource();
			Debug.Log(ResourceManager.Instance.currentSelected);

		}
	}



	void PaintAtPosition(Vector3 screenPosition)
	{
		if (mainCamera == null || designTilemap == null) return;

		screenPosition.z = Mathf.Abs(mainCamera.transform.position.z);
		Vector3 worldPos = mainCamera.ScreenToWorldPoint(screenPosition);
		Vector3Int cellPos = designTilemap.WorldToCell(worldPos);

		designTilemap.SetTile(cellPos, paintTile);
	}

}
