using UnityEngine;
using UnityEngine.UI;

public class GameplayCanvasActions : MonoBehaviour
{
    [Header("Player Variables")]
    [SerializeField] private IntReference maxArmor;
    [SerializeField] private IntReference actualArmor;
    [SerializeField] private FloatReference maxEnergy;
    [SerializeField] private FloatReference actualEnergy;
    [SerializeField] private Image armorGauge;
    [SerializeField] private Image energyGauge;

    public void UpdateEnergyGauge()
    {
        energyGauge.fillAmount = actualEnergy.Value / maxEnergy.Value;
    }

    public void UpdateArmorGauge()
    {
        armorGauge.fillAmount = (float) actualArmor.Value / maxArmor.Value;
    }
}
