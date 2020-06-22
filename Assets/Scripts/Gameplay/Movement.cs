using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private FloatReference movementSpeedPunishment;
    [SerializeField] private FloatReference horizontalAxis;
    [SerializeField] private FloatReference verticalAxis;
    [SerializeField] private FloatReference moveSpeed;
    private float _moveSpeed;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _moveSpeed = moveSpeed.Value * movementSpeedPunishment.Value;
    }

    public void Move()
    {
        var dualDirectionMultiplier = (horizontalAxis.Value != 0 && verticalAxis.Value != 0) ? 0.4f : 1;
        float newstraffe = horizontalAxis.Value * _moveSpeed * dualDirectionMultiplier * Time.deltaTime;
        float newtranslation = verticalAxis.Value * _moveSpeed * dualDirectionMultiplier * Time.deltaTime;
        transform.Translate(newstraffe, 0, newtranslation);
    }

    public void RestartStats()
    {
        StartCoroutine(Restart());
    }

    private IEnumerator Restart()
    {
        yield return new WaitForSeconds(0.1f);
        Initialize();
    }
}
