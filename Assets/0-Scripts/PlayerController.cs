using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    // w a s d -> w:ileri, s:geri, a:sola yurume, d:saga yurume
    // mouse yatay kaydirma: saga sola bakma, mouse dikey kaydirma: yukari asagi bakma
    // f: secme, toplama, ya da kapi acma, ... gibi interactionlar

    public float moveSpeed = 1f;
    public float lookSpped = 1f;

    private void Update() {
        float ver = Input.GetAxis("Vertical");
        float hor = Input.GetAxis("Horizontal");

        if (ver>0 || ver<0) {
            transform.position += transform.forward * ver * moveSpeed;
        }
        if (hor>0 || hor<0) {
            transform.position += transform.right * hor * moveSpeed;
        }
        

        float leftRight = Input.GetAxis("Mouse X");
        float upDown = Input.GetAxis("Mouse Y");

        if (leftRight>0 || leftRight<0) {
            transform.rotation *= Quaternion.Euler(0, leftRight * lookSpped, 0);  
        }
        if (upDown>0 || upDown<0) {
            Camera.main.transform.GetComponent<CameraFollow>().lookUpDownOffset += -upDown * lookSpped;
        }

    }

}
