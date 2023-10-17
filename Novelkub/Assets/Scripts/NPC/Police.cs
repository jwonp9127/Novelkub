using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Police : MonoBehaviour
{
    public enum AIState  //ai상태
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
    public float detectDistance;  //탐지거리
    public float safeDistance;   //안전거리

    [Header("Wandering")]
    public float minWanderDistance;  //방황 최소거리 
    public float maxWanderDistance;  // 방황 최대거리
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
        SetState(AIState.Wandering);  //처음에 방황으로 시작
    }

    private void Update()
    {


        
        playerDistance = Vector3.Distance(transform.position, player.transform.position); //플레이어와 자신사이의 거리
        // 여기는 문젱없음 Debug.Log(playerDistance);
        animator.SetBool("Moving", aiState != AIState.Idle);//가만히 있는것이 아니면 움직이기

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
        if (agent.remainingDistance < 0.1f)  //이동거리가 가까우면
        {
            agent.SetDestination(GetFleeLocation()); //목적지를 찾기
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
            if (agent.CalculatePath(player.transform.position, path)) //경로를 새로검색한다.
            {
                
                agent.SetDestination(player.transform.position); //목적기를 찾기
            }
            else
            {
                SetState(AIState.Fleeing);
            }
        }
        else
        {

            agent.isStopped = true;  //데미지를 입하는 부분 
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
        if (aiState == AIState.Wandering && agent.remainingDistance < 0.1f) //방황하는 중이고, 남은거리가 0.1보다 작다
        {

            SetState(AIState.Idle);
            Invoke("WanderToNewLocation", Random.Range(minWanderWaitTime, maxWanderWaitTime)); //새로운 로케이션하는 것이 지연시키는 것
        }



        if (playerDistance < detectDistance)  //거리안에 들오았다면
        {
            SetState(AIState.Attacking);
        }
    }

    bool IsPlaterInFireldOfView() //시아각에 들어오는지
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;//거리구하기
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        return angle < fieldOfView * 0.5f;
    }

    private void SetState(AIState newState) //상태에 따라 변화 구하기
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

    void WanderToNewLocation()  //새로운 거리를 구하는 방법
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
        while (Vector3.Distance(transform.position, hit.position) < detectDistance) //hit와 해당위치의 거리가 탐지거리보다 작다면
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



    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log(other.transform.position);
            // Debug.Log("플레이어와 닿았다. 플레이어를 이동시키는 메서드를 만들어야 한다.");
            //other.GetComponent<Transform>().position = spwamPosition.transform.position;
             other.transform.position = spwamPosition.position;
            Debug.Log(other.transform.position);
            //other.gameObject.transform.position = spwamPosition.transform.position;
            // Debug.Log(other.transform.position);
            // Debug.Log(spwamPosition.transform.position);
        }

    }


    void Die()
    {

        Destroy(gameObject);
    }

    IEnumerator DamageFlash() //깜빡이는 것임.
    {
        for (int x = 0; x < meshRenderers.Length; x++)
            meshRenderers[x].material.color = new Color(1.0f, 0.6f, 0.6f);

        yield return new WaitForSeconds(0.1f);
        for (int x = 0; x < meshRenderers.Length; x++)
            meshRenderers[x].material.color = Color.white;
    }

    IEnumerator DieAni() //깜빡이는 것임.
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
