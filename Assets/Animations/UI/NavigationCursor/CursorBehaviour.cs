using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorBehaviour : MonoBehaviour
{
    public GameObject cursor;
    void SetNavigatorIconInactive()
    {
        cursor.SetActive(false);
    }

    void SetNavigatorIconActive()
    {
        cursor.SetActive(true);
    }
}
