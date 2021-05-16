using System.Collections.Generic;
using UnityEngine;

public class MeshRendererMaterialSwitch : MonoBehaviour
{
    public class SwitchRenderer
    {
        public MeshRenderer MeshRenderer;
        public Material OriginalMaterial;

        public SwitchRenderer(MeshRenderer renderer, Material material)
        {
            MeshRenderer = renderer;
            OriginalMaterial = material;
        }

        public void Restore()
        {
            if (MeshRenderer != null)
            {
                MeshRenderer.material = OriginalMaterial;
            }
        }
    }

    [DisplayOnly] protected List<SwitchRenderer> MeshRenderers = new List<SwitchRenderer>();

    private void Awake()
    {
        try
        {
            FillMeshRendererList();
        }
        catch
        {
            throw;
        }
    }

    /// <summary>
    /// 填充MeshRenderer列表
    /// </summary>
    protected virtual void FillMeshRendererList()
    {
        try
        {
            if (MeshRenderers == null)
            {
                MeshRenderers = new List<SwitchRenderer>();
            }

            MeshRenderers.Clear();
            var renderers = GetComponentsInChildren<MeshRenderer>(true);
            if (renderers != null)
            {
                foreach (var tempRenderer in renderers)
                {
                    if (tempRenderer != null)
                    {
                        var switchRenderer = new SwitchRenderer(tempRenderer, tempRenderer.material);
                        MeshRenderers.Add(switchRenderer);
                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }

    public virtual void SetMaterial(Material material)
    {
        try
        {
            if (material == null)
            {
                return;
            }

            MeshRenderers?.ForEach(tempMeshRenderer =>
            {
                if (tempMeshRenderer != null && tempMeshRenderer.MeshRenderer != null)
                {
                    tempMeshRenderer.MeshRenderer.material = material;
                }
            });
        }
        catch
        {
            throw;
        }
    }

    public virtual void SetMaterial(Material material, Texture mainTexture)
    {
        try
        {
            if (material == null || mainTexture == null)
            {
                return;
            }

            material.mainTexture = mainTexture;

            SetMaterial(material);
        }
        catch
        {
            throw;
        }
    }

    public virtual void SetMaterialMainTexture(Texture mainTexture)
    {
        try
        {
            if (mainTexture == null)
            {
                return;
            }

            MeshRenderers?.ForEach(tempMeshRenderer =>
            {
                if (tempMeshRenderer != null && tempMeshRenderer.MeshRenderer != null && tempMeshRenderer.MeshRenderer.material != null)
                {
                    tempMeshRenderer.MeshRenderer.material.mainTexture = mainTexture;
                }
            });
        }
        catch
        {
            throw;
        }
    }

    public virtual void Restore()
    {
        try
        {
            MeshRenderers?.ForEach(tempSwitchRenderer => { tempSwitchRenderer?.Restore(); });
        }
        catch
        {
            throw;
        }
    }
}