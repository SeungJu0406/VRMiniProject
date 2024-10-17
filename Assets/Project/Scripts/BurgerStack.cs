using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BurgerStack : MonoBehaviour
{
    [SerializeField] BurgerStack _childStack;
    [SerializeField] Plate _plate;
    [SerializeField] Ingredient _ingredient;
    private void Awake()
    {
        _plate = GetComponentInParent<Plate>();
    }

    public void OnStackEnter(SelectEnterEventArgs args)
    {
        _ingredient = args.interactableObject.transform.GetComponent<Ingredient>();
        if (_ingredient == null) return;
        ProcessInStack();
    }
    Coroutine createStackRoutine;
    IEnumerator CreateStackRoutine(Ingredient ingredient)
    {
        yield return Manager.Delay.Get(0.2f);
        _childStack = Pool.Stack?.GetPool(ingredient.StackPivot);
        _childStack._plate = this._plate;
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
        _plate.AddStack(_ingredient);
        if (_ingredient.StackPivot != null)
        {
            createStackRoutine = createStackRoutine == null ? StartCoroutine(CreateStackRoutine(_ingredient)) : createStackRoutine;
        }
    }

    void ProcessOutStack()
    {
        _plate.RemoveStack(_ingredient);
        _ingredient = null;
        if (_childStack != null)
            Pool.Stack?.ReturnPool(_childStack);
    }
}
