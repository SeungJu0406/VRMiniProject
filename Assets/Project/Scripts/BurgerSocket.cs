using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BurgerSocket : MonoBehaviour
{
    [SerializeField] BurgerSocket _childSocket;
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
        if (_childSocket != null)
        {
            _childSocket._childSocket = null;
            if (Pool.Socket != null)
            {
                Pool.Socket.ReturnPool(_childSocket);
            }
        }
    }

    Coroutine createStackRoutine;
    IEnumerator CreateStackRoutine(Ingredient ingredient)
    {
        yield return Manager.Delay.GetDelay(0.2f);
        _childSocket = Pool.Socket.GetPool(ingredient.StackPivot);
        _childSocket._plate = this._plate;
        createStackRoutine = null;
    }
}
