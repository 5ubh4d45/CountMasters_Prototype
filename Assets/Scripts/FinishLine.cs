using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private bool _callNextLvl = true;
    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(CallNextLevel());
        _callNextLvl = false;
    }

    private IEnumerator CallNextLevel()
    {
        if (!_callNextLvl) yield return null;
        
        yield return new WaitForSeconds(0.5f);
        
        UIManager.Instance.NextLevel();
    }
}
