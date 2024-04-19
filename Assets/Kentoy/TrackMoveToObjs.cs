using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class TrackMoveToObjs : MonoBehaviour
{
    public TrackDemoSceneLogic trackDemoSceneLogic;

    public NavMeshAgent navMeshAgent;

    public GameObject myTarget;

    public Transform containerTransform;
    public Transform batteryTransform;

    public TextMesh textMesh;

    public Text textInfo;

    Transform selfTransform;

    public int itemCount;
    int maxItemCount = 5;

    float battery = 1f;

    private void Awake()
    {
        selfTransform = transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateText();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        AutoBackToContainer();

        AutoBackToBattery();

        if (myTarget == null)
        {
            if(trackDemoSceneLogic.listTargets.Count > 0)
            {
                float minDis = 99999999999f;
                GameObject curTarget = null;

                for(int i=0;i< trackDemoSceneLogic.listTargets.Count;i++)
                {
                    GameObject curDetectingTarget = trackDemoSceneLogic.listTargets[i];

                    float curDis = Vector3.Distance(selfTransform.position, curDetectingTarget.transform.position);

                    if (curDis < minDis)
                    {
                        minDis = curDis;

                        curTarget = curDetectingTarget;
                    }
                }

                myTarget = curTarget;
            }
        }

        if(myTarget)
        {
            navMeshAgent.SetDestination(myTarget.transform.position);
        }


        if(navMeshAgent.pathPending)
        {
            textInfo.text = "PathPending";
        }
        else
        {
            if (myTarget)
            {
                textInfo.text = "Distance to target: " + navMeshAgent.remainingDistance + " m";
            }
            else
            {
                textInfo.text = "Standby";
            }
        }

        textInfo.text += ", Battery: " + battery * 100 + "%";

        textInfo.text += ", Position: " + selfTransform.position;

        textInfo.text += ", Date: " + System.DateTime.Now;

        //------------------------------------------------
        if (myTarget && !navMeshAgent.pathPending)
        {
            battery -= 0.02f * Time.fixedDeltaTime;
        }
        else
        {
            battery -= 0.0001f * Time.fixedDeltaTime;
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        Debug.Log("Student Enter:" + coll.gameObject.name);

        if(coll.gameObject.CompareTag("Target"))
        {
            trackDemoSceneLogic.listTargets.Remove(coll.gameObject);

            Destroy(coll.gameObject);

            itemCount++;

            UpdateText();
        }
        else if (coll.gameObject.CompareTag("Container"))
        {
            myTarget = null;

            itemCount = 0;

            UpdateText();
        }
        else if (coll.gameObject.CompareTag("Battery"))
        {
            myTarget = null;

            battery = 1;

            UpdateText();
        }
    }

    void UpdateText ()
    {
        textMesh.text = itemCount + "-" + maxItemCount;
    }

    void AutoBackToContainer ()
    {
        if(itemCount >= maxItemCount)
        {
            myTarget = containerTransform.gameObject;
        }
    }

    void AutoBackToBattery()
    {
        if (battery <= 0.6f)
        {
            myTarget = batteryTransform.gameObject;
        }
    }
}
