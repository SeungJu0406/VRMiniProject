using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;

public class IngredientButtonUI : BaseUI
{
    [SerializeField] Result _result;

    [System.Serializable]
    public struct IngredientButton
    {
        public Image ButtonImage;
        public Button Button;
        public TextMeshProUGUI Text;
    }
    [SerializeField] IngredientButton[] _ingredientButtons;
    private void Start()
    {
        _ingredientButtons = new IngredientButton[GetGameObjectCoint()];

        SetButtonList(Result.IngredientType.CookedPatty, "PattyButton", "PattyButtonText");
        SetButtonList(Result.IngredientType.Lettuce, "LettuceButton", "LettuceButtonText");
        SetButtonList(Result.IngredientType.Onion, "OnionButton", "OnionButtonText");
        SetButtonList(Result.IngredientType.Tomate, "TomatoButton", "TomatoButtonText");
        SetButtonList(Result.IngredientType.Cheese, "CheeseButton", "CheeseButtonText");
    }

    public void OnClick(Result.IngredientType type)
    {
        ProcessButton(type);
        SetButtonText(type);
    }
    public void ProcessButton(Result.IngredientType type)
    {
        bool value = _result.GetUsableList(type);
        if (value)
        {
            _result.SetUsableList(type, false);
            _ingredientButtons[(int)type].ButtonImage.color = Color.white;
        }
        else
        {
            _result.SetUsableList(type, true);
            _ingredientButtons[(int)type].ButtonImage.color = Color.cyan;
        }
    }


    void SetButtonText(Result.IngredientType type)
    {
        _sb.Clear();
        if (_result.GetUsableList(type))
        {
            _sb.Append("ÄÑÁü");
        }
        else
        {
            _sb.Append("²¨Áü");
        }
        _ingredientButtons[(int)type].Text.SetText(_sb);
    }

    void SetButtonList(Result.IngredientType type, in string button, in string text)
    {
        IngredientButton ingredientButton = new IngredientButton();
        ingredientButton.ButtonImage = GetUI<Image>(button);
        ingredientButton.Button = GetUI<Button>(button);
        ingredientButton.Text = GetUI<TextMeshProUGUI>(text);
        _ingredientButtons[(int)type] = ingredientButton;

        _ingredientButtons[(int)type].Button.onClick.AddListener(() =>OnClick(type));
    }
}
