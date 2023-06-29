using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentPlayerTransform : MonoBehaviour
{
    private GameObject parentObject;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Find the active GameObject with the "Player" tag
        parentObject = GameObject.FindGameObjectWithTag("Player");
        Transform playerCameraRoot = parentObject.transform.Find("PlayerCameraRoot");

        if (parentObject != null)
        {
            this.transform.position = playerCameraRoot.position;
            this.transform.rotation = playerCameraRoot.rotation;
        }
        else
        {
            // No GameObject with the "Player" tag was found
            // Handle the case when the parentObject is null
        }
    }
}
