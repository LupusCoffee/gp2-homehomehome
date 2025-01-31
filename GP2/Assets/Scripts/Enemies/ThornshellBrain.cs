using UnityEngine;
using UnityEngine.AI;

public class ThornshellBrain : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] float speed;
    MindControl mindControl;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        mindControl = GetComponent<MindControl>();
    }

    private void Start()
    {
        agent.SetDestination(Player.Instance.transform.position);
        InvokeRepeating("SetDestination", 0, 0.1f);
    }

    private void Update()
    {
        if (mindControl.isMindControlled)
        {
            agent.ResetPath();
        }
    }

    private void SetDestination()
    {
        if (Vector3.Distance(gameObject.transform.position, Player.Instance.gameObject.transform.position) < 8)
            agent.SetDestination(Player.Instance.transform.position);
        else
            agent.ResetPath();
    }
}
