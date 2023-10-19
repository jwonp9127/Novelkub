using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Police : MonoBehaviour
{
    public enum AIState  //ai����
    {
        Idle,
        Wandering,
        Attacking,
        Fleeing
    }

    public Player player;
    public Transform spwamPosition;


    [Header("Stats")]
    public float walkSpeed;
    public float runSpeed;


    [Header("AI")]
    private AIState aiState;
    public float detectDistance;  //Ž���Ÿ�
    public float safeDistance;   //�����Ÿ�

    [Header("Wandering")]
    public float minWanderDistance;  //��Ȳ �ּҰŸ� 
    public float maxWanderDistance;  // ��Ȳ �ִ�Ÿ�
    public float minWanderWaitTime;
    public float maxWanderWaitTime;


    [Header("Combat")]
    public int damage;
    public float attackRate;
    private float lastAttackTime;
    public float attackDistance;

    private float playerDistance;

    public float fieldOfView = 120f;

    private NavMeshAgent agent;
    private Animator animator;
    //public Collider collider;
    private SkinnedMeshRenderer[] meshRenderers;

    private void Awake()
    {

        //player = GetComponent<Player>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();// meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
                                                                       //  collider = GetComponentInChildren<Collider>();
    }

    private void Start()
    {
        SetState(AIState.Wandering);  //ó���� ��Ȳ���� ����
    }

    private void Update()
    {



        playerDistance = Vector3.Distance(transform.position, player.transform.position); //�÷��̾�� �ڽŻ����� �Ÿ�
        // ����� �������� Debug.Log(playerDistance);
        animator.SetBool("Moving", aiState != AIState.Idle);//������ �ִ°��� �ƴϸ� �����̱�

        switch (aiState)
        {
            case AIState.Idle: PassiveUpdate(); break;
            case AIState.Wandering: PassiveUpdate(); break;
            case AIState.Attacking: AttackingUpdate(); break;
            case AIState.Fleeing: FleeingUpdate(); break;
        }
        // Debug.Log(playerDistance);
    }

    private void FleeingUpdate()
    {
        if (agent.remainingDistance < 0.1f)  //�̵��Ÿ��� ������
        {
            agent.SetDestination(GetFleeLocation()); //�������� ã��
        }
        else
        {
            SetState(AIState.Wandering);
        }
    }

    private void AttackingUpdate()
    {
        if (playerDistance > attackDistance || !IsPlaterInFireldOfView())
        {
            agent.isStopped = false;
            NavMeshPath path = new NavMeshPath();
            if (agent.CalculatePath(player.transform.position, path)) //��θ� ���ΰ˻��Ѵ�.
            {

                agent.SetDestination(player.transform.position); //�����⸦ ã��
            }
            else
            {
                SetState(AIState.Fleeing);
            }
        }
        else
        {

            agent.isStopped = true;  //�������� ���ϴ� �κ� 
            if (Time.time - lastAttackTime > attackRate)
            {
                animator.SetTrigger("Attack");


                lastAttackTime = Time.time;

                animator.speed = 1;
                fieldOfView = 60f;

                fieldOfView = 120f;
            }
            SetState(AIState.Wandering);
        }
    }

    private void PassiveUpdate()
    {
        if (aiState == AIState.Wandering && agent.remainingDistance < 0.1f) //��Ȳ�ϴ� ���̰�, �����Ÿ��� 0.1���� �۴�
        {

            SetState(AIState.Idle);
            Invoke("WanderToNewLocation", Random.Range(minWanderWaitTime, maxWanderWaitTime)); //���ο� �����̼��ϴ� ���� ������Ű�� ��
        }



        if (playerDistance < detectDistance)  //�Ÿ��ȿ� ����Ҵٸ�
        {
            SetState(AIState.Attacking);
        }
    }

    bool IsPlaterInFireldOfView() //�þư��� ��������
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;//�Ÿ����ϱ�
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        return angle < fieldOfView * 0.5f;
    }

    private void SetState(AIState newState) //���¿� ���� ��ȭ ���ϱ�
    {
        aiState = newState;
        switch (aiState)
        {
            case AIState.Idle:
                {
                    agent.speed = walkSpeed;
                    agent.isStopped = true;
                }
                break;
            case AIState.Wandering:
                {
                    agent.speed = walkSpeed;
                    agent.isStopped = false;
                }
                break;

            case AIState.Attacking:
                {
                    agent.speed = runSpeed;
                    agent.isStopped = false;
                }
                break;
            case AIState.Fleeing:
                {
                    agent.speed = runSpeed;
                    agent.isStopped = false;
                }
                break;
        }

        animator.speed = agent.speed / walkSpeed;
    }

    void WanderToNewLocation()  //���ο� �Ÿ��� ���ϴ� ���
    {
        if (aiState != AIState.Idle)
        {
            return;
        }
        SetState(AIState.Wandering);
        agent.SetDestination(GetWanderLocation());
    }


    Vector3 GetWanderLocation() //
    {
        NavMeshHit hit;

        NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * Random.Range(minWanderDistance, maxWanderDistance)), out hit, maxWanderDistance, NavMesh.AllAreas);

        int i = 0;
        while (Vector3.Distance(transform.position, hit.position) < detectDistance) //hit�� �ش���ġ�� �Ÿ��� Ž���Ÿ����� �۴ٸ�
        {
            NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * Random.Range(minWanderDistance, maxWanderDistance)), out hit, maxWanderDistance, NavMesh.AllAreas);
            i++;
            if (i == 30)
                break;
        }

        return hit.position;
    }

    Vector3 GetFleeLocation()
    {
        NavMeshHit hit;

        NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * safeDistance), out hit, maxWanderDistance, NavMesh.AllAreas);

        int i = 0;
        while (GetDestinationAngle(hit.position) > 90 || playerDistance < safeDistance)
        {

            NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * safeDistance), out hit, maxWanderDistance, NavMesh.AllAreas);
            i++;
            if (i == 30)
                break;
        }

        return hit.position;
    }

    float GetDestinationAngle(Vector3 targetPos)
    {
        return Vector3.Angle(transform.position - player.transform.position, transform.position + targetPos);
    }



    public void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            player.Teleport(spwamPosition.position);

        }

    }
    //void OnControllerColliderHit(ControllerColliderHit hit)
    //{
    //    if (hit.collider.CompareTag("Player"))
    //    {
    //        //Debug.Log(hit.collider.tag.transform.position);
    //        // Debug.Log("�÷��̾�� ��Ҵ�. �÷��̾ �̵���Ű�� �޼��带 ������ �Ѵ�.");
    //        //other.GetComponent<Transform>().position = spwamPosition.transform.position;
    //        hit.collider.transform.position = spwamPosition.position;
    //        //Debug.Log(collider.transform.position);
    //        //other.gameObject.transform.position = spwamPosition.transform.position;
    //        // Debug.Log(other.transform.position);
    //        // Debug.Log(spwamPosition.transform.position);
    //    }
    //}


    void Die()
    {

        Destroy(gameObject);
    }

    IEnumerator DamageFlash() //�����̴� ����.
    {
        for (int x = 0; x < meshRenderers.Length; x++)
            meshRenderers[x].material.color = new Color(1.0f, 0.6f, 0.6f);

        yield return new WaitForSeconds(0.1f);
        for (int x = 0; x < meshRenderers.Length; x++)
            meshRenderers[x].material.color = Color.white;
    }

    IEnumerator DieAni() //�����̴� ����.
    {
        //agent.speed = 0;
        animator.SetTrigger("Die");
        //animator.SetBool("Die", true);
        yield return new WaitForSeconds(8f);
        // DropItem();
        //BossNpc.SetActive(true);
        Destroy(gameObject);
    }







}