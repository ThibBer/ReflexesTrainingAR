using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class BaseGesture : MonoBehaviour
{
    #region Fields
    private GestureRecognizer m_GestureRecognizer;
    #endregion

    #region Methods

    private void OnTapped(TappedEventArgs tappedEventArgs)
    {
        // https://docs.unity3d.com/2018.2/Documentation/Manual/SpatialMappingCollider.html
        var gazeRay = new Ray(tappedEventArgs.headPose.position, tappedEventArgs.headPose.forward);
        var hits = Physics.RaycastAll(gazeRay, float.MaxValue);
        foreach (RaycastHit hit in hits)
        {
            handleHit(hit);
        }
    }

    public virtual void handleHit(RaycastHit hit)
    {

    }

    private void Awake()
    {
        m_GestureRecognizer = new GestureRecognizer();
        m_GestureRecognizer.StartCapturingGestures();
        m_GestureRecognizer.Tapped += OnTapped;
    }

    private void OnDestroy()
    {
        if (m_GestureRecognizer != null)
        {
            m_GestureRecognizer.Tapped -= OnTapped;
            m_GestureRecognizer.StopCapturingGestures();
            m_GestureRecognizer.Dispose();
        }
    }
    #endregion
}
