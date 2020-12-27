using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


/// <summary>
/// Скрипт анимашки по клику на логотип
/// </summary>
public class ClickLogo : MonoBehaviour, IPointerClickHandler
{
    private Animation _animation;

    public void OnPointerClick(PointerEventData eventData)
    {
        _animation.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        _animation = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
