using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Pan : MonoBehaviour
{
    Patty _choicePatty;

    public void OnSelectedEnter(SelectEnterEventArgs args)
    {
        Ingredient ingredient = args.interactableObject.transform.GetComponent<Ingredient>();
        if (ingredient as Patty)
        {
            _choicePatty = ingredient as Patty;
            _cookingRoutine = _cookingRoutine == null ? StartCoroutine(CookingRoutine()) : _cookingRoutine;
        }
    }

    public void OnSelectedExit()
    {
        if (_cookingRoutine != null)
        {
            StopCoroutine(_cookingRoutine);
            _cookingRoutine = null;
        }
    }

    Coroutine _cookingRoutine;
    IEnumerator CookingRoutine()
    {
        while (true)
        {
            _choicePatty.CurCookTime += Time.deltaTime;
            if (_choicePatty.CurCookTime >= _choicePatty.MaxCookTime)
            {
                if (_choicePatty.NextPatty == null) yield break;
                Instantiate(_choicePatty.NextPatty, _choicePatty.transform.position, _choicePatty.transform.rotation);
                Destroy(_choicePatty.gameObject);
                break;
            }
            yield return null;
        }
    }
}
