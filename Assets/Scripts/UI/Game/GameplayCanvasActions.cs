using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameplayCanvasActions : MonoBehaviour
{
    [SerializeField] private FloatReference maxHealthPointsPunishment = null;
    [SerializeField] private FloatReference maxEnergyPointsPunishment = null;

    [Header("Player Variables")]
    [SerializeField] private FloatReference maxArmor = null;
    [SerializeField] private FloatReference actualArmor = null;
    [SerializeField] private FloatReference maxEnergy = null;
    [SerializeField] private FloatReference actualEnergy = null;
    [SerializeField] private Image armorGauge = null;
    [SerializeField] private Image energyGauge = null;
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
