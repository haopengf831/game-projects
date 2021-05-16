using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Loxodon.Framework.Execution;
using UnityEngine;
using UnityEngine.Video;

public abstract class VideoLocator : AssetLocator
{
    protected static readonly Dictionary<string, VideoClip> Videos = new Dictionary<string, VideoClip>();
    protected static bool IsCompressionEnabled;

    public static void LoadVideoAsync(string videoKey, Action<VideoClip> succeededAction = null, Action failedAction = null)
    {
        IsCompressionEnabled = Caching.compressionEnabled;

        if (!string.IsNullOrWhiteSpace(videoKey))
        {
            Caching.ClearCache();
            Caching.compressionEnabled = false;

            if (Videos.TryGetValue(videoKey, out var videoClip) && videoClip != null)
            {
                succeededAction?.Invoke(videoClip);
                Caching.compressionEnabled = IsCompressionEnabled;
            }
            else
            {
                LoadAssetAsyncByCoroutine(videoKey, (VideoClip result) =>
                {
                    Videos[videoKey] = result ? result : throw Exception;
                    succeededAction?.Invoke(result);
                    Caching.compressionEnabled = IsCompressionEnabled;
                }, failedAction);
            }
        }
        else
        {
            failedAction?.Invoke();
        }
    }

    public static async Task LoadVideoAsyncWithTask(string videoKey, Action<VideoClip> succeededAction = null, Action failedAction = null)
    {
        IsCompressionEnabled = Caching.compressionEnabled;

        if (!string.IsNullOrWhiteSpace(videoKey))
        {
            Caching.compressionEnabled = false;
            if (Videos.TryGetValue(videoKey, out var videoClip) && videoClip != null)
            {
                succeededAction?.Invoke(videoClip);
                Caching.compressionEnabled = IsCompressionEnabled;
            }
            else
            {
                await LoadAssetAsyncByTask(videoKey, (VideoClip result) =>
                {
                    Videos[videoKey] = result ? result : throw Exception;
                    succeededAction?.Invoke(result);
                    Caching.compressionEnabled = IsCompressionEnabled;
                }, failedAction);
            }
        }
        else
        {
            failedAction?.Invoke();
        }
    }

    public static void ReleaseVideo(string videoKey)
    {
        if (string.IsNullOrWhiteSpace(videoKey))
        {
            return;
        }

        if (Videos.TryGetValue(videoKey, out var video))
        {
            Videos.Remove(videoKey);
            ReleaseAsset(video);
        }
    }

    public static void ReleaseVideo(VideoClip video)
    {
        if (video != null)
        {
            Videos.Remove(video);
        }

        ReleaseAsset(video);
    }

    public static void ReleaseVideos()
    {
        foreach (var pair in Videos.Values)
        {
            ReleaseAsset(pair);
        }

        Videos.Clear();
    }
}