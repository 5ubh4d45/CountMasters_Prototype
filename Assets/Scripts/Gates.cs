using UnityEngine;
using TMPro;

public class Gates : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private Multiplier leftMultiplier;
    [SerializeField] private Multiplier rightMultiplier;

    [Space][Header("Components")]
    [SerializeField] private TextMeshPro leftGateText;
    [SerializeField] private TextMeshPro rightGateText;
    
    [Space][Tooltip("0 for Left; 1 for Right collider")]
    [SerializeField] private Collider[] gateCol;

    private Multiplier _currentMultiplier;
    public enum MultiplierType { Add, Multiply }
    
    // Start is called before the first frame update
    void Start()
    {
        SetGateText();
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
            
            //disable the gate
            // Debug.Log("rightGate");
            gateCol[1].gameObject.SetActive(false);
            
            return CalculateMultiplier(currentRunnerAmount);
        }

        _currentMultiplier = leftMultiplier;
        
        //disable the gate
        // Debug.Log("leftGate");
        gateCol[0].gameObject.SetActive(false);
        
        return CalculateMultiplier(currentRunnerAmount);
    }

    private int CalculateMultiplier(int currentRunnerAmount)
    {
        //calculates the values to add
        switch (_currentMultiplier.GetMultiplierType())
        {
            case MultiplierType.Add:
                return _currentMultiplier.GetMultiplierValue();
            
            case MultiplierType.Multiply:
                return ((currentRunnerAmount * _currentMultiplier.GetMultiplierValue()) - currentRunnerAmount);
            
            default:
                return 0;
        }
    }

    private void DisableGates()
    {
        foreach (Collider collider1 in gateCol)
        {
            collider1.enabled = false;
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
            if (multiplierType == MultiplierType.Add)
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
