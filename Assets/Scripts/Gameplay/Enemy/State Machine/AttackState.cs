using UnityEngine;
using UnityEngine.AI;

public class AttackState : iStates
{
    private float _initTime;
    private float _chaseVelocity;
    private float _sightRadius;
    private float _speed;
    private bool _playerSpotted;
    private EnemyStateMachine _stateMachine;
    private NavMeshAgent _agent;
    private Rigidbody _enemyRigidbody;
    private Vector3 _targetDestination;

    public AttackState(EnemyStateMachine stateMachine, Rigidbody enemyRigidbody, NavMeshAgent agent, float chaseVelocity, float sightRadius, float speed)
    {
        _stateMachine = stateMachine;
        _enemyRigidbody = enemyRigidbody;
        _agent = agent;
        _chaseVelocity = chaseVelocity;
        _sightRadius = sightRadius;
        _speed = speed;
    }

    public override void OnEnter()
    {
        _initTime = 0;
    }

    public override void OnExit()
    {
        //Debug.Log("Exit");
    }

    public override void Update()
    {
        _playerSpotted = SearchPlayer();
        float step = _speed * Time.deltaTime;
        _enemyRigidbody.position = Vector3.MoveTowards(_enemyRigidbody.position, _targetDestination, step);
        if (Vector3.Distance(_enemyRigidbody.position, _targetDestination) < 0.001f)
            _targetDestination *= -1.0f;
        _initTime += Time.deltaTime;
        if (_playerSpotted)
            DashToPlayer();
        else
            _stateMachine.ChangeState(Global.IdleState);
    }

    private bool SearchPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(_enemyRigidbody.position, _sightRadius);

        foreach (Collider item in hitColliders)
        {
            if (item.tag.Equals(Global.PlayerTag))
            {
                _targetDestination = item.transform.position;
                _enemyRigidbody.transform.LookAt(_targetDestination);
                return true;
            }
        }
        return false;
    }

    private void DashToPlayer()
    {
        _speed = _chaseVelocity;
        _agent.SetDestination(_targetDestination);
    }
}
