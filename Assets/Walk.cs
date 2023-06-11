using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private Animator animator;

    private Vector3 moveVelocity;
    public bool isShooting;

    public Transform[] Leg;
    public bool leftCan = true;
    public bool rightCan = false;
    public float delayTrace;

    public GameObject tracePrefab;

    private void Start()
    {
        StartCoroutine(TrackCoroutine());
    }

    void Update()
    {
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        if (horizontalInput != 0 || verticalInput != 0)
        {
            Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput);
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, Time.deltaTime * 10f);

            if (!isShooting)
            {
                animator.Play("anim_walk");
            }
            moveVelocity = new Vector3(horizontalInput, 0f, verticalInput).normalized * speed;
        }
        else
        {
            if (!isShooting)
            {
                animator.Play("anim_idle");
            }
            moveVelocity = Vector3.zero;
        }

        // Calculate the move velocity based on the joystick input

    }

    void FixedUpdate()
    {
        // Move the object using Rigidbody's MovePosition method
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    public void StartShootingAnimation()
    {
        isShooting = true;
        animator.Play("anim_Shooting");
    }

    IEnumerator TrackCoroutine()
    {
        while (true)
        {
            if (moveVelocity.magnitude > 0.01f)
            {
                if (leftCan)
                {
                    SpawnTrace(0);
                    leftCan = false;
                    rightCan = true;
                }
                else if (rightCan)
                {
                    SpawnTrace(1);
                    rightCan = false;
                    leftCan = true;
                }
            }

            yield return new WaitForSeconds(delayTrace);
        }
    }

    public void SpawnTrace(int which)
    {
        GameObject trace = Instantiate(tracePrefab, Leg[which].position, Quaternion.identity);
    }
}