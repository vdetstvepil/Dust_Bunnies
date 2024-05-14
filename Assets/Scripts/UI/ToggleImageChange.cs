using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleImageChange : MonoBehaviour
{
    public GameObject checkmarkObject;

    public Sprite checkmarkOnSprite;
    public Sprite checkmarkOffSprite;

    public bool isEnabled = false;

    public void ChangeSetting()
    {
        Image checkmarkImage = checkmarkObject.GetComponent<Image>();

        isEnabled = !isEnabled;
        checkmarkImage.sprite = isEnabled ? checkmarkOnSprite : checkmarkOffSprite;
    }
}
