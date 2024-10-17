using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BurgerStack : MonoBehaviour
{
    [SerializeField] BurgerStack childStack;
    public void OnStackEnter(SelectEnterEventArgs args)
    {
        Ingredient ingredient = args.interactableObject.transform.GetComponent<Ingredient>();
        if (ingredient == null) return;
        if (ingredient.stackPivot != null)
        {
            createStackRoutine = createStackRoutine == null ? StartCoroutine(CreateStackRoutine(ingredient)) : createStackRoutine;
        }
    }
    Coroutine createStackRoutine;
    IEnumerator CreateStackRoutine(Ingredient ingredient)
    {
        yield return Manager.Delay.Get(0.2f);
        childStack = Pool.Stack.GetPool(ingredient.stackPivot);
        createStackRoutine = null;

    }

    public void OnStackExit()
    {
        if (createStackRoutine != null) 
        {
            StopCoroutine(createStackRoutine);
            createStackRoutine = null;
        }
        
        if(childStack != null)
            Pool.Stack.ReturnPool(childStack);
    }
}
