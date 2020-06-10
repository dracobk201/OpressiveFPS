using UnityEngine;

public class Armor : MonoBehaviour
{
    [SerializeField] private IntReference playerActualMaxImpacts;
    [SerializeField] private IntReference playerRemainingImpacts;
    [SerializeField] private GameEvent playerDamaged;
    [SerializeField] private GameEvent playerDefeated;

    private void Start()
    {
        Initilize();
    }

    private void Initilize()
    {
        playerRemainingImpacts.Value = playerActualMaxImpacts.Value;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals(Global.EnemyTag))
            DamageReceived();
    }

    private void DamageReceived()
    {
        playerRemainingImpacts.Value--;
        playerDamaged.Raise();
        if (playerRemainingImpacts.Value <= 0)
        {
            transform.position = Vector2.zero;
            playerDefeated.Raise();
            gameObject.SetActive(false);
        }
    }
}
