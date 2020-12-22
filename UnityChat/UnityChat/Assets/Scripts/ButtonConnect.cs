using Assets.Scripts;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonConnect : MonoBehaviour
{
    private Animation _anim;
    private bool isVisible = false;

    private Canvas _chat;
    private Canvas _menu;

    private Button _btn;

    private string _ipAdress;
    private string _port;
    public UnityEvent<ErrorMessage> ErrorHappend; 

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animation>();
        _chat = GameObject.FindGameObjectWithTag("Chat").GetComponent<Canvas>();
        _menu = GameObject.FindGameObjectWithTag("MainMenu").GetComponent<Canvas>();

        _btn = GetComponent<Button>();
        _btn.onClick.AddListener(EntryInChat);

        _ipAdress = _menu.GetComponentInChildren<IpText>().gameObject.GetComponent<TextMeshProUGUI>().text;
        _port = _menu.GetComponentInChildren<TextMeshProUGUI>().text;

    }

    public void ShowButton() 
    {
        if (!isVisible) 
        {
            _anim.Play("ShowButton");
            isVisible = true;
        }
      
    }

    public void HideButton() 
    {
        if (isVisible) 
        {
            _anim.Play("HideButton");
            isVisible = false;
        }    
    }

    public void EntryInChat() 
    {

        if (string.IsNullOrWhiteSpace(_ipAdress) || string.IsNullOrWhiteSpace(_port))
        {
            ErrorHappend?.Invoke(new ErrorMessage(){Caption = "Внимание!", Text = "Введите корректный IP адрес" });
            return;
        }

        _chat.GetComponent<Animation>().Play();
        _menu.GetComponent<Animation>().Play();
    }
}
