using UnityEngine;

namespace VRMain.Assets.Code.GamePlay.NPC
{
    public class RobotWalker : MonoBehaviour
    {
        [Header("Path")]
        [SerializeField] private Transform _waypointHolder;
        [SerializeField] private float _moveSpeed = 2f;
        [SerializeField] private float _waypointThreshold = 2f;

        [Header("Leg Joints")]
        [SerializeField] private HingeJoint _leftHip;
        [SerializeField] private HingeJoint _rightHip;
        [SerializeField] private HingeJoint _leftKnee;
        [SerializeField] private HingeJoint _rightKnee;

        [Header("Arm Joints")]
        [SerializeField] private HingeJoint _leftShoulder;
        [SerializeField] private HingeJoint _rightShoulder;
        [SerializeField] private HingeJoint _leftElbow;
        [SerializeField] private HingeJoint _rightElbow;

        [Header("Walking Settings")]
        [SerializeField] private float _stepSpeed = 2f;
        [SerializeField] private float _hipSwingAngle = 15f;
        [SerializeField] private float _kneeBendAngle = 20f;
        [SerializeField] private float _armSwingAngle = 10f;
        [SerializeField] private float _motorForce = 100f;

        [SerializeField] private Rigidbody _torsoRigidbody;
        [SerializeField] private float _torsoBounce = 0.05f; // optional vertical bob
        private float _baseHeight;

        private int _currentWaypoint = 0;
        private float walkPhase = 0f;

        void Start()
        {
            if (_torsoRigidbody != null)
            {
                _baseHeight = _torsoRigidbody.position.y;
            }
        }

        void FixedUpdate()
        {
            MoveAlongPath();
            AnimateWalk();
        }

        void MoveAlongPath()
        {
            if (_waypointHolder.childCount == 0 || _torsoRigidbody == null)
            {
                return;
            }

            Transform waypoint = _waypointHolder.GetChild(_currentWaypoint);
            Vector3 dir = waypoint.position - _torsoRigidbody.position;
            dir.y = 0;

            Vector3 nextPos = _torsoRigidbody.position + dir.normalized * _moveSpeed * Time.fixedDeltaTime;

            nextPos.y = _baseHeight + Mathf.Sin(walkPhase * 2f) * _torsoBounce;
            _torsoRigidbody.MovePosition(nextPos);

            if (Vector3.Distance(_torsoRigidbody.position, waypoint.position) < _waypointThreshold)
            {
                _currentWaypoint = (_currentWaypoint + 1) % _waypointHolder.childCount;
            }

            if (dir.sqrMagnitude > 0.01f)
            {
                Quaternion targetRot = Quaternion.LookRotation(dir.normalized);
                _torsoRigidbody.MoveRotation(Quaternion.Slerp(_torsoRigidbody.rotation, targetRot, Time.fixedDeltaTime * 5f));
            }
        }

        void AnimateWalk()
        {
            walkPhase += Time.fixedDeltaTime * _stepSpeed;

            float hipTargetL = Mathf.Sin(walkPhase) * _hipSwingAngle;
            float hipTargetR = Mathf.Sin(walkPhase + Mathf.PI) * _hipSwingAngle;
            float kneeTargetL = Mathf.Max(0f, Mathf.Sin(walkPhase)) * _kneeBendAngle;
            float kneeTargetR = Mathf.Max(0f, Mathf.Sin(walkPhase + Mathf.PI)) * _kneeBendAngle;

            float shoulderTargetL = Mathf.Sin(walkPhase + Mathf.PI) * (_armSwingAngle * 2f);
            float shoulderTargetR = Mathf.Sin(walkPhase) * (_armSwingAngle * 2f);

            float elbowTargetL = Mathf.Max(0f, -Mathf.Sin(walkPhase + Mathf.PI)) * (_kneeBendAngle * 2f);
            float elbowTargetR = Mathf.Max(0f, -Mathf.Sin(walkPhase)) * (_kneeBendAngle * 2f);

            SetMotor(_leftHip, hipTargetL);
            SetMotor(_rightHip, hipTargetR);
            SetMotor(_leftKnee, kneeTargetL);
            SetMotor(_rightKnee, kneeTargetR);
            SetMotor(_leftShoulder, shoulderTargetL);
            SetMotor(_rightShoulder, shoulderTargetR);
            SetMotor(_leftElbow, elbowTargetL);
            SetMotor(_rightElbow, elbowTargetR);
        }


        void SetMotor(HingeJoint joint, float targetVelocity)
        {
            if (joint == null)
            {
                return;
            }

            JointMotor motor = joint.motor;
            motor.force = _motorForce;
            motor.targetVelocity = targetVelocity;
            joint.motor = motor;
            joint.useMotor = true;
        }
    }
}
