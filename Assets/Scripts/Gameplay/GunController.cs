﻿using System.Collections;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [Header("Bullet Variables")]
    [SerializeField] private BulletsRuntimeSet bullets;
    [SerializeField] private FloatReference actualMaxEnergy;
    [SerializeField] private FloatReference remainingEnergy;
    [SerializeField] private FloatReference energyBulletCost;
    [SerializeField] private FloatReference energyRegenerationAmount;
    [SerializeField] private FloatReference energyRegenerationTime;
    [SerializeField] private GameEvent playerShot;
    [SerializeField] private GameEvent energyRecharged;
    [SerializeField] private Transform bulletPosition;
    private bool keepRecharging;

    private void Start()
    {
        Initilize();
    }

    private void Initilize()
    {
        remainingEnergy.Value = actualMaxEnergy.Value;
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
                remainingEnergy.Value += energyRegenerationAmount.Value;
                currentTime = energyRegenerationTime.Value;
            }
            yield return null;
        }
    }

    public void ShootBullet()
    {
        var initialPosition = bulletPosition.position;
        var initialRotation = bulletPosition.rotation;

        for (int i = 0; i < bullets.Items.Count; i++)
        {
            if (!bullets.Items[i].activeInHierarchy)
            {
                bullets.Items[i].transform.localPosition = initialPosition;
                bullets.Items[i].transform.localRotation = initialRotation;
                bullets.Items[i].SetActive(true);
                remainingEnergy.Value -= energyBulletCost.Value;
                playerShot.Raise();
                energyRecharged.Raise();
                break;
            }
        }
    }
}
