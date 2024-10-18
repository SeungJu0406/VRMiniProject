using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class ResultUI : MonoBehaviour
{
    [SerializeField] List<TextMeshProUGUI> _resultUI = new List<TextMeshProUGUI>();

    [SerializeField] TextMeshProUGUI _successUI;

    int _index;
    StringBuilder _sb = new StringBuilder();

    private void Awake()
    {
        _successUI.gameObject.SetActive(false);
    }

    public void UpdateResultText(IngredientInfo info)
    {
        _sb.Clear();
        _sb.Append($"{info.Data.EngName} X {info.Count}");
        _resultUI[_index].SetText(_sb);
        _index++;
    }
    public void ClearResultText()
    {
        _index = 0;
        for (int i = 0; i < _resultUI.Count; i++) 
        {
            _sb.Clear();
            _resultUI[i].SetText(_sb);
        }
    }
    public void UpdateSuccessText()
    {
        _sb.Clear();
        _sb.Append("Success");
        _successUI.SetText(_sb);
        StartCoroutine(SuccessTextRoutine());
    }
    public void UpdateFailText()
    {
        _sb.Clear();
        _sb.Append("Fail");
        _successUI.SetText(_sb);
        StartCoroutine(SuccessTextRoutine());
    }
    IEnumerator SuccessTextRoutine()
    {
        _successUI.gameObject.SetActive(true);
        yield return Manager.Delay.GetDelay(5f);
        _successUI.gameObject.SetActive(false);

    }
}
