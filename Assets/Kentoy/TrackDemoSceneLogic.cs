using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class TrackDemoSceneLogic : MonoBehaviour
{
    public GameObject prefabToInstantiate; // Ҫ������Ԥ����

    public List<GameObject> listTargets = new List<GameObject>();

    public Transform cameraTransform;
    public float cameraMoveSpeed;

    void FixedUpdate()
    {
        // ����û��Ƿ�����������
        if (Input.GetMouseButtonDown(0)) // 0 ����������
        {
            //Debug.LogError("Input.GetMouseButtonUp " + Time.time);

            // �������raycast��ȡ���λ��
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // ��������Ͷ�䣬����Ƿ����������
            if (Physics.Raycast(ray, out hit))
            {
                if(hit.collider.gameObject.CompareTag("Ground"))
                {
                    // �ڵ��λ�ô���Ԥ�����ʵ��
                    GameObject curTarget = Instantiate(prefabToInstantiate, hit.point, Quaternion.identity);

                    listTargets.Add(curTarget);
                }
            }
        }


        if(Input.GetKey(KeyCode.A))
        {
            cameraTransform.Translate(new Vector3(-cameraMoveSpeed * Time.fixedDeltaTime, 0, 0), Space.World);
        }
        if (Input.GetKey(KeyCode.D))
        {
            cameraTransform.Translate(new Vector3(cameraMoveSpeed * Time.fixedDeltaTime, 0, 0), Space.World);
        }
        if (Input.GetKey(KeyCode.S))
        {
            cameraTransform.Translate(new Vector3(0, 0, -cameraMoveSpeed * Time.fixedDeltaTime), Space.World);
        }
        if (Input.GetKey(KeyCode.W))
        {
            cameraTransform.Translate(new Vector3(0, 0, cameraMoveSpeed * Time.fixedDeltaTime), Space.World);
        }
    }
}
