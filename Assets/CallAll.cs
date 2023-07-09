using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallAll : MonoBehaviour
{
    public void Call()
    {
        ProximityIndicatorHandler.TEMP_CallAll_TAB?.Invoke();
    }
}
