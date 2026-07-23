using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnLocation : MonoBehaviour
{
    [Header("spawn location")]
    public Transform spawnPoint;

    [Header("spawn keys")]
    public PortalKey portalKey;

    [Header("Location name")]
    public string locationName;

}