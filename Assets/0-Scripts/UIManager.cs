using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public Text grabWarningText;

    public void ShowGrabWarningText(string aText) {
        grabWarningText.text = aText;
        if (!grabWarningText.transform.parent.gameObject.activeSelf) {
            grabWarningText.transform.parent.gameObject.SetActive(true);
        }
    }

    public void HideGrabWarningText() {
        if (grabWarningText.transform.parent.gameObject.activeSelf) {
            grabWarningText.transform.parent.gameObject.SetActive(false);
        }
        grabWarningText.text = "";
    }
}
