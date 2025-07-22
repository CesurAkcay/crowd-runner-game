using System;
using TMPro;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using Random = UnityEngine.Random;
public class Enemy : MonoBehaviour
{
    enum State { Idle, Running }

    [Header("Settings")]
    [SerializeField] private float searchRadius;
    [SerializeField] private float moveSpeed;
    [SerializeField] private int runnerThreshold = 3;

    private State currentState;
    private Transform targetRunner;

    [Header("Events")]
    public static Action onRunnerDied; // Event to notify when a runner is killed by the enemy
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Add a small delay to prevent immediate conflicts
        Invoke(nameof(StartSearching), 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        // Only manage state if we are still alive
        if(this != null)
        {
            ManageState();
        }
    }

    private void StartSearching()
    {
        if(currentState == State.Idle)
        {
            SearchForTarget(); 
        }
    }

    private void ManageState()
    {
        switch (currentState)
        {
            case State.Idle:
                // Only search periodically to avoid constant searching,not every frame
                if (Time.frameCount % 30 == 0) // Every 30 frames
                {
                    SearchForTarget();
                }
                break;
            case State.Running:
                RunTowardsTarget();
                break;
        }
    }

    private void SearchForTarget()
    {
        // Don't search for a new target while running
        if (currentState == State.Running)
            return; 

        Collider[] detectedColliders = Physics.OverlapSphere(transform.position, searchRadius);

        for (int i = 0; i < detectedColliders.Length; i++)
        {
            if (detectedColliders[i].TryGetComponent(out Runner runner))
            {
                if (runner.IsTarget())
                    continue;

                runner.SetTarget();
                targetRunner = runner.transform;
                StartRunningTowardTarget();

                Invoke(nameof(CheckForOverwhelm), 1.5f); // Check for overwhelm after a short delay
                return;
            }
        }
    }

    // This is a method to switch from idle state to running state
    private void StartRunningTowardTarget()
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

        // Use collider center instead of transform position for accurate distance
        Vector3 targetPosition = targetRunner.position;
        if (targetRunner.TryGetComponent(out Collider targetCollider))
        {
            targetPosition = targetCollider.bounds.center;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition,
            Time.deltaTime * moveSpeed);

        float distance = Vector3.Distance(transform.position, targetPosition);

        if (distance < 0.5f) // Increased threshold to account for collider sizes
        {
            onRunnerDied?.Invoke();
            Destroy(targetRunner.gameObject);
            Destroy(gameObject);
        }
    }
    // Add this to Enemy.cs and make sure Enemy has a Trigger Collider
    

    private void CheckForOverwhelm()
    {
        if (targetRunner != null)
        {
            Transform runnersParent = targetRunner.parent;
            int runnersCount = runnersParent.childCount;

            if (runnersCount > runnerThreshold)
            {
                Debug.Log($"Enemy overwhelmed! {runnersCount} runners > {runnerThreshold}");
                Destroy(gameObject);
            }
            else
            {
                Debug.Log($"Enemy continues chase. {runnersCount} runners <= {runnerThreshold}");
            }
        }
    }
}
