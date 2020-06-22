using System.Collections;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private FloatReference maxEnergyPointsPunishment;
    [SerializeField] private FloatReference energyPointsRegenPunishment;
    [SerializeField] private FloatReference bulletEnergyCostPunishment;
    [SerializeField] private FloatReference bulletDamagePunishment;
    [SerializeField] private FloatReference gunAccuracyPunishment;

    [Header("Bullet Variables")]
    [SerializeField] private BulletsRuntimeSet bullets;
    [SerializeField] private FloatReference actualMaxEnergy;
    [SerializeField] private FloatReference remainingEnergy;
    [SerializeField] private FloatReference energyBulletCost;
    [SerializeField] private FloatReference energyRegenerationAmount;
    [SerializeField] private FloatReference energyRegenerationTime;
    [SerializeField] private FloatReference bulletDamage;
    [SerializeField] private FloatReference actualBulletDamage;
    [SerializeField] private FloatReference accuracyFactor;
    [SerializeField] private GameEvent playerShot;
    [SerializeField] private GameEvent energyRecharged;
    [SerializeField] private Transform bulletPosition;
    private bool keepRecharging;
    private float actualEnergyRegen;
    private float actualBulletCost;
    private float actualAccuracyFactor;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        remainingEnergy.Value = actualMaxEnergy.Value * maxEnergyPointsPunishment.Value;
        actualEnergyRegen = energyRegenerationAmount.Value * energyPointsRegenPunishment.Value;
        actualBulletCost = energyBulletCost.Value * bulletEnergyCostPunishment.Value;
        actualBulletDamage.Value = bulletDamage.Value * bulletDamagePunishment.Value;
        actualAccuracyFactor = accuracyFactor.Value * gunAccuracyPunishment.Value;
        keepRecharging = true;
        StartCoroutine(RechargeEnergy());
    }


    private IEnumerator RechargeEnergy()
    {
        var currentTime = energyRegenerationTime.Value;
        while (keepRecharging)
        {
            currentTime -= Time.deltaTime;
            if (currentTime < 0)
            {
                energyRecharged.Raise();
                remainingEnergy.Value += actualEnergyRegen;
                currentTime = energyRegenerationTime.Value;
            }
            yield return null;
        }
    }

    public void ShootBullet()
    {
        if (remainingEnergy.Value < actualBulletCost) return;

        var initialPosition = bulletPosition.position;
        var initialRotation = bulletPosition.rotation;

        for (int i = 0; i < bullets.Items.Count; i++)
        {
            if (!bullets.Items[i].activeInHierarchy)
            {
                bullets.Items[i].transform.localPosition = initialPosition;
                bullets.Items[i].transform.localRotation = initialRotation;

                var randomNumberX = Random.Range(-actualAccuracyFactor, actualAccuracyFactor);
                var randomNumberY = Random.Range(-actualAccuracyFactor, actualAccuracyFactor);
                var randomNumberZ = Random.Range(-actualAccuracyFactor, actualAccuracyFactor);
                bullets.Items[i].transform.Rotate(randomNumberX, randomNumberY, randomNumberZ);

                bullets.Items[i].SetActive(true);
                remainingEnergy.Value -= actualBulletCost;
                playerShot.Raise();
                energyRecharged.Raise();
                break;
            }
        }
    }

    public void RestartStats()
    {
        StartCoroutine(Restart());
    }

    private IEnumerator Restart()
    {
        keepRecharging = false;
        yield return new WaitForSeconds(0.1f);
        Initialize();
    }

}
