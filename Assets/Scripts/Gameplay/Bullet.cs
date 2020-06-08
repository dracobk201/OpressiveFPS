﻿using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Bullet Variables
    [Header("Bullet Variables")]
    [SerializeField]
    private FloatReference bulletVelocity;
    [SerializeField]
    private FloatReference bulletTimeOfLife;
    [SerializeField]
    private GameEvent enemyImpacted;
    #endregion

    private void OnEnable()
    {
        TryGetComponent(out Rigidbody bulletRigidbody);
        StartCoroutine(AutoDestruction());
        bulletRigidbody.velocity = Vector2.zero;
        bulletRigidbody.AddForce(transform.forward * bulletVelocity.Value, ForceMode.Impulse);
    }

    private void Destroy()
    {
        transform.rotation = Quaternion.identity;
        gameObject.SetActive(false);
    }

    private IEnumerator AutoDestruction()
    {
        yield return new WaitForSeconds(bulletTimeOfLife.Value);
        Destroy();
    }

    private void OnTriggerEnter(Collider other)
    {
        string targetTag = other.tag;

        if (targetTag.Equals(Global.EnemyTag) && gameObject.tag.Equals(Global.PlayerBulletTag))
        {
            enemyImpacted.Raise();
            Destroy();
        }
    }
}
