using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuAnimator : MonoBehaviour
{
    public static MenuAnimator instance = null;

    private bool isVisible = false;
    private Canvas _chat;
    private Canvas _menu;
    private Button _btn;
    private Animation _animationButton;
    
    void Start()
    {
        if (instance == null)
        { 
            instance = this; 
        }

        else if (instance == this)
        { 
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        Initializator();
    }

    private void Initializator()
    {
        _chat = transform.GetChild(1).GetComponent<Canvas>();
        _menu = transform.GetChild(2).GetComponent<Canvas>();
        _btn = _menu.GetComponentInChildren<Button>();
        _animationButton = _btn.GetComponent<Animation>();
    }

    public void EntryChatAnimation()
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
