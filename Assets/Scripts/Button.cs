using UnityEngine;

public class Button : MonoBehaviour
{
    #region Fields
    
    public Color defaultColor;
    public Color activeColor;
    public GameObject mesh;

    [SerializeField] private AudioClip audioClip;
    
    private bool m_IsActive;
    
    private GameObject m_Cursor;
    private SpriteRenderer m_CursorSpriteRenderer;

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
        m_Cursor = GameObject.Find("Cursor");
        m_CursorSpriteRenderer = m_Cursor.GetComponent<SpriteRenderer>();
        
        UpdateColor();
    }

    private void UpdateColor()
    {
        // TODO: remove activeColor / defaultColor logic if not needed
        SetColor(m_IsActive ? activeColor : defaultColor);
    }

    private void SetColor(Color color)
    {
        mesh.GetComponent<Renderer>().material.color = color;
    }

    private void Update()
    {
        var cameraTransform = m_Cursor.gameObject.transform;

        var spriteName = Physics.Raycast(cameraTransform.position, cameraTransform.forward, out _, 100.0f)
            ? "red_cursor_circle"
            : "white²²_cursor_circle";
        m_CursorSpriteRenderer.sprite = Resources.Load<Sprite>(spriteName);
    }

    public void PlaySound()
    {
        AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position, 1);
    }

    #endregion
}
