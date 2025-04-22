using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]private bool _isMoving;
    private Vector3 _origPos, _targetPos;
    private float _timeToMove = 0.2f;
    public Tilemap grid;
    public RuleTile ruleTile;
    void Update()
    {
        if (Input.GetKey(KeyCode.W) && !_isMoving) StartCoroutine(MovePlayer(Vector3.up));
        if (Input.GetKey(KeyCode.S) && !_isMoving) StartCoroutine(MovePlayer(Vector3.down));
        if (Input.GetKey(KeyCode.A) && !_isMoving) StartCoroutine(MovePlayer(Vector3.left));
        if (Input.GetKey(KeyCode.D) && !_isMoving) StartCoroutine(MovePlayer(Vector3.right));
    }

    private IEnumerator MovePlayer(Vector3 direction)
    {
        _isMoving = true;
        
        float elapsedTime = 0;
        
        _origPos = transform.position;
        _targetPos = _origPos + direction;
        
        Vector3Int targetCell = grid.WorldToCell(_targetPos);
        
        // Check if target cell matches rule tile
        if (grid.GetTile(targetCell) == ruleTile)
        {
            _isMoving = false;
            yield break;
        }

        
        while (elapsedTime < _timeToMove)
        {
            transform.position = Vector3.Lerp(transform.position, _targetPos, elapsedTime / _timeToMove);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        transform.position = _targetPos;

        _isMoving = false;
    }

    
}
