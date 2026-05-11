using UnityEngine;

public class cameraController : MonoBehaviour
{
    public Transform playerTransform;

    // distância da câmera
    public float offsetX = 2f;

    // posição fixa da câmera
    private float fixedY;
    private float fixedZ;

    void Start()
    {
        // guarda posição inicial da câmera
        fixedY = transform.position.y;
        fixedZ = transform.position.z;
    }

    void LateUpdate()
    {
        if (playerTransform != null)
        {
            transform.position = new Vector3(
                playerTransform.position.x + offsetX,
                fixedY,
                fixedZ
            );
        }
    }
}