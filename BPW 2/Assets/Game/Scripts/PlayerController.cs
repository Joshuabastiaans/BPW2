using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public List<Transform> m_InteractiblesInRange;
    private Rigidbody2D rigidbody2D;
    private Animator animator;
    private bool isWalking = false;
    public Transform m_IndicatorTransform;
    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 direction = new Vector2(horizontal, vertical);
        rigidbody2D.velocity = direction * speed;

        if (rigidbody2D.velocity.magnitude > 0.1f)
        {
            if (!isWalking)
            {
                AudioManager.Instance.Play("Walking");
                isWalking = true;
            }
        }
        else
        {
            if (isWalking)
            {
                AudioManager.Instance.Stop("Walking");
                isWalking = false;
            }
        }

        if (horizontal > 0.1f)
        {
            animator.SetBool("Right", true);
            animator.SetBool("Left", false);
        }
        else if (horizontal < -0.1f)
        {
            animator.SetBool("Left", true);
            animator.SetBool("Right", false);
        }
        else
        {
            animator.SetBool("Right", false);
            animator.SetBool("Left", false);
        }
        if (vertical > 0.1f)
        {
            animator.SetBool("Up", true);
            animator.SetBool("Down", false);
        }
        else if (vertical < -0.1f)
        {
            animator.SetBool("Down", true);
            animator.SetBool("Up", false);
        }
        else
        {
            animator.SetBool("Up", false);
            animator.SetBool("Down", false);
        }

        Interact();
    }

    public void Interact()
    {
        float maxDist = float.MaxValue;
        Transform closestTransform = null;
        Vector3 posToCheckFrom = transform.position;
        posToCheckFrom.y -= 0.1f;
        foreach (var col in m_InteractiblesInRange)
        {
            float dist = Vector2.Distance(col.transform.position, posToCheckFrom);
            if (dist < maxDist)
            {
                maxDist = dist;
                closestTransform = col;
            }
        }

        if (closestTransform != null)
        {
            m_IndicatorTransform.position = closestTransform.position;
            if (Input.GetKeyDown(KeyCode.F))
                closestTransform.GetComponent<IInteractible>().Interact();
            if (Input.GetKeyDown(KeyCode.JoystickButton0))
                closestTransform.GetComponent<IInteractible>().Interact();
        }
    }
}
