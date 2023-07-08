using UnityEngine;

public class NPCPatroling : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float moveSpeed = 3f;
    public float rotationSpeed = 5f;

    private int currentPoint = 0;
    private bool isMovingForward = true;

    private void Start()
    {
        ShufflePatrolPoints();
        SetDestination(patrolPoints[currentPoint]);
    }

    private void Update()
    {
        MoveTowardsDestination();
    }

    private void MoveTowardsDestination()
    {
        Vector3 targetDirection = patrolPoints[currentPoint].position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPoint].position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, patrolPoints[currentPoint].position) < 0.1f)
        {
            if (isMovingForward)
            {
                currentPoint++;
                if (currentPoint >= patrolPoints.Length)
                {
                    currentPoint = 0;
                }
            }
            else
            {
                currentPoint--;
                if (currentPoint < 0)
                {
                    currentPoint = patrolPoints.Length - 1;
                }
            }

            SetDestination(patrolPoints[currentPoint]);
        }
    }

    private void SetDestination(Transform target)
    {
        transform.LookAt(target);
    }

    private void ShufflePatrolPoints()
    {
        for (int i = 0; i < patrolPoints.Length - 1; i++)
        {
            int randomIndex = Random.Range(i, patrolPoints.Length);
            Transform temp = patrolPoints[i];
            patrolPoints[i] = patrolPoints[randomIndex];
            patrolPoints[randomIndex] = temp;
        }
    }
}
