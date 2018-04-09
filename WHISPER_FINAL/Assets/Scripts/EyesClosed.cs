using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fove.Managed;
using UnityEngine.UI;


public class EyesClosed : MonoBehaviour
{
    private EFVR_Eye fove;
    public bool isOpen;

    void Update()
    {
        fove = FoveInterface.CheckEyesClosed();

        if (fove != null)
        {
            if (fove == EFVR_Eye.Neither)
                isOpen = true;

            else if (fove == EFVR_Eye.Both)
                isOpen = false;
        }
    }
}
