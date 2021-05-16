using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class ShaderLocator : AssetLocator
{
    protected static readonly Dictionary<string, Shader> Shaders = new Dictionary<string, Shader>();

    public static void LoadShaderAsync(string shaderKey, Action<Shader> succeededAction = null, Action failedAction = null)
    {
        if (string.IsNullOrWhiteSpace(shaderKey))
        {
            failedAction?.Invoke();
            return;
        }

        if (Shaders.TryGetValue(shaderKey, out Shader shader))
        {
            if (shader != null)
            {
                succeededAction?.Invoke(shader);
                return;
            }
        }

        LoadAssetAsyncByCoroutine(shaderKey, (Shader result) =>
        {
            Shaders[shaderKey] = result ? result : throw Exception;
            succeededAction?.Invoke(result);
        }, failedAction);
    }

    public static async Task LoadShaderAsyncWithTask(string shaderKey, Action<Shader> succeededAction = null, Action failedAction = null)
    {
        if (string.IsNullOrWhiteSpace(shaderKey))
        {
            failedAction?.Invoke();
            return;
        }

        if (Shaders.TryGetValue(shaderKey, out Shader shader))
        {
            if (shader != null)
            {
                succeededAction?.Invoke(shader);
                return;
            }
        }

        await LoadAssetAsyncByTask(shaderKey, (Shader result) =>
        {
            Shaders[shaderKey] = result ? result : throw Exception;
            succeededAction?.Invoke(result);
        }, failedAction);
    }

    public static void ReleaseShader(string shaderKey)
    {
        if (shaderKey == null)
        {
            return;
        }

        if (Shaders.TryGetValue(shaderKey, out var shader))
        {
            Shaders.Remove(shaderKey);
            ReleaseAsset(shader);
        }
    }
    
    public static void ReleaseShader(Shader shader)
    {
        if (shader != null)
        {
            Shaders.Remove(shader);
        }

        ReleaseAsset(shader);
    }

    public static void ReleaseAllShader()
    {
        foreach (var pair in Shaders.Values)
        {
            ReleaseAsset(pair);
        }

        Shaders.Clear();
    }
}