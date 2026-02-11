using UnityEngine;
using UnityEngine.Tilemaps;

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
		// PC (chuột)
		if (Input.GetMouseButton(0))
		{
			PaintAtPosition(Input.mousePosition);
		}

		// Mobile (chạm)
		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);
			PaintAtPosition(touch.position);
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
