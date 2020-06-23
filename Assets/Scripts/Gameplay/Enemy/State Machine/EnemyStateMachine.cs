using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private FloatReference idleTime = null;
    [SerializeField] private FloatReference patrolTime = null;
    [SerializeField] private FloatReference patrolSwitchProbability = null;
    [SerializeField] private FloatReference chaseVelocity = null;
    [SerializeField] private FloatReference enemySpeed = null;
    [SerializeField] private FloatReference enemySightRadius = null;
    [SerializeField] private Rigidbody enemyRigidbody = null;

    private Dictionary<string, iStates> _map = new Dictionary<string, iStates>();
    private iStates _currentState = null;

    private void Start()
    {
        NavMeshAgent agent = (TryGetComponent<NavMeshAgent>(out NavMeshAgent agentResult)) ? agentResult : gameObject.AddComponent<NavMeshAgent>();
        iStates newIdleState = new IdleState(this, idleTime.Value);
        iStates newPatrolState = new PatrolState(this, enemyRigidbody, agent, patrolTime.Value, patrolSwitchProbability.Value, enemySightRadius.Value);
        iStates newAttackState = new AttackState(this, enemyRigidbody, agent, chaseVelocity.Value, enemySightRadius.Value, enemySpeed.Value);
        _map.Add(Global.IdleState, newIdleState);
        _map.Add(Global.PatrolState, newPatrolState);
        _map.Add(Global.AttackState, newAttackState);
        _currentState = newIdleState;
    }

    private void Update()
    {
        _currentState.Update();
    }

    public void ChangeState(string state)
    {
        _currentState.OnExit();
        _currentState = _map[state];
        _currentState.OnEnter();
    }
}
