using DG.Tweening;
using UnityEngine;

public class ScreenFade : MonoBehaviour
{
    [Tooltip("Fade duration")] public float FadeTime = 2.0f;

    [Tooltip("Screen color at maximum fade")]
    public Color FadeColor = new Color(0.01f, 0.01f, 0.01f, 1.0f);

    /// <summary>
    /// The render queue used by the fade mesh. Reduce this if you need to render on top of it.
    /// </summary>
    public int RenderQueue = 5000;

    private float m_UiFadeAlpha = 0;

    public MeshRenderer m_FadeRenderer;
    private MeshFilter m_FadeMesh;
    public Material FadeMaterial;
    private bool m_IsFading = false;

    public float CurrentAlpha { get; private set; }

    private void Awake()
    {
        if (FadeMaterial == null)
        {
            FadeMaterial = new Material(Shader.Find("Standard"));
        }

        m_FadeMesh = gameObject.RequireComponent<MeshFilter>();
        m_FadeRenderer = gameObject.RequireComponent<MeshRenderer>();

        var mesh = new Mesh();
        m_FadeMesh.mesh = mesh;

        Vector3[] vertices = new Vector3[4];

        float width = 2f;
        float height = 2f;
        float depth = 1f;

        vertices[0] = new Vector3(-width, -height, depth);
        vertices[1] = new Vector3(width, -height, depth);
        vertices[2] = new Vector3(-width, height, depth);
        vertices[3] = new Vector3(width, height, depth);

        mesh.vertices = vertices;

        int[] tri = new int[6];

        tri[0] = 0;
        tri[1] = 2;
        tri[2] = 1;

        tri[3] = 2;
        tri[4] = 3;
        tri[5] = 1;

        mesh.triangles = tri;

        Vector3[] normals = new Vector3[4];

        normals[0] = -Vector3.forward;
        normals[1] = -Vector3.forward;
        normals[2] = -Vector3.forward;
        normals[3] = -Vector3.forward;

        mesh.normals = normals;

        Vector2[] uv = new Vector2[4];

        uv[0] = new Vector2(0, 0);
        uv[1] = new Vector2(1, 0);
        uv[2] = new Vector2(0, 1);
        uv[3] = new Vector2(1, 1);

        mesh.uv = uv;

        SetFadeLevel(0);
    }

    /// <summary>
    /// Cleans up the fade material
    /// </summary>
    private void OnDestroy()
    {
        if (m_FadeRenderer != null)
        {
            Destroy(m_FadeRenderer);
        }

        if (m_FadeMesh != null)
        {
            Destroy(m_FadeMesh);
        }
    }

    protected Tweener FadeTween;

    /// <summary>
    /// Start a fade out
    /// </summary>
    public void Fade(float from, float to, float duration, TweenCallback callback)
    {
        FadeTween?.Kill();

        FadeTween = DOVirtual.Float(from, to, duration, value =>
        {
            CurrentAlpha = value;
            SetMaterialAlpha();
        }).SetAutoKill().OnKill(callback);
    }

    /// <summary>
    /// Set the UI fade level - fade due to UI in foreground
    /// </summary>
    public void SetUiFade(float level)
    {
        m_UiFadeAlpha = Mathf.Clamp01(level);
        SetMaterialAlpha();
    }

    /// <summary>
    /// Override current fade level
    /// </summary>
    /// <param name="level"></param>
    public void SetFadeLevel(float level)
    {
        CurrentAlpha = level;
        SetMaterialAlpha();
    }

    /// <summary>
    /// Update material alpha. UI fade and the current fade due to fade in/out animations (or explicit control)
    /// both affect the fade. (The max is taken)
    /// </summary>
    private void SetMaterialAlpha()
    {
        var color = FadeColor;
        color.a = Mathf.Max(CurrentAlpha, m_UiFadeAlpha);
        m_IsFading = color.a > 0;
        if (FadeMaterial == null) return;
        FadeMaterial.color = color;
        FadeMaterial.renderQueue = RenderQueue;
        m_FadeRenderer.material = FadeMaterial;
        m_FadeRenderer.enabled = m_IsFading;
    }
}