using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTableSettings : MonoBehaviour
{
    public float hoverOffAt = 1f;
    public float clickOffAt = 1f;

    [Header("Materials")]
    public Material NormalMaterial;
    public Material HoverMaterial;
    public Material ClickMaterial;
}
