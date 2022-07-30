using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeController : MonoBehaviour
{
    public GameObject grenade; 
    public Transform throwPoint;
    
    public int maxGrenadeNumber = 3;
    int currentGrenadeNum;
    public BombContainer bombContainer;

    void Start() {
        currentGrenadeNum = maxGrenadeNumber;
        if(bombContainer!=null)
            bombContainer.SetMaxBomb(maxGrenadeNumber);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && currentGrenadeNum>0) {
            ThrowTheGrenade();
            FindObjectOfType<AudioManager>().Play("throw");
            currentGrenadeNum--;
        }
        if (bombContainer != null)
            bombContainer.SetCurrBomb(currentGrenadeNum);
        
    }

    void ThrowTheGrenade() {
        Vector2 mouseScreenPos = Input.mousePosition;
        float distanceFromCameraToXZPlane = Camera.main.transform.position.y;
        Vector3 screenPosWithZDistance = (Vector3)mouseScreenPos + (Vector3.forward * distanceFromCameraToXZPlane);
        Vector3 faceToPosition = Camera.main.ScreenToWorldPoint(screenPosWithZDistance);
        Vector3 newFaceToPosition = new Vector3 (faceToPosition.x, transform.position.y, faceToPosition.z);
        Rigidbody grenadeObject = Instantiate(grenade.GetComponent<Rigidbody>(),throwPoint.position,throwPoint.rotation);
        grenadeObject.velocity = ComputeProjectionVelocity(newFaceToPosition,throwPoint.position,1f);

    }

    Vector3 ComputeProjectionVelocity(Vector3 fallPoint, Vector3 throwPoint, float time) {
        Vector3 distanceBetweenTwoPoints = fallPoint - throwPoint;
        Vector3 distanceBetweenTwoPointsXZ = distanceBetweenTwoPoints;
        distanceBetweenTwoPointsXZ.y = 0f;
        float yComponent = distanceBetweenTwoPoints.y;
        float xzComponent = distanceBetweenTwoPointsXZ.magnitude;
        float velocityX = xzComponent / time;
        float velocityY = yComponent / time + 0.5f*Mathf.Abs(Physics.gravity.y)*time;
        Vector3 normalizedHorizontalDis = distanceBetweenTwoPointsXZ.normalized;
        normalizedHorizontalDis *= velocityX;
        normalizedHorizontalDis.y = velocityY;

        return normalizedHorizontalDis;
    
    }

    public void AddGrenade(int amount) {
        if(amount < 0) amount = 0;
        currentGrenadeNum += amount;
        if(currentGrenadeNum > maxGrenadeNumber) {
            currentGrenadeNum = maxGrenadeNumber;
        }
    }

    public int GetRemainGrenade() {
        return currentGrenadeNum;
    }
}
