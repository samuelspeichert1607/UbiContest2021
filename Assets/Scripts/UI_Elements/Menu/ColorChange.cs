using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ColorChange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Color colorHover;
    [SerializeField] private Color colorDefault;
    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        text.color = new Color(colorDefault.r,colorDefault.g,colorDefault.b,colorDefault.a);
    }
    
    //Possibly pointless
    public void OnPointerExit (PointerEventData eventData)
    {
        text.color = new Color(colorDefault.r,colorDefault.g,colorDefault.b,colorDefault.a);
    }

    public void OnPointerEnter (PointerEventData eventData)
    {
        text.color = new Color(colorHover.r,colorHover.g,colorHover.b,colorHover.a);
    }
}
