using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class SpriteLocator : AssetLocator
{
    protected static readonly Dictionary<string, Sprite> Sprites = new Dictionary<string, Sprite>();

    public static void LoadSpriteAsync(string spriteKey, Action<Sprite> succeededAction = null, Action failedAction = null)
    {
        if (string.IsNullOrWhiteSpace(spriteKey))
        {
            failedAction?.Invoke();
            return;
        }

        if (Sprites.TryGetValue(spriteKey, out var sprite))
        {
            if (sprite != null)
            {
                succeededAction?.Invoke(sprite);
                return;
            }
        }

        LoadAssetAsyncByCoroutine(spriteKey, (Sprite result) =>
        {
            Sprites[spriteKey] = result ? result : throw Exception;
            succeededAction?.Invoke(result);
        }, failedAction);
    }

    public static async Task LoadSpriteAsyncWithTask(string spriteKey, Action<Sprite> succeededAction = null, Action failedAction = null)
    {
        if (string.IsNullOrWhiteSpace(spriteKey))
        {
            failedAction?.Invoke();
            return;
        }

        if (Sprites.TryGetValue(spriteKey, out var sprite))
        {
            if (sprite != null)
            {
                succeededAction?.Invoke(sprite);
                return;
            }
        }

        await LoadAssetAsyncByTask(spriteKey, (Sprite result) =>
        {
            Sprites[spriteKey] = result ? result : throw Exception;
            succeededAction?.Invoke(result);
        }, failedAction);
    }

    public static void ReleaseSprite(string spriteKey)
    {
        if (string.IsNullOrWhiteSpace(spriteKey))
        {
            return;
        }

        if (Sprites.TryGetValue(spriteKey, out var sprite))
        {
            Sprites.Remove(spriteKey);
            ReleaseAsset(sprite);
        }
    }

    public static void ReleaseSprite(Sprite sprite)
    {
        if (sprite != null)
        {
            Sprites.Remove(sprite);
        }

        ReleaseAsset(sprite);
    }

    public static void ReleaseSprites()
    {
        foreach (var pair in Sprites.Values)
        {
            ReleaseAsset(pair);
        }

        Sprites.Clear();
    }
}