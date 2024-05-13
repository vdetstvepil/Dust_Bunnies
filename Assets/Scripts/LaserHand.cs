using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.Extras;

public class LaserHand : SteamVR_LaserPointer
{
    public override void OnPointerIn(PointerEventArgs e)
    {
        base.OnPointerIn(e);
    }

    public override void OnPointerClick(PointerEventArgs e)
    {
        base.OnPointerIn(e);

        if (e.target.CompareTag("ButtonUI"))
        {
            e.target.GetComponent<Button>().onClick.Invoke();
        }
    }

    public override void OnPointerOut(PointerEventArgs e)
    {
        base.OnPointerIn(e);
    }
}
