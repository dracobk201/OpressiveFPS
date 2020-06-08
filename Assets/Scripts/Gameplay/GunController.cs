using UnityEngine;

public class GunController : MonoBehaviour
{
    [Header("Bullet Variables")]
    [SerializeField]
    private BulletsRuntimeSet Bullets;
    [SerializeField]
    private GameEvent PlayerShot;
    [SerializeField]
    private Transform bulletPosition;

    public void ShootBullet()
    {
        var initialPosition = bulletPosition.position;
        var initialRotation = bulletPosition.rotation;

        for (int i = 0; i < Bullets.Items.Count; i++)
        {
            if (!Bullets.Items[i].activeInHierarchy)
            {
                Bullets.Items[i].transform.localPosition = initialPosition;
                Bullets.Items[i].transform.localRotation = initialRotation;
                Bullets.Items[i].SetActive(true);
                PlayerShot.Raise();
                break;
            }
        }
    }
}
