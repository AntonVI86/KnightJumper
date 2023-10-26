using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TextSizeChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TMP_Text _text;

    private Button _button;
    private float _defaultSize;
    private Color _defaultColor;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void Start()
    {
        _defaultSize = _text.fontSize;
        _defaultColor = _text.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(_button.interactable)
            _text.fontSize += 2f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_button.interactable)
        {
            _text.fontSize = _defaultSize;
            _text.color = _defaultColor;
        }
    }
}
