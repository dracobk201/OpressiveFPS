﻿using UnityEngine;
using TMPro;

public class PunishmentCanvasActions : MonoBehaviour
{
    [SerializeField] private FloatReference maxHealthPointsPunishment;
    [SerializeField] private FloatReference maxEnergyPointsPunishment;
    [SerializeField] private FloatReference energyPointsRegenPunishment;
    [SerializeField] private FloatReference bulletEnergyCostPunishment;
    [SerializeField] private FloatReference bulletDamagePunishment;
    [SerializeField] private FloatReference bulletVelocityPunishment;
    [SerializeField] private FloatReference gunAccuracyPunishment;
    [SerializeField] private FloatReference movementSpeedPunishment;
    [SerializeField] private TextMeshProUGUI punishmentsText;

    private void Start()
    {
        RefreshPunishmentStats();
    }

    public void RefreshPunishmentStats()
    {
        string newStats = string.Empty;
        if (maxHealthPointsPunishment.Value != 1)
            newStats += $"{maxHealthPointsPunishment.Value * 100}% Max HP \n";
        if (maxEnergyPointsPunishment.Value != 1)
            newStats += $"{maxEnergyPointsPunishment.Value * 100}% Max EP \n";
        if (energyPointsRegenPunishment.Value != 1)
            newStats += $"{energyPointsRegenPunishment.Value * 100}% EP Regen \n";
        if (bulletEnergyCostPunishment.Value != 1)
            newStats += $"{bulletEnergyCostPunishment.Value * 100}% Bullet Cost \n";
        if (bulletDamagePunishment.Value != 1)
            newStats += $"{bulletDamagePunishment.Value * 100}% Bullet DMG \n";
        if (bulletVelocityPunishment.Value != 1)
            newStats += $"{bulletVelocityPunishment.Value * 100}% Bullet Spd \n";
        if (gunAccuracyPunishment.Value != 1)
            newStats += $"{gunAccuracyPunishment.Value * 100}% Accuracy \n";
        if (movementSpeedPunishment.Value != 1)
            newStats += $"{movementSpeedPunishment.Value * 100}% Move Spd";
        punishmentsText.text = newStats;
    }
}
