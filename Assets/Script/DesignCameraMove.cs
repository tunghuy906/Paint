using UnityEngine;

public class DesignCameraMove : MonoBehaviour
{
    public float moveSpeed = 10f;

    void Update()
    {
        if (!GameStateManager.Instance.isDesignMode)
            return;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(h, v, 0f);

        if (move != Vector3.zero)
        {
            Debug.Log("Design Camera Moving: " + move);
        }

        transform.position += move * moveSpeed * Time.unscaledDeltaTime;
    }
}
