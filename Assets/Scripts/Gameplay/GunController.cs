using System.Collections;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private FloatReference maxEnergyPointsPunishment = null;
    [SerializeField] private FloatReference energyPointsRegenPunishment = null;
    [SerializeField] private FloatReference bulletEnergyCostPunishment = null;
    [SerializeField] private FloatReference bulletDamagePunishment = null;
    [SerializeField] private FloatReference gunAccuracyPunishment = null;

    [Header("Bullet Variables")]
    [SerializeField] private BulletsRuntimeSet bullets = null;
    [SerializeField] private FloatReference actualMaxEnergy = null;
    [SerializeField] private FloatReference remainingEnergy = null;
    [SerializeField] private FloatReference energyBulletCost = null;
    [SerializeField] private FloatReference energyRegenerationAmount = null;
    [SerializeField] private FloatReference energyRegenerationTime = null;
    [SerializeField] private FloatReference bulletDamage = null;
    [SerializeField] private FloatReference actualBulletDamage = null;
    [SerializeField] private FloatReference accuracyFactor = null;
    [SerializeField] private GameEvent playerShot = null;
    [SerializeField] private GameEvent energyRecharged = null;
    [SerializeField] private Transform bulletPosition = null;
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
