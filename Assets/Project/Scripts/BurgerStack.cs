using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BurgerStack : MonoBehaviour
{
    [SerializeField] BurgerStack childStack;
    [SerializeField] Plate plate;
    [SerializeField] Ingredient ingredient;
    private void Awake()
    {
        plate = GetComponentInParent<Plate>();
    }

    public void OnStackEnter(SelectEnterEventArgs args)
    {
        ingredient = args.interactableObject.transform.GetComponent<Ingredient>();
        if (ingredient == null) return;
        ProcessInStack();
    }
    Coroutine createStackRoutine;
    IEnumerator CreateStackRoutine(Ingredient ingredient)
    {
        yield return Manager.Delay.Get(0.2f);
        childStack = Pool.Stack?.GetPool(ingredient.stackPivot);
        childStack.plate = this.plate;
        createStackRoutine = null;

    }

    public void OnStackExit()
    {
        if (createStackRoutine != null) 
        {
            StopCoroutine(createStackRoutine);
            createStackRoutine = null;
        }

        ProcessOutStack();
    }

    void ProcessInStack()
    {
        plate.AddStack(ingredient);
        if (ingredient.stackPivot != null)
        {
            createStackRoutine = createStackRoutine == null ? StartCoroutine(CreateStackRoutine(ingredient)) : createStackRoutine;
        }
    }

    void ProcessOutStack()
    {
        plate.RemoveStack(ingredient);
        ingredient = null;
        if (childStack != null)
            Pool.Stack?.ReturnPool(childStack);
    }
}
