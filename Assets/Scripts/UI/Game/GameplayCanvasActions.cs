using UnityEngine;
using UnityEngine.UI;

public class GameplayCanvasActions : MonoBehaviour
{
    [Header("Player Variables")]
    [SerializeField] private IntReference maxArmor;
    [SerializeField] private IntReference actualArmor;
    [SerializeField] private Image armorGauge;

    public void UpdateArmorGauge()
    {
        armorGauge.fillAmount = (float) actualArmor.Value / maxArmor.Value;
    }
}
