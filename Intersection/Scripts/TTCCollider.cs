using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class TTCCollider : MonoBehaviour
{
    public int nCollider = 20;
    List<GameObject> childObjects = new List<GameObject>();
    List<SphereCollider> colliders = new List<SphereCollider>();
    GameObject parent;
    PathCreator path;

    void Start()
    {
        parent = this.gameObject;
        path = GetComponent<PathFollower>().pathCreator;

        for (int i=0; i<nCollider; i++)
        {
            childObjects.Add(new GameObject("Collider_" + i.ToString()));
            childObjects[i].transform.SetParent(this.transform);
            childObjects[i].transform.localScale = new Vector3(1f, 1f, 1f);

            colliders.Add(childObjects[i].AddComponent<SphereCollider>());
            colliders[i].isTrigger = true;
            colliders[i].radius = 0.19f;

            // 충돌 처리 스크립트 추가
            childObjects[i].AddComponent<TTCColliderTrigger>();
        }
        
    }

    float speed = 1f;
    float distanceTravelled = 0f;
    float distanceOffset = 4.07f/2f;
    public float dT = 0.2f;

    public bool collisionExpected = false;
    public Vector3 collisionExpectedPoint = new Vector3(0,0,0);
    public int collisionExpectedID = -1;
    public string collisionExpectedTag = " ";
    public float collisionLeftStation = -1;
    public GameObject collisionExpectedObject;

    void FixedUpdate()
    {
        distanceTravelled = parent.GetComponent<PathFollower>().distanceTravelled;
        speed = parent.GetComponent<PathFollower>().speed;
        
        if (speed < 0.1f)
        {
            speed = 0.1f;
        }
        
        for (int i = 0; i < nCollider; i++)
        {
            float targetDistanceTravelled = dT * speed * i + distanceTravelled + distanceOffset;
            Vector3 targetPoint = path.path.GetPointAtDistance(targetDistanceTravelled);
            Quaternion targetRotation = path.path.GetRotationAtDistance(targetDistanceTravelled);
            childObjects[i].transform.position = targetPoint;
            
            if (targetDistanceTravelled >= path.path.length)
            {
                // (child object list에서도 제거하게끔 수정하기)
                Destroy(childObjects[i]);
                nCollider = nCollider - 1;
            }
        }

        // 충돌 위치 체크
        int targetID = -1;
        for (int i = 0; i < nCollider; i++)
        {
            targetID = childObjects[i].GetComponent<TTCColliderTrigger>().targetID;
            if (targetID != -1)
            {
                collisionExpected = true;
                collisionExpectedID = targetID;
                collisionExpectedPoint = childObjects[i].transform.position;
                collisionExpectedTag = childObjects[i].GetComponent<TTCColliderTrigger>().targetTag;
                collisionLeftStation = path.path.GetClosestDistanceAlongPath(collisionExpectedPoint) - distanceTravelled;
                collisionExpectedObject = childObjects[i].GetComponent<TTCColliderTrigger>().targetGameObject;
                break;
            }
            else
            {
                collisionExpected = false;
                collisionExpectedID = -1;
                collisionExpectedPoint = new Vector3(0, 0, 0);
                collisionExpectedTag = " ";
                collisionExpectedObject = null;

            }
        }

    }
}
