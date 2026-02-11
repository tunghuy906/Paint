using UnityEngine;

public class DesignCameraController : MonoBehaviour
{
    public Camera designCamera;

    [Header("Move Settings")]
    public float moveSpeed = 10f;
    public float stageStartX = 0f;
    public float stageEndX = 50f;

    float camHalfWidth;
    float moveDir = 0f; // -1 = trái, 1 = phải

    void Start()
    {
        camHalfWidth = designCamera.orthographicSize * designCamera.aspect;
    }

    void Update()
    {
        if (!GameStateManager.Instance.isDesignMode)
            return;

        // PC input
        float keyboardInput = Input.GetAxisRaw("Horizontal");
        if (keyboardInput != 0)
            moveDir = keyboardInput;

        MoveCamera();

        Debug.Log(designCamera.transform.position);
    }

    void MoveCamera()
    {
        if (moveDir == 0) return;

        float minX = stageStartX + camHalfWidth;
        float maxX = stageEndX - camHalfWidth;

        Vector3 pos = designCamera.transform.position;
        pos.x += moveDir * moveSpeed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);

        designCamera.transform.position = pos;
    }

    // 📱 UI button gọi
    public void MoveLeft(bool isHolding)
    {
        moveDir = isHolding ? -1f : 0f;
    }

    public void MoveRight(bool isHolding)
    {
        moveDir = isHolding ? 1f : 0f;
    }
}
