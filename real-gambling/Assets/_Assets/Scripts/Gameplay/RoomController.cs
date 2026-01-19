using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class RoomController : MonoBehaviour
{
    private float wheelSpawnOffset = 1f;
    private List<GamblingWheel> wheels = new List<GamblingWheel>();
    public GameObject wheelPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Debug spinning wheel on key press
        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            Debug.Log("A");
            SpawnWheel();
        }
    }

    void SpawnWheel()
    {
        var offset = new Vector3();
        if (wheels.LastOrDefault() is var lastWheel && lastWheel != null)
        {
            offset.x = wheelSpawnOffset * wheels.Count;
        }
        var wheelObj = Instantiate(wheelPrefab, offset, wheelPrefab.transform.rotation);
        GamblingWheel wheel = wheelObj.GetComponent<GamblingWheel>();
        wheels.Add(wheel);
    }
}
