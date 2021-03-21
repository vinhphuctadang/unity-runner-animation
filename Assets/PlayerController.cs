using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision entered");
        animator.SetBool("Grounded", true);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("up"))
        {
            Debug.Log("Walking pressed");
            animator.SetFloat("MoveSpeed", 2.0f);
        }
        else if (Input.GetKeyUp("up")){
            Debug.Log("Walking released");
            animator.SetFloat("MoveSpeed", 0.0f);
        }
        else if (Input.GetKeyDown("w")){
            Debug.Log("Running pressed");
            animator.SetFloat("MoveSpeed", 6f);
        }
        else if (Input.GetKeyUp("w")){
            Debug.Log("Running released");
            animator.SetFloat("MoveSpeed", 0f);
        }
    }
}
