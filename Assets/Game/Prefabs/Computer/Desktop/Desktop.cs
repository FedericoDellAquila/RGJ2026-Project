using System;
using UnityEngine;
using UnityEngine.UI;

public class Desktop : MonoBehaviour
{
    [SerializeField] private Button Button;

    private void Awake()
    {
        gameObject.SetActive(false);
        
        if (Button != null)
            Button.onClick.AddListener(() =>
            {
                gameObject.SetActive(false);
            });
    }
}
