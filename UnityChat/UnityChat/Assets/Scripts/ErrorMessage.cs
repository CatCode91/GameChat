using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

        public void OnPointerClick(PointerEventData eventData)
        {
            gameObject.SetActive(false);
        }

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
