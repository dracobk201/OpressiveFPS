using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameplayCanvasActions : MonoBehaviour
{
    [SerializeField] private FloatReference maxHealthPointsPunishment;
    [SerializeField] private FloatReference maxEnergyPointsPunishment;

    [Header("Player Variables")]
    [SerializeField] private FloatReference maxArmor;
    [SerializeField] private FloatReference actualArmor;
    [SerializeField] private FloatReference maxEnergy;
    [SerializeField] private FloatReference actualEnergy;
    [SerializeField] private Image armorGauge;
    [SerializeField] private Image energyGauge;
    private float armorPunishValue;
    private float energyPunishValue;

    private void Initialize()
    {
        armorPunishValue = maxArmor.Value * maxHealthPointsPunishment.Value;
        energyPunishValue = maxEnergy.Value * maxEnergyPointsPunishment.Value;
    }

    public void UpdateEnergyGauge()
    {
        if (maxEnergyPointsPunishment.Value < 1)
            energyGauge.fillAmount = (actualEnergy.Value / maxEnergy.Value) - energyPunishValue;
        else
            energyGauge.fillAmount = (actualEnergy.Value / maxEnergy.Value);

    }

    public void UpdateArmorGauge()
    {
        if (maxHealthPointsPunishment.Value < 1)
            armorGauge.fillAmount = (actualArmor.Value / maxArmor.Value) - armorPunishValue;
        else
            armorGauge.fillAmount = (actualArmor.Value / maxArmor.Value);
    }

    public void RestartStats()
    {
        StartCoroutine(Restart());
    }

    private IEnumerator Restart()
    {
        yield return new WaitForSeconds(0.1f);
        Initialize();
    }
}
