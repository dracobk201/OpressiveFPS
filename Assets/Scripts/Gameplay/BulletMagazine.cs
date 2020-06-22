using UnityEngine;

public class BulletMagazine : MonoBehaviour
{
    [SerializeField]
    private BulletsRuntimeSet playerBullets;
    [SerializeField]
    private IntReference playerBulletsPool;
    [SerializeField]
    private GameObject playerBulletPrefab;

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
