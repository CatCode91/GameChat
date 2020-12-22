using TMPro;
using UnityEngine;

public class IpText : MonoBehaviour
{
    private TextMeshProUGUI _tmp;
    private ButtonConnect _btnConn;

    // Start is called before the first frame update
    void Start()
    {
        _tmp = GetComponent<TextMeshProUGUI>();
        _btnConn = GameObject.Find("btn_connect").GetComponent<ButtonConnect>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_tmp.text.Length <= 1) 
        {
            _btnConn.HideButton();
        }

        else
        {
            _btnConn.ShowButton();
        }

    }
}
