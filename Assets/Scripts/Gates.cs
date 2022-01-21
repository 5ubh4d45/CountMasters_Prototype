using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gates : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private Multiplier leftMultiplier;
    [SerializeField] private Multiplier rightMultiplier;

    [Space][Header("Components")]
    [SerializeField] private Collider[] gateCol;
    [SerializeField] private TextMeshPro leftGateText;
    [SerializeField] private TextMeshPro rightGateText;


    private Multiplier _currentMultiplier;
    public enum MultiplierType { add, multiply }
    
    // Start is called before the first frame update
    void Start()
    {
        SetGateText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetGateText()
    {
        leftGateText.text = leftMultiplier.GetMultiplierText();
        rightGateText.text = rightMultiplier.GetMultiplierText();
    }

    public int GetRunnerAmount(Collider gateCollider, int currentRunnerAmount)
    {
        DisableGates();
    
        // checks for if it is left or right gate
        if (gateCollider.transform.position.x > 0)
        {
            _currentMultiplier = rightMultiplier;
            
            return CalculateMultiplier(currentRunnerAmount);
        }

        _currentMultiplier = leftMultiplier;
            
        return CalculateMultiplier(currentRunnerAmount);
    }

    private int CalculateMultiplier(int currentRunnerAmount)
    {
        //calculates the values to add
        switch (_currentMultiplier.GetMultiplierType())
        {
            case MultiplierType.add:
                return _currentMultiplier.GetMultiplierValue();
            
            case MultiplierType.multiply:
                return ((currentRunnerAmount * _currentMultiplier.GetMultiplierValue()) - currentRunnerAmount);
            
            default:
                return 0;
        }
    }

    private void DisableGates()
    {
        foreach (Collider collider in gateCol)
        {
            collider.enabled = false;
        }
    }
    

    [System.Serializable]
    public struct Multiplier
    {
        [SerializeField] private MultiplierType multiplierType;
        [SerializeField] private int multiplierValue;

        public Multiplier(MultiplierType multiplierType, int multiplierValue)
        {
            this.multiplierType = multiplierType;
            this.multiplierValue = multiplierValue;
        }

        public MultiplierType GetMultiplierType()
        {
            return multiplierType;
        }

        public int GetMultiplierValue()
        {
            return multiplierValue;
        }

        public string GetMultiplierText()
        {
            if (multiplierType == MultiplierType.add)
            {
                return "+" + multiplierValue;
            }
            else
            {
                return "x" + multiplierValue;
            }
        }
    }
    

}
