using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICloseButton : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    void Start()
    {
        button.onClick.AddListener(OnClickClose);
    }

    private void OnClickClose()
    {
        UITween.OnClickEffect(gameObject);
        UITween.HideUI(panel);
        UIManager.Instance.PauseClient();
    }
}
