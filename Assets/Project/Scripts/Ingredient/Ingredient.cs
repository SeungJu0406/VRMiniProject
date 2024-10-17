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
    [Header("스택")]
    [SerializeField] public Ingredient parent;
    [SerializeField] public Ingredient child;

    int ingredientLayer;
    int ignoreLayer;
    int socketLayer;

    InteractionLayerMask ingredientLayerMask;
    InteractionLayerMask ignoreLayerMask;
    private void Awake()
    {
        InitLayer();

        grabInteractable = GetComponent<XRGrabInteractable>();  
    }
    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactorObject.transform.gameObject.layer != socketLayer)
        {
            gameObject.layer = ignoreLayer;
        }
    }
    public void OnSelectExited(SelectExitEventArgs args)
    {
        if (args.interactorObject.transform.gameObject.layer != socketLayer)
        {
            gameObject.layer = ingredientLayer;
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
            // 인터렉터블 활성화
            grabInteractable.interactionLayers = ingredientLayerMask;
        }
        else
        {
            // 인터렉터블 비활성화
            grabInteractable.interactionLayers = ignoreLayerMask;
        }
    }

    void InitLayer()
    {
        ingredientLayer = LayerMask.NameToLayer("Ingredient");
        ignoreLayer = LayerMask.NameToLayer("Ignore Collision");
        socketLayer = LayerMask.NameToLayer("Socket");

        ingredientLayerMask = InteractionLayerMask.GetMask("Ingredient");
        ignoreLayerMask = InteractionLayerMask.GetMask("Ignore Interactor");
    }

}
