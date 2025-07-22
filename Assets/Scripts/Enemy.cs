using System;
using TMPro;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;
public class Enemy : MonoBehaviour
{
    enum State { Idle, Running }

    [Header("Settings")]
    [SerializeField] private float searchRadius;
    [SerializeField] private float moveSpeed;
    private State currentState;
    private Transform targetRunner;

    [Header("Events")]
    public static Action onRunnerDied; // Event to notify when a runner is killed by the enemy
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
        Collider[] detectedColliders = Physics.OverlapSphere(transform.position, searchRadius);

        for (int i = 0; i < detectedColliders.Length; i++)
        {
            if (detectedColliders[i].TryGetComponent(out Runner runner))
            {
                if (runner.IsTarget())
                    continue;

                runner.SetTarget();
                targetRunner = runner.transform;

                // Debug: Show what we're actually targeting
                Debug.Log($"Targeting runner at transform: {runner.transform.position}, collider center: {detectedColliders[i].bounds.center}");

                StartRunningTowardTarget();
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Runner runner))
        {
            Transform runnersParent = runner.transform.parent;
            int runnersCount = runnersParent.childCount;
            if (runnersCount > 3)
            {
                Destroy(gameObject);
                return;
            }

            if (currentState == State.Idle && !runner.IsTarget())
            {
                runner.SetTarget();
                targetRunner = runner.transform;
                StartRunningTowardTarget();
            }
        }


    }
}
