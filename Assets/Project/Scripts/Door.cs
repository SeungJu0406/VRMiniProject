using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Door : MonoBehaviour
{
    [SerializeField] TransformJoint _doorPuller;

    private void Awake()
    {
        _doorPuller.enabled = false;
    }

    public void BeginDoorPulling(SelectEnterEventArgs args)
    {
        _doorPuller.connectedBody = args.interactorObject.GetAttachTransform(args.interactableObject);
        _doorPuller.enabled = true;
    }
    public void EndDoorPulling() 
    {
        _doorPuller.connectedBody = null;
        _doorPuller.enabled = false;
    }
}
