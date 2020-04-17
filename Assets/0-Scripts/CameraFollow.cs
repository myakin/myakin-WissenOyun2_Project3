using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public GameObject player;
    public float followDistance=0.5f;
    public float offsetUp=1f;
    public float lookUpDownOffset;

    void Update() {
        transform.position = player.transform.position + (-player.transform.forward * followDistance) + (player.transform.up * offsetUp);
        transform.rotation = player.transform.rotation * Quaternion.Euler(lookUpDownOffset, 0, 0);
    }

    
}
