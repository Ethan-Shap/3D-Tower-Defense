using UnityEngine;
using UnityEngine.UI;

public class CustomToggle : MonoBehaviour
{
    public Sprite onSprite, offSprite;

    private Toggle toggle;
    private Image image;
    
    private void Awake()
    {
        toggle = GetComponentInChildren<Toggle>();
        image = GetComponent<Image>();
    }	 

    public void ToggleChangeHandler()
    {
        if (toggle.isOn)
            image.sprite = onSprite;
        else
            image.sprite = offSprite;
    }

}