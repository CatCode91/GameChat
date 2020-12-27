using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//синглтон для Unity объекта ErrorMessage - для показа окошка с ошибкой при ее возникновении


namespace Assets.Scripts
{
    public class ErrorMessage : MonoBehaviour, IPointerClickHandler
    {
        public static ErrorMessage instance = null;

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
        }

        //прячет окно по тапу в любом месте
        public void OnPointerClick(PointerEventData eventData)
        {
            gameObject.SetActive(false);
        }

        //открывает окошко с ошибкой (по-хорошему вместо двух параметров нужно было бы завернуть объект и подсовывать интерфейс)
        public void ShowMessage(string caption, string mess) 
        {
            gameObject.SetActive(true);
            Text[] texts = GetComponentsInChildren<Text>();
            texts[0].text = caption;
            texts[1].text = mess;
            GetComponentInChildren<Animation>().Play();
        }
    }
}
