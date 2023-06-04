using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseChick : MonoBehaviour
{
    // component references
    [SerializeField] protected Animator animator;

    // movement variables
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float runSpeed = 2f;
    internal float CurrentSpeed { get { return running ? runSpeed : moveSpeed; } }
    protected bool running = false;
    private Vector3 wantedDirection;
    private Vector3 currentDirection;

    /// <summary>
    /// Initializes the chick, makes it ready for updates.
    /// </summary>
    internal virtual void Initialize()
    {
        // no initial velocities
        wantedDirection = currentDirection = Vector3.zero;
    }

    /// <summary>
    /// Called every frame.
    /// </summary>
    protected abstract void Update();

    /// <summary>
    /// Call to make the chick move in given direction.
    /// </summary>
    /// <param name="direction">Direction to move in</param>
    internal virtual void Move(Vector3 direction)
    {
        // stop moving if no direction is provided
        if (direction == Vector3.zero)
        {
            animator.SetBool("Run", false);
            animator.SetBool("Walk", false);
            return;
        }

        // make sure the direction is normalized
        direction.Normalize();

        // determine wanted velocity
        wantedDirection = direction * Time.deltaTime;

        // smooth the wanted velocity to get the velocity we'll actually apply
        Vector3 smoothVelocity = Vector3.zero;
        currentDirection = Vector3.SmoothDamp(currentDirection, wantedDirection, ref smoothVelocity, 0.05f);

        // move in the direction of the current velocity
        transform.position += currentDirection * CurrentSpeed;

        // look in that direction as well
        transform.LookAt(transform.position + currentDirection);

        // set the animator variables
        animator.SetBool("Walk", !running);
        animator.SetBool("Run", running);
    }

    /// <summary>
    /// Call to turn the eat animation on/off.
    /// </summary>
    /// <param name="on">Whether to turn the animation on or off</param>
    internal void Eat(bool on)
    {
        animator.SetBool("Eat", on);
    }
}
