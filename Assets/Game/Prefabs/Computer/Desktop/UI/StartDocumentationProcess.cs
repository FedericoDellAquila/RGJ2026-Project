using System;
using System.Collections;
using Game.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class StartDocumentationProcess : MonoBehaviour
{
    [SerializeField] private Button button;

    private void Awake()
    {
        button.onClick.AddListener(OnButtonClicked);
    }
    
    private void OnButtonClicked()
    {
        button.gameObject.SetActive(false);
        StartCoroutine(DocumentationRoutine());
    }

    private IEnumerator DocumentationRoutine()
    {
        yield return new WaitForSeconds(1.0f);
        EventManager.Instance.TriggerDocumentationProcessStarted();
        Destroy(gameObject);
    }
}
