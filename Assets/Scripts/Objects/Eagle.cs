using UnityEngine;

public class Eagle : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 2f;

    public int hitsToDestroy = 3;
    private int currentHits;

    public Rigidbody childRigidbody;

    private bool childDropped = false;

    private void Start()
    {
        if (pointA != null)
            transform.position = pointA.position;

        if (childRigidbody != null)
        {
            childRigidbody.isKinematic = true;
            childRigidbody.transform.SetParent(transform);
        }
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (pointB == null) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            pointB.position,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, pointB.position) < 0.05f)
        {
            Destroy(gameObject);
        }
    }

    public void OnHit(Vector3 hitPoint)
    {
        currentHits++;

        if (currentHits >= hitsToDestroy && !childDropped)
        {
            DropChild();
        }
    }

    private void DropChild()
    {
        childDropped = true;

        if (childRigidbody != null)
        {
            childRigidbody.transform.SetParent(null);
            childRigidbody.isKinematic = false;
        }
    }
}
