using UnityEngine;

public class Button : MonoBehaviour
{
    #region Fields
    
    public Color defaultColor;
    public Color activeColor;
    public GameObject push;

    [SerializeField] private AudioClip audioClip;
    
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
    
    private void OnGazeEnterEvent()
    {
        GameObject.Find("Cursor").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("red_cursor_circle");
    }

    private void OnGazeLeaveEvent()
    {
        GameObject.Find("Cursor").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("blue_cursor_circle");
    }

    public void PlaySound()
    {
        AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position, 1);
    }

    #endregion
}
