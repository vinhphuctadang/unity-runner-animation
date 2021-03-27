using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

public class PlayerController : MonoBehaviour
{
    public static string actionUri = "http://192.168.0.107:5000/pose";
    Animator animator; 
    public string action = "";
    public IEnumerator getPoseRequest;

    [Serializable]
    public class PoseResponse
    {
        public string code;
        public string action;
    }


    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        getPoseRequest = GetPose();
        StartCoroutine(getPoseRequest);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision entered");
        animator.SetBool("Grounded", true);
    }
    // Update is called once per frame
    void Update()
    {
        
        if (action == "walking") {
            animator.SetFloat("MoveSpeed", 2.0f);
        }
        else if (action == "running"){
            animator.SetFloat("MoveSpeed", 6f);
        }
        else if (action == "idle" || action == ""){   
            animator.SetFloat("MoveSpeed", 0f);
        }
    }

    IEnumerator GetPose()
    {

        while (true) {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(actionUri))
            {
                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                switch (webRequest.result)
                {
                    case UnityWebRequest.Result.ConnectionError:
                    case UnityWebRequest.Result.DataProcessingError:
                    case UnityWebRequest.Result.ProtocolError:
                        Debug.LogError("Error in request:" + webRequest.error);
                        break;
                    case UnityWebRequest.Result.Success:
                        var text = webRequest.downloadHandler.text;
                        var response = JsonUtility.FromJson<PoseResponse>(text);
                        this.action = response.action;
                        Debug.Log("New action updated: " + action);
                        break;
                }

                yield return new WaitForSeconds(1f);
            }
        }
    }
}
