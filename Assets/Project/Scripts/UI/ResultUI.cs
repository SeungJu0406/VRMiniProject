using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class ResultUI : BaseUI
{
    [SerializeField] List<TextMeshProUGUI> _resultUI = new List<TextMeshProUGUI>();

    [SerializeField] TextMeshProUGUI _successText;

    int _index;
    StringBuilder _sb = new StringBuilder();

     protected override void Awake()
    {
        base.Awake();
        InitOrderUIList();
        _successText = GetUI<TextMeshProUGUI>("Success");
        _successText.gameObject.SetActive(false);
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
        _successText.SetText(_sb);
        StartCoroutine(SuccessTextRoutine());
    }
    public void UpdateFailText()
    {
        _sb.Clear();
        _sb.Append("Fail");
        _successText.SetText(_sb);
        StartCoroutine(SuccessTextRoutine());
    }
    IEnumerator SuccessTextRoutine()
    {
        _successText.gameObject.SetActive(true);
        yield return Manager.Delay.GetDelay(5f);
        _successText.gameObject.SetActive(false);
    }


    void InitOrderUIList()
    {
        _resultUI.Add(GetUI<TextMeshProUGUI>("Order1"));
        _resultUI.Add(GetUI<TextMeshProUGUI>("Order2"));
        _resultUI.Add(GetUI<TextMeshProUGUI>("Order3"));
        _resultUI.Add(GetUI<TextMeshProUGUI>("Order4"));
        _resultUI.Add(GetUI<TextMeshProUGUI>("Order5"));
    }
}
