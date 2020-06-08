using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private FloatReference horizontalAxis;
    [SerializeField] private FloatReference verticalAxis;
    [SerializeField] private FloatReference moveSpeed;

    public void Move()
    {
        var dualDirectionMultiplier = (horizontalAxis.Value != 0 && verticalAxis.Value != 0) ? 0.4f : 1;
        float newstraffe = horizontalAxis.Value * moveSpeed.Value * dualDirectionMultiplier * Time.deltaTime;
        float newtranslation = verticalAxis.Value * moveSpeed.Value * dualDirectionMultiplier * Time.deltaTime;
        transform.Translate(newstraffe, 0, newtranslation);
    }
}
