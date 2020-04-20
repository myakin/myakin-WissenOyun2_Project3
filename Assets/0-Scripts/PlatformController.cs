using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {
    public Transform door;
    public float weightToOpenDoor = 100;
    private float currentWeightOnPlatform;

    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<WeightManager>()) {
            AddToTotalWeightOnPlatform(other.GetComponent<WeightManager>().weight);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.GetComponent<WeightManager>()) {
            RemoveFromTotalWeightOnPlatform(other.GetComponent<WeightManager>().weight);
        }
    }

    private void AddToTotalWeightOnPlatform(float anIncomingWeight) {
        currentWeightOnPlatform+=anIncomingWeight;
        EvaluateDoorOpenFunction();
    }
    private void RemoveFromTotalWeightOnPlatform(float anIncomingWeight) {
        currentWeightOnPlatform-=anIncomingWeight;
        EvaluateDoorOpenFunction();
    }

    private void EvaluateDoorOpenFunction() {
        if (currentWeightOnPlatform>=weightToOpenDoor) {
            float openAngle = currentWeightOnPlatform - weightToOpenDoor > 0 ? -(currentWeightOnPlatform - weightToOpenDoor) : 0;
            door.GetComponent<DoorAnimation>().OpenDoor(openAngle);
        } else {
            door.GetComponent<DoorAnimation>().CloseDoor();
        }
    }
}
