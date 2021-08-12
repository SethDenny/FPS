using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public AudioSource audioSrc;
    public Animator animator;
    public float lookRadius = 10f;
    NavMeshAgent agent;

    Transform target; 
    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if(distance <= lookRadius)
        {
            animator.SetBool("isRunning", true);
            if (!audioSrc.isPlaying)
            {
                Step();
            }
            agent.SetDestination(target.position);
            
            FaceTarget();
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }


    [SerializeField]
    private AudioClip[] monsterClips;

    private void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    private void Step()
    {
        AudioClip clip = GetRandomClip();
        audioSrc.PlayOneShot(clip);
        
    }

    private AudioClip GetRandomClip()
    {
        return monsterClips[UnityEngine.Random.Range(0, monsterClips.Length)];
    }
}
