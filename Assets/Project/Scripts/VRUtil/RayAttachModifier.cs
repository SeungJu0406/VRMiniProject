using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RayAttachModifier : MonoBehaviour
{
    IXRSelectInteractable selectInteractable;

    protected void OnEnable()
    {
        selectInteractable = GetComponent<IXRSelectInteractable>();
        if (selectInteractable as Object == null)
        {
            Debug.Log("¾ÈµÊ");
            return;
        }
        selectInteractable.selectEntered.AddListener(OnSelectEntered);
    }
    protected void OnDisable()
    {
        if (selectInteractable != null)
        {
            selectInteractable.selectEntered.RemoveListener(OnSelectEntered);
        }
    }

    private void OnSelectEntered(SelectEnterEventArgs arg0)
    {
        if (arg0.interactorObject is XRRayInteractor == false) return;

        Transform atttachTransform = arg0.interactorObject.GetAttachTransform(selectInteractable);
        Pose originAttachTransform = arg0.interactorObject.GetLocalAttachPoseOnSelect(selectInteractable);
        atttachTransform.SetLocalPose(originAttachTransform);
    }

}
