using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Filtering;

public class Ingredient : MonoBehaviour
{
    [SerializeField] public XRGrabInteractable grabInteractable;
    [SerializeField] public IngredientData data;
    [SerializeField] public Transform stackPivot;
    [Header("����")]
    [SerializeField] public Ingredient parent;
    [SerializeField] public Ingredient child;

    InteractionLayerMask ingredientLayerMask;
    InteractionLayerMask ignoreLayerMask;
    private void Awake()
    {
        ingredientLayerMask = InteractionLayerMask.GetMask("Ingredient");
        ignoreLayerMask = InteractionLayerMask.GetMask("Ignore Interactor");

        grabInteractable = GetComponent<XRGrabInteractable>();  
    }

    public void SubscribePlateEvent(Plate plate)
    {
        plate.OnChangeTop += CheckActiveGrabInteratable;
    }

    public void UnSubscribePlateEvent(Plate plate)
    {
        plate.OnChangeTop -= CheckActiveGrabInteratable;
    }

    public void CheckActiveGrabInteratable(Plate plate)
    {
        if (plate.TopIngredient == this)
        {
            // ���ͷ��ͺ� Ȱ��ȭ
            grabInteractable.interactionLayers = ingredientLayerMask;
        }
        else
        {
            // ���ͷ��ͺ� ��Ȱ��ȭ
            grabInteractable.interactionLayers = ignoreLayerMask;
        }
    }
}
