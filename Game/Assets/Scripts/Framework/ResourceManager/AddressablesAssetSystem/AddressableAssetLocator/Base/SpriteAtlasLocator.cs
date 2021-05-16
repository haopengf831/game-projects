using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.U2D;

public abstract class SpriteAtlasLocator : AssetLocator
{
    protected static readonly Dictionary<string, SpriteAtlas> SpriteAtlases = new Dictionary<string, SpriteAtlas>();

    public static void LoadSpriteAtlasAsync(string spriteAtlasKey, Action<SpriteAtlas> succeededAction = null, Action failedAction = null)
    {
        if (string.IsNullOrWhiteSpace(spriteAtlasKey))
        {
            failedAction?.Invoke();
            return;
        }

        if (SpriteAtlases.TryGetValue(spriteAtlasKey, out var spriteAtlas))
        {
            if (spriteAtlas != null)
            {
                succeededAction?.Invoke(spriteAtlas);
                return;
            }
        }

        LoadAssetAsyncByCoroutine(spriteAtlasKey, (SpriteAtlas result) =>
        {
            SpriteAtlases[spriteAtlasKey] = result ? result : throw Exception;
            succeededAction?.Invoke(result);
        }, failedAction);
    }

    public static async Task LoadSpriteAtlasAsyncWithTask(string spriteAtlasKey, Action<SpriteAtlas> succeededAction = null, Action failedAction = null)
    {
        if (string.IsNullOrWhiteSpace(spriteAtlasKey))
        {
            failedAction?.Invoke();
            return;
        }

        if (SpriteAtlases.TryGetValue(spriteAtlasKey, out var spriteAtlas))
        {
            if (spriteAtlas != null)
            {
                succeededAction?.Invoke(spriteAtlas);
                return;
            }
        }

        await LoadAssetAsyncByTask(spriteAtlasKey, (SpriteAtlas result) =>
        {
            SpriteAtlases[spriteAtlasKey] = result ? result : throw Exception;
            succeededAction?.Invoke(result);
        }, failedAction);
    }

    public static void ReleaseSpriteAtlas(string spriteAtlasKey)
    {
        if (string.IsNullOrWhiteSpace(spriteAtlasKey))
        {
            return;
        }

        if (SpriteAtlases.TryGetValue(spriteAtlasKey, out var spriteAtlas))
        {
            SpriteAtlases.Remove(spriteAtlasKey);
            ReleaseAsset(spriteAtlas);
        }
    }

    public static void ReleaseSpriteAtlas(SpriteAtlas spriteAtlas)
    {
        if (spriteAtlas != null)
        {
            SpriteAtlases.Remove(spriteAtlas);
        }

        ReleaseAsset(spriteAtlas);
    }

    public static void ReleaseSpriteAtlases()
    {
        foreach (var pair in SpriteAtlases.Values)
        {
            ReleaseAsset(pair);
        }

        SpriteAtlases.Clear();
    }
}