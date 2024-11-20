using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPause : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        panel.transform.localScale = Vector3.one * 0.1f;
    }

    void Start()
    {
        button.onClick.AddListener(OnClickPause);
    }

    private void OnClickPause()
    {
        UITween.OnClickEffect(gameObject);
        UITween.ShowUI(panel);
        UIManager.Instance.PauseClient();
    }
}
