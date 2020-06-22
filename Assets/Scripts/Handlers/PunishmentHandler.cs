using UnityEngine;

public class PunishmentHandler : MonoBehaviour
{
    [SerializeField] private FloatReference maxHealthPointsPunishment;
    [SerializeField] private FloatReference maxEnergyPointsPunishment;
    [SerializeField] private FloatReference energyPointsRegenPunishment;
    [SerializeField] private FloatReference bulletEnergyCostPunishment;
    [SerializeField] private FloatReference bulletDamagePunishment;
    [SerializeField] private FloatReference bulletVelocityPunishment;
    [SerializeField] private FloatReference gunAccuracyPunishment;
    [SerializeField] private FloatReference movementSpeedPunishment;

    private void Awake()
    {
        maxHealthPointsPunishment.Value = 1;
        maxEnergyPointsPunishment.Value = 1;
        energyPointsRegenPunishment.Value = 1;
        bulletEnergyCostPunishment.Value = 1;
        bulletDamagePunishment.Value = 1;
        bulletVelocityPunishment.Value = 1;
        gunAccuracyPunishment.Value = 1;
        movementSpeedPunishment.Value = 1;
    }

    public void GeneratePunish()
    {
        float valueFactor = Random.Range(5, 10);
        float targetPunishIndex = Random.Range(0, 8);
        switch (targetPunishIndex)
        {
            case 0:
                maxHealthPointsPunishment.Value -= (valueFactor / 100);
                break;
            case 1:
                maxEnergyPointsPunishment.Value -= (valueFactor / 100);
                break;
            case 2:
                energyPointsRegenPunishment.Value -= (valueFactor / 100);
                break;
            case 3:
                bulletEnergyCostPunishment.Value += (valueFactor / 100);
                break;
            case 4:
                bulletDamagePunishment.Value -= (valueFactor / 100);
                break;
            case 5:
                bulletVelocityPunishment.Value -= (valueFactor / 100);
                break;
            case 6:
                gunAccuracyPunishment.Value += (valueFactor / 100);
                break;
            case 7:
                movementSpeedPunishment.Value -= (valueFactor / 100);
                break;
        }
    }
}
