using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Filtering;

public class Ingredient : MonoBehaviour
{
    [SerializeField] public IngredientData Data;
    [SerializeField] public Transform StackPivot;
    [SerializeField] public XRGrabInteractable GrabInteractable;
    [Header("����")]
    [SerializeField] public Ingredient Parent;
    [SerializeField] public Ingredient Child;

    int _ingredientLayer;
    int _ignoreLayer;
    int _socketLayer;

    InteractionLayerMask ingredientLayerMask;
    InteractionLayerMask ignoreLayerMask;
    private void Awake()
    {
        InitLayer();

        GrabInteractable = GetComponent<XRGrabInteractable>();  
    }
    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactorObject.transform.gameObject.layer != _socketLayer)
        {
            gameObject.layer = _ignoreLayer;
        }
    }
    public void OnSelectExited(SelectExitEventArgs args)
    {
        if (args.interactorObject.transform.gameObject.layer != _socketLayer)
        {
            gameObject.layer = _ingredientLayer;
        }
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
            GrabInteractable.interactionLayers = ingredientLayerMask;
        }
        else
        {
            // ���ͷ��ͺ� ��Ȱ��ȭ
            GrabInteractable.interactionLayers = ignoreLayerMask;
        }
    }

    void InitLayer()
    {
        _ingredientLayer = LayerMask.NameToLayer("Ingredient");
        _ignoreLayer = LayerMask.NameToLayer("Ignore Collision");
        _socketLayer = LayerMask.NameToLayer("Socket");

        ingredientLayerMask = InteractionLayerMask.GetMask("Ingredient");
        ignoreLayerMask = InteractionLayerMask.GetMask("Ignore Interactor");
    }

}
