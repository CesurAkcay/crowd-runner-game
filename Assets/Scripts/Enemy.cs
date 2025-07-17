using UnityEngine;

public class Enemy : MonoBehaviour
{
    enum State {Idle, Running}

    [Header("Settings")]
    [SerializeField] private float searchRadius;
    [SerializeField] private float moveSpeed;
    private State currentState;
    private Transform targetRunner;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ManageState();
    }

    private void ManageState()
    {
        switch (currentState)
        {
            case State.Idle:
                SearchForTarget();
                break;
            case State.Running:
                RunTowardsTarget();
                break;
        }
    }

    private void SearchForTarget()
    {
        //OverlapSphere creates a sphere around the enemy to detect nearby runners
        Collider[] detectedColliiders = Physics.OverlapSphere(transform.position, searchRadius);     
        for (int i = 0; i < detectedColliiders.Length; i++)
        {
            if(detectedColliiders[i].TryGetComponent(out Runner runner))
            {
                if (runner.IsTarget()) // Check if the runner is already a target
                    continue;

                runner.SetTarget();
                targetRunner = runner.transform; // Just in order to store the target runner

                StartRunningTowardTarger();
            }
        }
    }

    // This is a method to switch from idle state to running state
    private void StartRunningTowardTarger()
    {
        currentState = State.Running;     //Grab the Animator component and play the run animation
        GetComponent<Animator>().Play("Run");
    }
    private void RunTowardsTarget()
    {
        if (targetRunner == null)
        {
            currentState = State.Idle;
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetRunner.position, 
            Time.deltaTime * moveSpeed);

        if (Vector3.Distance(transform.position, targetRunner.position) < .1f) 
        { 
            Destroy(targetRunner.gameObject); // Destroy the target runner when reached
            Destroy(gameObject); // Destroy the enemy when it reaches the target
        }
    }
}
