using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [SerializeField] private ObstacleType obstacleType;

    [SerializeField] private float rotationSpeed;

    [SerializeField] private Collider[] colliders;

    private enum ObstacleType
    {
        Blades,
        SpinnerVertical,
        SpinnerHorizontal
    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        SetOperation();
    }

    private void SetOperation()
    {
        switch (obstacleType)
        {
            case ObstacleType.Blades:
                RunBlades();
                return;
            
            case ObstacleType.SpinnerVertical:
                RunSpinnerVer();
                return;
            
            case ObstacleType.SpinnerHorizontal:
                RunSpinnerHor();
                return;
        }
    }
    
    
    private void RunBlades()
    {
        float maxRotation = 60;
        float minRotation = 0;
        Transform colTransform = colliders[0].transform;
        

        if (maxRotation <= 60f)
        {
            colTransform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(minRotation, maxRotation, rotationSpeed));
        }
        else
        {
            colTransform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(maxRotation, minRotation, rotationSpeed));
        }
        
    }

    private void RunSpinnerVer()
    {
        Vector3 rotationVector = new Vector3(0, 0, 1f);
        
        foreach (var col in colliders)
        {
            col.transform.Rotate(rotationVector.x, rotationVector.y, rotationVector.z * rotationSpeed * Time.deltaTime);
        }
    }

    private void RunSpinnerHor()
    {
        Vector3 rotationVector = new Vector3(0, 10f, 0);

        foreach (var col in colliders)
        {
            col.transform.Rotate(rotationVector.x, rotationVector.y *  rotationSpeed * Time.deltaTime, rotationVector.z);
        }
        
        
    }
    
}
