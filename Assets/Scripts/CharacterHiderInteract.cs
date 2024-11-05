using System.Collections;
using UnityEngine;

public class CharacterHiderInteract : MonoBehaviour
{
    bool canNowHide = false;
    [SerializeField] private Animator anim;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float moveDuration = 10f;
    [SerializeField] private Vector3 moveDirection = new Vector3(0, 0, -1); 
    [SerializeField] private Vector3 faceDirection = new Vector3(0, 180, 0);

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canNowHide = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (canNowHide && other.gameObject.CompareTag("Player"))
        {
            anim.SetBool("isWalking", true);
            StartCoroutine(MoveAndHide());
        }
    }

    IEnumerator MoveAndHide()
    {
        float elapsedTime = 0f;

        RotateTowardsDirection(); 

        while (elapsedTime < moveDuration)
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World); 
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        anim.SetBool("isWalking", false);
        this.gameObject.SetActive(false);
    }

    private void RotateTowardsDirection()
    {
        // Option 1: Using eulerAngles directly to set only Y rotation
        transform.eulerAngles = new Vector3(0, faceDirection.y, 0);

        // Option 2: Using LookRotation but limiting X rotation
        // This is an alternative if faceDirection is meant to be a direction vector.
        // Vector3 direction = new Vector3(faceDirection.x, 0, faceDirection.z).normalized;
        // if (direction != Vector3.zero)
        // {
        //     Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        //     transform.rotation = targetRotation;
        // }
    }
}
