using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Responsible for the movement of the avatar in the right direction
/// </summary>
public class AvatarLocomotion : MonoBehaviour, ITerrainRequester {

    bool isUp = false, isDown = false, isLeft = false, isRight = false;

    [HideInInspector]
    public Transform leftBoundry, rightBoundry, upBoundry, downBoundry;
    public float avatarWidth = 0.3f;

    public void UpOn() {
        isUp = true;
    }

    public void UpOff() {
        isUp = false;
    }

    public void DownOn() {
        isDown = true;
    }

    public void DownOff() {
        isDown = false;
    }

    public void RightOn() {
        isRight = true;
    }

    public void RightOff() {
        isRight = false;
    }

    public void LeftOn() {
        isLeft = true;
    }

    public void LeftOff() {
        isLeft = false;
    }

    public float speed;

    bool isSet = false;

    void Update() {
        if (isSet == true) {
            Vector2 direction = getDirection();

            Vector2 velocity = getVelocity(direction);

            move(velocity);

            clamp();

        }
    }

    Vector2 getDirection() {
        Vector2 direction = Vector2.zero;

        if (isUp == true) {
            direction.y = 1;
        }

        if (isDown == true) {
            direction.y = -1;
        }

        if (isLeft == true) {
            direction.x = -1;
        }

        if (isRight == true) {
            direction.x = 1;
        }

        return direction;
    }

    Vector2 getVelocity(Vector2 direction) {
        direction = direction.normalized * speed * Time.deltaTime;

        return direction;
    }

    void move(Vector2 velocity) {
        transform.Translate(
            velocity.x,
            0,
            velocity.y,
            Space.World
        );

    }

    void clamp() {
        if (transform.position.x > rightBoundry.position.x - avatarWidth) {
            transform.SetX(rightBoundry.position.x - avatarWidth);
        }

        if (transform.position.x < leftBoundry.position.x + avatarWidth) {
            transform.SetX(leftBoundry.position.x + avatarWidth);
        }

        if (transform.position.z > upBoundry.position.z - avatarWidth) {
            transform.SetZ(upBoundry.position.z - avatarWidth);
        }

        if (transform.position.z < downBoundry.position.z + avatarWidth) {
            transform.SetZ(downBoundry.position.z + avatarWidth);
        }
    }


    public void SetTerrain(TerrainHolder terrain) {
        
        leftBoundry = terrain.leftBoundry;
        rightBoundry = terrain.rightBoundry;
        upBoundry = terrain.upBoundry;
        downBoundry = terrain.downBoundry;

        isSet = true;
    }
}
