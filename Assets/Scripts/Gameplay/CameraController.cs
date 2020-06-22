using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private FloatReference mouseVerticalAxis;
    [SerializeField] private FloatReference mouseHorizontalAxis;
    [SerializeField] private FloatReference minCameraRotation;
    [SerializeField] private FloatReference maxCameraRotation;
    [SerializeField] private FloatReference horizontalCameraSensitivity;
    [SerializeField] private FloatReference verticalCameraSensitivity;
    [SerializeField] private Camera playerCamera;
    private Rigidbody _playerRigidbody;
    private float rotAroundX, rotAroundY;

    private void Start()
    {
        _playerRigidbody = (TryGetComponent(out Rigidbody rigidbodyResult)) ? rigidbodyResult : gameObject.AddComponent<Rigidbody>();
        rotAroundX = transform.eulerAngles.x;
        rotAroundY = transform.eulerAngles.y;
    }

    public void RotateCamera()
    {
        rotAroundX += mouseHorizontalAxis.Value * horizontalCameraSensitivity.Value;
        rotAroundY += mouseVerticalAxis.Value * verticalCameraSensitivity.Value;

        rotAroundX = Mathf.Clamp(rotAroundX, minCameraRotation.Value, maxCameraRotation.Value);
        CameraRotation();
    }

    private void CameraRotation()
    {
        _playerRigidbody.rotation = Quaternion.Euler(0, rotAroundY, 0);
        playerCamera.transform.rotation = Quaternion.Euler(-rotAroundX, rotAroundY, 0);
    }
}