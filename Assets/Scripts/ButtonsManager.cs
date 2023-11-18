using UnityEngine;
using Random = UnityEngine.Random;

public class ButtonsManager : MonoBehaviour
{
    #region Fields

    private const float SPAWN_DISTANCE_FROM_PLAYER = 20;
    private readonly Quaternion m_MinSpawnAngles = Quaternion.Euler(-50, -50, 0);
    private readonly Quaternion m_MaxSpawnAngles = Quaternion.Euler(50, 50, 0);
    
    [SerializeField]
    private Button button;
    private Button m_CurrentButton;
    /// <summary>
    /// Number of generated buttons so far
    /// </summary>
    private int m_GeneratedNumber;
    #endregion

    #region Properties
    /// <summary>
    /// Number of tapped buttons
    /// </summary>
    public int Tapped { get => m_GeneratedNumber - 1; }

    /// <summary>
    /// Total distance between all the generated buttons
    /// </summary>
    public float TotalDistance { get; private set; }
    #endregion

    #region Methods

    public void GenerateNextButton()
    {
        var cameraPosition = Camera.main.transform.position;
        var buttonPos = GetRandomSphericalButtonPosition(cameraPosition);
        var buttonRotation = GetRotationToLookAtTarget(buttonPos, cameraPosition);

        var btn = Instantiate(button, buttonPos, buttonRotation);

        if (m_CurrentButton == null)
        {
            m_CurrentButton = btn;
        }

        m_GeneratedNumber++;
        TotalDistance += m_CurrentButton != null ? Vector3.Distance(m_CurrentButton.transform.position, btn.transform.position) : 0;
        btn.IsActive = true;
        m_CurrentButton = btn;
    }

    private Vector3 GetRandomSphericalButtonPosition(Vector3 startPosition)
    {
        var xAngle = Random.Range(m_MinSpawnAngles.x, m_MaxSpawnAngles.x);
        var yAngle = Random.Range(m_MinSpawnAngles.y, m_MaxSpawnAngles.y);
        
        // Debug.Log("X degree angle : " + xAngle * 180 / Mathf.PI);
        // Debug.Log("Y degree angle : " + yAngle * 180 / Mathf.PI);

        var position = new Vector3(Mathf.Sin(yAngle), Mathf.Sin(xAngle), Mathf.Cos(yAngle));

        return startPosition + position * SPAWN_DISTANCE_FROM_PLAYER;
    }

    private Quaternion GetRotationToLookAtTarget(Vector3 origin, Vector3 target, float xOffset = -90)
    {
        var direction = target - origin;
        
        var xRotation = Mathf.Atan2(direction.z, direction.y) * 180f / Mathf.PI;
        var yRotation = xOffset - Mathf.Atan2(direction.z, direction.x) * 180f / Mathf.PI;
        
        return Quaternion.Euler(xRotation, yRotation, 0);
    }

    public void RemoveLast()
    {
        m_CurrentButton.IsActive = false;
    }

    private void Start()
    {
        GenerateNextButton();
        button.IsActive = false; // Template button: disable/hide it in another way
    }
    #endregion
}