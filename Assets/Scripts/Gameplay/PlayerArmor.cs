using System.Collections;
using UnityEngine;

public class PlayerArmor : MonoBehaviour
{
    [SerializeField] private FloatReference maxHealthPointsPunishment = null;
    [SerializeField] private FloatReference playerMaxImpacts = null;
    [SerializeField] private FloatReference playerRemainingImpacts = null;
    [SerializeField] private GameEvent playerDamaged = null;
    [SerializeField] private GameEvent playerDefeated = null;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        playerRemainingImpacts.Value = playerMaxImpacts.Value * maxHealthPointsPunishment.Value;
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (var impact in collision.contacts)
        {
            if (impact.otherCollider.tag.Equals(Global.EnemyTag))
                DamageReceived();
        }
    }

    private void DamageReceived()
    {
        playerRemainingImpacts.Value--;
        playerDamaged.Raise();
        if (playerRemainingImpacts.Value <= 0)
        {
            playerDefeated.Raise();
            StartCoroutine(Restart());
        }
    }

    public void RestartStats()
    {
        transform.position = new Vector3(0, 0.2f, 0);
        transform.rotation = Quaternion.identity;
        StartCoroutine(Restart());
    }

    private IEnumerator Restart()
    {
        yield return new WaitForSeconds(0.1f);
        Initialize();
    }
}
