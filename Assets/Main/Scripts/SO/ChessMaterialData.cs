using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Chess Material Data", menuName ="Scriptable Object/Chess Material Data", order = int.MaxValue)]
public class ChessMaterialData : ScriptableObject
{
    [SerializeField]
    ChessMaterial[] chessMaterial;

    public ChessMaterial[] ChessMaterial { get { return chessMaterial; } }
}

[System.Serializable]
public class ChessMaterial
{

    public SelectType type;
    public Material material;

} 