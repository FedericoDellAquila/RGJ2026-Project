using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Computer : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Desktop Desktop;
    
    private Material targetMaterial;

    void Awake()
    {
        // Get the material from the Sprite Renderer
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            // Note: .material creates a unique instance for this object so it doesn't affect other objects sharing the same material
            targetMaterial = spriteRenderer.material;
        }
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (Desktop != null)
            Desktop.gameObject.SetActive(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (targetMaterial != null)
            targetMaterial.SetFloat("_OutlineEnabled", 1.0f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (targetMaterial != null)
            targetMaterial.SetFloat("_OutlineEnabled", 0.0f);
    }
}