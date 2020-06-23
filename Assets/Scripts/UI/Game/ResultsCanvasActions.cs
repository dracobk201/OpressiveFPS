using UnityEngine;
using TMPro;

public class ResultsCanvasActions : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI punishmentsText = null;
    [SerializeField] private FloatReference maxHealthPointsPunishment = null;
    [SerializeField] private FloatReference maxEnergyPointsPunishment = null;
    [SerializeField] private FloatReference energyPointsRegenPunishment = null;
    [SerializeField] private FloatReference bulletEnergyCostPunishment = null;
    [SerializeField] private FloatReference bulletDamagePunishment = null;
    [SerializeField] private FloatReference bulletVelocityPunishment = null;
    [SerializeField] private FloatReference gunAccuracyPunishment = null;
    [SerializeField] private FloatReference movementSpeedPunishment = null;
    [SerializeField] private StringReference sceneToChange = null;
    [SerializeField] private GameEvent changeSceneEvent = null;

    public void ShowInfo()
    {
        Time.timeScale = 0.05f;
        string newStats = string.Empty;
        if (maxHealthPointsPunishment.Value < 1)
            newStats += $"{maxHealthPointsPunishment.Value * 100}% Max HP \n";
        if (maxEnergyPointsPunishment.Value < 1)
            newStats += $"{maxEnergyPointsPunishment.Value * 100}% Max EP \n";
        if (energyPointsRegenPunishment.Value < 1)
            newStats += $"{energyPointsRegenPunishment.Value * 100}% EP Regen \n";
        if (bulletEnergyCostPunishment.Value < 1)
            newStats += $"{bulletEnergyCostPunishment.Value * 100}% Bullet Cost \n";
        if (bulletDamagePunishment.Value < 1)
            newStats += $"{bulletDamagePunishment.Value * 100}% Bullet DMG \n";
        if (bulletVelocityPunishment.Value < 1)
            newStats += $"{bulletVelocityPunishment.Value * 100}% Bullet Spd \n";
        if (gunAccuracyPunishment.Value < 1)
            newStats += $"{gunAccuracyPunishment.Value * 100}% Accuracy \n";
        if (movementSpeedPunishment.Value < 1)
            newStats += $"{movementSpeedPunishment.Value * 100}% Move Spd";
        punishmentsText.text = newStats;
    }

    public void RestartScene()
    {
        sceneToChange.Value = Global.FirstLevelScene;
        changeSceneEvent.Raise();
    }
}
