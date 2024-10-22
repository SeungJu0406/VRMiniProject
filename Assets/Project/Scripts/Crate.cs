using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Crate : MonoBehaviour
{
    [SerializeField] XRInteractionManager manager;
    [SerializeField] XRGrabInteractable grabInteractablePrefab;

    private void Start()
    {
        manager = FindObjectOfType<XRInteractionManager>();
    }

    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        XRGrabInteractable instance = Instantiate(grabInteractablePrefab,transform.position, transform.rotation);
        manager.SelectEnter(args.interactorObject, instance);
    }

}
