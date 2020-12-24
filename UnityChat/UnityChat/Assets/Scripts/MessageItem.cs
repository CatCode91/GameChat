using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageItem : MonoBehaviour
{
    private Text _caption;
    private Text _text;
    private Text _time;


    public void FillFields(IMessageChat message)
    {
        Transform panel = transform.GetChild(0);

        _caption = panel.GetChild(0).GetComponent<Text>();
        _text = panel.GetChild(1).GetComponent<Text>();
        _time = panel.GetChild(2).GetComponent<Text>();

        _caption.text = message.Caption;
        _text.text = message.Text;
        _time.text = message.Time;
    }

  
}
