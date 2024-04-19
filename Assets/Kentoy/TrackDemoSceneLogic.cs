using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class TrackDemoSceneLogic : MonoBehaviour
{
    public GameObject prefabToInstantiate; // 要创建的预制体

    public List<GameObject> listTargets = new List<GameObject>();

    public Transform cameraTransform;
    public float cameraMoveSpeed;

    void FixedUpdate()
    {
        // 检查用户是否点击了鼠标左键
        if (Input.GetMouseButtonDown(0)) // 0 代表鼠标左键
        {
            //Debug.LogError("Input.GetMouseButtonUp " + Time.time);

            // 从摄像机raycast获取点击位置
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // 进行射线投射，检查是否击中了物体
            if (Physics.Raycast(ray, out hit))
            {
                if(hit.collider.gameObject.CompareTag("Ground"))
                {
                    // 在点击位置创建预制体的实例
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
