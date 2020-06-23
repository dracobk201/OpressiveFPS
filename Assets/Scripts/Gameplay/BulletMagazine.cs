using UnityEngine;

public class BulletMagazine : MonoBehaviour
{
    [SerializeField] private BulletsRuntimeSet playerBullets = null;
    [SerializeField] private IntReference playerBulletsPool = null;
    [SerializeField] private GameObject playerBulletPrefab = null;

    private void Awake()
    {
        playerBullets.Items.Clear();
        InstantiateBullets();
    }

    private void InstantiateBullets()
    {
        for (int i = 0; i < playerBulletsPool.Value; i++)
        {
            GameObject bullet = Instantiate(playerBulletPrefab) as GameObject;
            bullet.GetComponent<Transform>().SetParent(transform);
            bullet.SetActive(false);
            playerBullets.Add(bullet);
        }
    }
}
