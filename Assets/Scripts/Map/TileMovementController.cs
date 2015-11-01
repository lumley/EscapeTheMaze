﻿using UnityEngine;
using System.Collections;
using Model;

public class TileMovementController : MonoBehaviour {

    // determinates how quick the player should move.
    // The unit expected here is tiles per second
    public float speed = 2.0f;

    private float interpolant = 1.0f;

    private Vector3 start = Vector3.zero;
    private Vector3 finish = Vector3.zero;

	private Tile currentTile;

	public enum RelativeDirection{
		FORWARD, RIGHT, BACKWARD, LEFT
	}

    public void SetCurrentTile(Tile tile)
    {
        currentTile = tile;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (IsMoving())
		{
			interpolant += Time.smoothDeltaTime * speed;
			// the interpolated vector between start and finish is the
			transform.position = Vector3.Lerp(start, finish, interpolant);
		}

	}

	private bool IsMoving(){
        return interpolant < 1.0f;
	}
	
	private void MoveLeft(){
		Move(RelativeDirection.LEFT);
	}
	
	private void MoveRight(){
		Move(RelativeDirection.RIGHT);
	}
	
	private void MoveForward(){
		Move(RelativeDirection.FORWARD);
	}
	
	private void MoveBackward(){
		Move(RelativeDirection.BACKWARD);
	}
	
	
	private void Move(RelativeDirection direction){
		if (!IsMoving()) {
			Tile destinationTile = GetTileInDirection (direction);
			if (destinationTile != null) {
                SetCurrentTile(destinationTile);
				interpolant = 0.0f;
				start = transform.position;
				finish = transform.position;
				finish += FacingHelper.GetDirectionVector (GetCardinalDirectionAtRelativeDirection (direction));
			}
		}
	}
	
	private Tile GetLeftTile() {
		return GetTileInDirection(RelativeDirection.LEFT);
	}
	
	private Tile GetRightTile() {
		return GetTileInDirection(RelativeDirection.RIGHT);
	}
	
	private Tile GetFrontTile() {
		return GetTileInDirection(RelativeDirection.FORWARD);
	}
	
	private Tile GetBackTile() {
		return GetTileInDirection(RelativeDirection.BACKWARD);
	}
	
	private Tile GetTileInDirection(RelativeDirection direction){
		return currentTile.GetNeighbour(GetCardinalDirectionAtRelativeDirection(direction));
	}
	
	private Direction GetCardinalDirectionAtRelativeDirection(RelativeDirection direction){
		switch (direction){
		case RelativeDirection.LEFT:		return GetLeftCardinalDirection();
		case RelativeDirection.RIGHT:		return GetRightCardinalDirection();
		case RelativeDirection.FORWARD:		return GetCardinalDirection();
		case RelativeDirection.BACKWARD:	return GetBackwardCardinalDirection();
		default:							throw new FacingHelper.InvalidDirectionException();
		}
	}
	
	private Direction GetLeftCardinalDirection(){
        return Utils.TurnLeft(GetCardinalDirection());
	}
	
	private Direction GetRightCardinalDirection(){
        return Utils.TurnRight(GetCardinalDirection());
    }
	
	private Direction GetBackwardCardinalDirection(){
        return Utils.Reverse(GetCardinalDirection());
	}

    private Direction GetCardinalDirection()
    {
        return FacingHelper.GetFacingDirection(transform);
    }
	
}
