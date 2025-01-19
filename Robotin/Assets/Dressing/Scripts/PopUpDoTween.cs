using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PopUpDoTween : MonoBehaviour
{
    private Button button;
    private Vector3 defaultScale;

    [Header("Variables")]
    public float initialSpeed = 0.5f;
    public float hoverSpeed = 0.2f;

    public void Awake()
    {
        button = GetComponent<Button>();
        
        defaultScale = transform.localScale;
    }
    
    private void Start()
    {
        EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((data) => { OnMouseEnter(); });
        trigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerExit;
        entry.callback.AddListener((data) => { OnMouseExit(); });
        trigger.triggers.Add(entry);
        
        button.onClick.AddListener(OnMouseClick);
    }

    private void OnMouseEnter()
    {
        transform.DOScale(defaultScale * 1.1f, hoverSpeed);
    }

    private void OnMouseExit()
    {
        transform.DOScale(defaultScale, hoverSpeed);
    }

    private void OnMouseClick()
    {
        transform.DOScale(defaultScale * 0.8f, hoverSpeed);
    }

}
