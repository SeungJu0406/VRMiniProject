using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class DoorUI : BaseUI
{
    TextMeshProUGUI _openText;

    private void Start()
    {
        _openText = GetUI<TextMeshProUGUI>("OpenText");
    }

    public void UpdateOpenText()
    {
        _sb.Clear();
        _sb.Append("OPEN");
        _openText.SetText(_sb);
    }
    public void UpdateCloseText()
    {
        _sb.Clear();
        _sb.Append("CLOSE");
        _openText.SetText(_sb);
    }
}
