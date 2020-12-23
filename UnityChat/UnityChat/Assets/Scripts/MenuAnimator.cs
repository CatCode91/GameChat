using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuAnimator : MonoBehaviour
{
    private Canvas _chat;
    private Canvas _menu;
    private Button _btn;
    private Animation _animationButton;
    private bool isVisible = false;
    private TMP_InputField[] _checkFields;

    void Start()
    {
        //ищем канвас с чатом
        _chat = transform.GetChild(1).GetComponent<Canvas>();
        //ищем канвас с регистрацией и входом
        _menu = transform.GetChild(2).GetComponent<Canvas>();
        //ищем кнопку на канвасе с регистрацией
        _btn = _menu.GetComponentInChildren<Button>();

        _animationButton = _btn.GetComponent<Animation>();
    }

    public void StartAnimation()
    {
        _chat.GetComponent<Animation>().Play();
        _menu.GetComponent<Animation>().Play();
    }

    public void CheckInputField(string text) 
    {
        if (text.Length == 0)
        {
            if(isVisible)
            _animationButton.Play("HideButton");
            isVisible = false;
        }

        else if (text.Length > 1)
        {
            if (!isVisible)
                _animationButton.Play("ShowButton");
            isVisible = true;
        }
    }
}
