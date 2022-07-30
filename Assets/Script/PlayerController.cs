using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

 	private Rigidbody playerRigidbody;
    public float playerSpeed;
    private Animator animator;


    private void Awake()
    {
        playerRigidbody = this.GetComponent<Rigidbody>();
		animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
        Turning();
    }
    public void Move()
    {
        Vector3 movementVec = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 playerVelocity = movementVec.normalized * playerSpeed;
        playerRigidbody.MovePosition(playerRigidbody.position + playerVelocity * Time.fixedDeltaTime);
		animator.SetFloat("speed",Mathf.Sqrt(playerVelocity.x * playerVelocity.x + playerVelocity.z * playerVelocity.z));
    }

    void Turning()
    {
        Vector2 mouseScreenPos = Input.mousePosition;
        float distanceFromCameraToXZPlane = Camera.main.transform.position.y;
        Vector3 screenPosWithZDistance = (Vector3)mouseScreenPos + (Vector3.forward * distanceFromCameraToXZPlane);
        Vector3 faceToPosition = Camera.main.ScreenToWorldPoint(screenPosWithZDistance);
        Vector3 newFaceToPosition = new Vector3 (faceToPosition.x, transform.position.y, faceToPosition.z);
		transform.LookAt (newFaceToPosition);
    }
}