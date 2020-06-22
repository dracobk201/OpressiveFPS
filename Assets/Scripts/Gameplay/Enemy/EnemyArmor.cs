using UnityEngine;

public class EnemyArmor : MonoBehaviour
{
    [SerializeField] private FloatReference enemyMaxImpacts;
    [SerializeField] private FloatReference actualPlayerBulletDamage;
    [SerializeField] private GameEvent enemyDefeated;
    private float _enemyRemainingImpacts;

    private void Start()
    {
        Initilize();
    }

    private void Initilize()
    {
        _enemyRemainingImpacts = enemyMaxImpacts.Value;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals(Global.PlayerBulletTag))
            DamageReceived();
    }

    private void DamageReceived()
    {
        _enemyRemainingImpacts-= actualPlayerBulletDamage.Value;
        if (_enemyRemainingImpacts <= 0)
        {
            transform.position = Vector3.zero;
            enemyDefeated.Raise();
            gameObject.SetActive(false);
        }
    }

}
