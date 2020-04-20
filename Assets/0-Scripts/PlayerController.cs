using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    // w a s d -> w:ileri, s:geri, a:sola yurume, d:saga yurume
    // mouse yatay kaydirma: saga sola bakma, mouse dikey kaydirma: yukari asagi bakma
    // f: secme, toplama, ya da kapi acma, ... gibi interactionlar

    public float moveSpeed = 1f;
    public float lookSpped = 1f;
    public GameObject carriedItem;
    public bool isCarryingItem;
    private bool isInputFrozen;
    private float moveSpeedMultiplier = 1;

    private void Update() {
        float ver = Input.GetAxis("Vertical");
        float hor = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.LeftShift)) {
            moveSpeedMultiplier = 2;
        } else {
            moveSpeedMultiplier = 1;
        }

        if (ver>0 || ver<0) {
            transform.position += transform.forward * ver * moveSpeed * moveSpeedMultiplier;
        }
        if (hor>0 || hor<0) {
            transform.position += transform.right * hor * moveSpeed * moveSpeedMultiplier;
        }
        

        float leftRight = Input.GetAxis("Mouse X");
        float upDown = Input.GetAxis("Mouse Y");

        if (leftRight>0 || leftRight<0) {
            transform.rotation *= Quaternion.Euler(0, leftRight * lookSpped, 0);  
        }
        if (upDown>0 || upDown<0) {
            Camera.main.transform.GetComponent<CameraFollow>().lookUpDownOffset += -upDown * lookSpped;
        }

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 3.5f, 1<<0, QueryTriggerInteraction.Ignore)) {
            Debug.Log(hit.collider.name + " " + isCarryingItem);
            if (hit.collider.GetComponent<WeightManager>()) {
                if (Input.GetKeyDown(KeyCode.F)) {
                    if (!isCarryingItem && !isInputFrozen) {
                        isInputFrozen = true;
                        StartCoroutine(FreezeInput());
                        CarryItem(hit.collider.gameObject);
                    }
                }
                
            }
        }

        if (isCarryingItem) {
            carriedItem.transform.position = transform.position + (transform.forward * 1.5f);
            if (!isInputFrozen && Input.GetKeyDown(KeyCode.F)) {
                isInputFrozen = true;
                StartCoroutine(FreezeInput());
                ReleaseItem();
            }
        }

    }

    public void CarryItem(GameObject aCarryItem) {
        aCarryItem.GetComponent<Rigidbody>().isKinematic = true;
        carriedItem = aCarryItem;
        isCarryingItem = true;
    }
    public void ReleaseItem() {
        isCarryingItem = false;
        carriedItem.GetComponent<Rigidbody>().isKinematic = false;
        carriedItem = null;
    }
    private IEnumerator FreezeInput() {
        yield return new WaitForSeconds(1);
        isInputFrozen = false;
    }

}
