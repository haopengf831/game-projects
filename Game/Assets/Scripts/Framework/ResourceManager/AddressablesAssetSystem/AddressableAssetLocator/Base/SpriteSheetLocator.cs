using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class SpriteSheetLocator : AssetLocator
{
    protected static readonly Dictionary<string, IList<Sprite>> SpriteSheets = new Dictionary<string, IList<Sprite>>();

    public static void LoadSpriteSheetAsync(string spriteSheetKey, Action<IList<Sprite>> succeededAction = null, Action failedAction = null)
    {
        if (string.IsNullOrWhiteSpace(spriteSheetKey))
        {
            failedAction?.Invoke();
            return;
        }

        if (SpriteSheets.TryGetValue(spriteSheetKey, out var spriteSheet))
        {
            if (spriteSheet != null)
            {
                succeededAction?.Invoke(spriteSheet);
                return;
            }
        }

        LoadAssetAsyncByCoroutine(spriteSheetKey, (IList<Sprite> result) =>
        {
            if (result != null)
            {
                SpriteSheets[spriteSheetKey] = result;
            }
            else
            {
                throw Exception;
            }

            succeededAction?.Invoke(result);
        }, failedAction);
    }

    public static async Task LoadSpriteSheetAsyncWithTask(string spriteSheetKey, Action<IList<Sprite>> succeededAction = null, Action failedAction = null)
    {
        if (string.IsNullOrWhiteSpace(spriteSheetKey))
        {
            failedAction?.Invoke();
            return;
        }

        if (SpriteSheets.TryGetValue(spriteSheetKey, out var spriteSheet))
        {
            if (spriteSheet != null)
            {
                succeededAction?.Invoke(spriteSheet);
                return;
            }
        }

        await LoadAssetAsyncByTask(spriteSheetKey, (IList<Sprite> result) =>
        {
            if (result != null)
            {
                SpriteSheets[spriteSheetKey] = result;
            }
            else
            {
                throw Exception;
            }

            succeededAction?.Invoke(result);
        }, failedAction);
    }

    public static void ReleaseSpriteSheet(string spriteSheetKey)
    {
        if (string.IsNullOrWhiteSpace(spriteSheetKey))
        {
            return;
        }

        if (SpriteSheets.TryGetValue(spriteSheetKey, out var spriteSheet))
        {
            SpriteSheets.Remove(spriteSheetKey);
            ReleaseAsset(spriteSheet);
        }
    }

    public static void ReleaseSpriteSheet(IList<Sprite> spriteSheet)
    {
        if (spriteSheet != null)
        {
            SpriteSheets.Remove(spriteSheet);
        }
        ReleaseAsset(spriteSheet);
    }

    public static void ReleaseSpriteSheets()
    {
        foreach (var pair in SpriteSheets.Values)
        {
            ReleaseAsset(pair);
        }

        SpriteSheets.Clear();
    }
}