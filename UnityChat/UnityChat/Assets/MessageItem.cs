using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageItem : MonoBehaviour
{
    private Text _caption;
    private Text _text;
    private Text _time;

    // Start is called before the first frame update
    void Start()
    {
        Transform panel = transform.GetChild(0);

        _caption = panel.GetChild(0).GetComponent<Text>();
        _text = panel.GetChild(1).GetComponent<Text>();
        _time = panel.GetChild(2).GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddMessage()
    {
       
    }

  
}
