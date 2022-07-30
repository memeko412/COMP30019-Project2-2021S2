using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeControllerEnemy : MonoBehaviour
{
    public GameObject grenade; 
    public Transform throwPoint;
    public int maxGrenadeNumber = 3;
    int currentGrenadeNum;
    public float throwRate = 1f;
    private float throwCounting = 0f;
    LivingObject player;
    Transform playerTransform;

    void Start() {
        currentGrenadeNum = maxGrenadeNumber;
        player = FindObjectOfType<Player> ();
        playerTransform = player.transform;
    }

    void Update()
    {
        if (playerTransform!=null) {
            if (throwCounting<=0f&&currentGrenadeNum>0) {
                ThrowTheGrenade();
                currentGrenadeNum--;
                throwCounting = 1f/throwRate;
            }
            throwCounting-=Time.deltaTime;
        }
    }

    void ThrowTheGrenade() {
        Vector3 newFaceToPosition = playerTransform.position;
        Rigidbody grenadeObject = Instantiate(grenade.GetComponent<Rigidbody>(),throwPoint.position,throwPoint.rotation);
        grenadeObject.velocity = ComputeProjectionVelocity(newFaceToPosition,throwPoint.position,2f);

    }

    public void ThrowTheGrenade( Vector3 to)
    {
        Vector3 newFaceToPosition = to;
        Rigidbody grenadeObject = Instantiate(grenade.GetComponent<Rigidbody>(), throwPoint.position, throwPoint.rotation);
        grenadeObject.velocity = ComputeProjectionVelocity(newFaceToPosition, throwPoint.position, 2f);

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

    public int GetRemainGrenade() {
        return currentGrenadeNum;
    }
}
