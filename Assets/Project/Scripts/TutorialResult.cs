using UnityEngine;

public class TutorialResult : Result
{
    [SerializeField] HingeJoint _doorJoint;
    [SerializeField] BoxCollider _handleCollider;

    JointLimits _openDoorLimit;
    JointLimits _closeDoorLimit;

    protected override void Start()
    {
        base.Start();
        _openDoorLimit = _doorJoint.limits;
        _closeDoorLimit = _doorJoint.limits;
        _closeDoorLimit.min = 0;
        _closeDoorLimit.max = 0;
        _doorJoint.limits = _closeDoorLimit;
        _handleCollider.enabled = false; 
    }

    protected override void InitRecipe()
    {
        _resultList.Clear();
        _resultUI.ClearResultText();
        for (int i = 0; i < _recipeData.RecipeList.Count; i++)
        {
            _resultList.Add(_recipeData.RecipeList[i]);
        }

        _resultList.Sort((s1, s2) => s1.Data.ID.CompareTo(s2.Data.ID));
        for (int i = 1; i < _resultList.Count - 1; i++)
        {
            IngredientInfo temp = new IngredientInfo();
            temp.Data = _resultList[i].Data;
            temp.Count = 1;
            _resultList[i] = temp;
            _resultUI.UpdateResultText(_resultList[i]);
        }
    }

    protected override void ProcessSucess()
    {
        base.ProcessSucess();
        // 성공시 문 열림
        OpenDoor();
    }

    void OpenDoor()
    {
        _doorJoint.limits = _openDoorLimit;
        _handleCollider.enabled = true ;
    }
}
