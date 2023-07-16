using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentPlayerTransform : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Find the active GameObject with the "Player" tag
        player = GameObject.FindGameObjectWithTag("Player");
        Transform playerCameraRoot = player.transform.Find("PlayerCameraRoot");

        if (player != null)
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
