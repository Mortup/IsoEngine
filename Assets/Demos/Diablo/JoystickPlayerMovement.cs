using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.mortup.iso.pathfinding;

namespace com.mortup.iso.demo.diablo {

    public class JoystickPlayerMovement : MonoBehaviour {

        [SerializeField] private bool useJoystickMovement = false;
        [SerializeField] private bool useClickMovement = false;
        [SerializeField] private bool wander = false;

        [SerializeField] private Level level;
        [SerializeField] private Transform feetPosition;
        [SerializeField] private List<Animator> animators;
        [SerializeField] private float speed;

        private Vector2Int currentTarget;
        private List<Vector2Int> currentPath;

        private float timeToNextWander = 0f;

        private Vector2 previousDirection = Vector2.down;
        private Transform followTarget = null;

        private bool isDead = false;

        public void Init(Level level) {
            this.level = level;
        }

        void Update() {
            if (isDead)
                return;

            if (wander && (currentPath == null || currentPath.Count == 0)) {
                if (timeToNextWander > 0) {
                    timeToNextWander -= Time.deltaTime;
                }
                else {
                    timeToNextWander = Random.value * 5f;

                    Vector2Int coords = level.transformer.WorldToTile(feetPosition.position);
                    Vector2Int randomTarget = coords + new Vector2Int(Random.Range(-10, 10), Random.Range(-10, 10));

                    int attempts = 0;
                    while (PathCalculator.FindPath(level, coords, randomTarget, true).Count == 0 && attempts < 120) {
                        randomTarget = coords + new Vector2Int(Random.Range(-10, 10), Random.Range(-10, 10));
                        attempts++;
                    }

                    if (attempts < 120) {
                        GoTo(randomTarget);
                    }
                    else {
                        Debug.Log("Path search failed");
                        wander = false;
                    }
                }
            }

            if (Input.GetMouseButtonDown(0) && useClickMovement) {
                GoTo(level.transformer.ScreenToTile(Input.mousePosition));
            }

            if (followTarget != null) {
                GoTo(level.transformer.WorldToTile(followTarget.position));
            }

            Vector2 input = useJoystickMovement ? new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) : Vector2.zero;

            // Update path if already close to target.
            if (currentPath != null && currentPath.Count > 0) {
                Vector2Int inmediateTarget = currentPath[0];
                Vector2Int coords = level.transformer.WorldToTile(feetPosition.position);

                if (currentPath.Count > 1) {
                    if (inmediateTarget == coords) {
                        currentPath.RemoveAt(0);
                    }
                }
                else {
                    if (((level.transformer.TileToWorld(inmediateTarget) + 0.5f * Vector2.right) - (Vector2)feetPosition.position).magnitude < 0.05f) {
                        currentPath = null;
                        followTarget = null;
                    }
                    else if (followTarget != null && followTarget.gameObject.CompareTag("Enemy")) {
                        if (inmediateTarget == coords) {
                            AnimateHit();
                            followTarget.gameObject.GetComponent<JoystickPlayerMovement>().AnimateHit();
                            currentPath = null;
                            followTarget = null;
                        }
                    }
                }
            }

            // Force input to follow path.
            if (currentPath != null && currentPath.Count > 0) {
                Vector2Int inmediateTarget = currentPath[0];
                Vector2 inmediateWorldTarget = level.transformer.TileToWorld(inmediateTarget) + 0.5f * Vector2.right;

                input = (inmediateWorldTarget - (Vector2)feetPosition.position).normalized;
            }

            UpdateAnimators(input);
            transform.position = transform.position + (Vector3)(input * speed * Time.deltaTime);            
        }

        public void SetEnemyTarget(Transform target) {
            followTarget = target;
        }

        public void GoTo(Vector2Int targetPos) {
            currentTarget = targetPos;

            Vector2Int tilePos = level.transformer.WorldToTile(feetPosition.position);
            currentPath = PathCalculator.FindPath(level, tilePos, currentTarget, true);
        }

        private void UpdateAnimators(Vector2 input) {
            foreach (Animator a in animators) {
                float inputMagnitude = input.magnitude;
                a.SetFloat("velocity", inputMagnitude);

                if (inputMagnitude > 0.25f) {
                    a.SetFloat("velX", input.x);
                    a.SetFloat("velY", input.y);

                    previousDirection = input;
                }
                else {
                    a.SetFloat("velX", previousDirection.x);
                    a.SetFloat("velY", previousDirection.y);
                }

                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0)) {
                    a.SetTrigger("hit");
                }
            }
        }

        public void AnimateHit() {
            foreach (Animator a in animators) {
                a.SetTrigger("hit");
            }

            if (wander) {
                isDead = true;
                Destroy(gameObject, 0.4f);
            }
        }
    }

}