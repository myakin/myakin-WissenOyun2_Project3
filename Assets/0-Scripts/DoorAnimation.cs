using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimation : MonoBehaviour {
    public float openAngle = -10;
    public float duration = 1;
    private float targetRotationY;
    private bool isAnimating;
    private float timer = 0;

    public void OpenDoor(float anOpenAngle) {
        openAngle = anOpenAngle;
        targetRotationY = openAngle;
        timer = 0;
        if (!isAnimating) {
            isAnimating = true;
            StartCoroutine(AnimateDoor());
        }
    }

    public void CloseDoor() {
        targetRotationY = 0;
        timer = 0;
        if (!isAnimating) {
            isAnimating = true;
            StartCoroutine(AnimateDoor());
        }
    }

    private IEnumerator AnimateDoor() {
        while (timer<=duration) {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, targetRotationY, 0), timer/duration);
            timer+=Time.deltaTime;
            yield return null;
        }
        transform.rotation = Quaternion.Euler(0, targetRotationY, 0);
        isAnimating = false;
        
    }
    
}
