using UnityEngine;
using System.Collections.Generic;

public class MainroomTableData
{
    /// <summary>
    /// 编号
    /// <summary>
    public int Id { get; } = 0;

    /// <summary>
    /// 用户类型
    /// <summary>
    public byte UserType { get; } = 0;

    /// <summary>
    /// 场景id
    /// <summary>
    public int SceneId { get; } = 0;

    /// <summary>
    /// 场景世界坐标
    /// <summary>
    public Vector3 SceneWorldPosition { get; } = Vector3.zero;

    /// <summary>
    /// 主角坐标
    /// <summary>
    public Vector3 PersonWorldPosition { get; } = Vector3.zero;

}