using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class ResultUI : MonoBehaviour
{
    [SerializeField] List<TextMeshProUGUI> _resultUI = new List<TextMeshProUGUI>();

    int _index;
    StringBuilder _sb = new StringBuilder();
    public void UpdateResultUI(IngredientInfo info)
    {
        _sb.Clear();
        _sb.Append($"{info.Data.EngName} X {info.Count}");
        _resultUI[_index].SetText(_sb);
        _index++;
    }
    public void ClearResultUI()
    {
        _index = 0;
        for (int i = 0; i < _resultUI.Count; i++) 
        {
            _sb.Clear();
            _resultUI[i].SetText(_sb);
        }
    }
}
