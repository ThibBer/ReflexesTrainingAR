using UnityEngine;

public class Button : MonoBehaviour
{
    #region Fields
    
    public Color defaultColor;
    public Color activeColor;
    public GameObject push;
    
    private bool m_IsActive;

    #endregion

    #region Properties
    
    public bool IsActive
    {
        get => m_IsActive;
        set
        {
            m_IsActive = value;
            gameObject.SetActive(value);
        }
    }

    #endregion

    #region Methods

    // Start is called before the first frame update
    private void Start()
    {
        UpdateColor();
    }

    private void UpdateColor()
    {
        // TODO: remove activeColor / defaultColor logic if not needed
        SetColor(m_IsActive ? activeColor : defaultColor);
    }

    private void SetColor(Color color)
    {
        push.GetComponent<Renderer>().material.color = color;
    }
    
    #endregion
}
