using Spine;
using Spine.Unity;
using StructureElements;
using System;
using UnityEngine;
using UnityEngine.U2D;
using ArrowControl;

namespace ArcherControl
{
    public class ArcherView : View
    {
        [SpineBone(dataField: "skeletonAnimation")]
        [SerializeField] private string _bowTiltBoneName;
        [SpineBone(dataField: "skeletonAnimation")]
        [SerializeField] private string _arrowBoneName;
        [SpineAnimation, SerializeField] private string _aimingAnimation;
        [SpineAnimation, SerializeField] private string _idleAnimation;
        [SpineAnimation, SerializeField] private string _attackFinishAnimation;
        [SerializeField] private SkeletonAnimation _skeletonAnimation;
        [SerializeField] private SpriteShapeController _spriteShapeController;
        
        private ArrowTrajectory _arrowTrajectory;
        private Vector2 _pullingVector;
        private Bone _bowTiltBone;
        private Bone _arrowBone;
        private float _shootingForce = 3f;

        public Action<Vector2, Vector2> ShootingArrow;

        private void Awake()
        {
            _bowTiltBone = _skeletonAnimation.Skeleton.FindBone(_bowTiltBoneName);
            _arrowBone = _skeletonAnimation.Skeleton.FindBone(_arrowBoneName);
            _arrowTrajectory = new ArrowTrajectory(_spriteShapeController);
        }

        public void StartPullingArrow()
        {
            _skeletonAnimation.AnimationState.SetAnimation(1, _aimingAnimation, false);
        }

        public void PullArrow(Vector2 mousePosition)
        {
            Vector2 pullingVector = (Vector2)_skeletonAnimation.transform.InverseTransformPoint(mousePosition) -
                _arrowBone.GetSkeletonSpacePosition();

            if (pullingVector.x > -0.5f)
                return;

            _pullingVector = pullingVector;

            Vector2 arrowWorldPosition = _arrowBone.GetWorldPosition(_skeletonAnimation.transform);

            _bowTiltBone.Rotation = Vector2.SignedAngle(Vector2.left, _pullingVector);

            _spriteShapeController.transform.position = arrowWorldPosition;
            _arrowTrajectory.Update(-_pullingVector * _shootingForce);

            Debug.DrawRay(arrowWorldPosition, mousePosition - arrowWorldPosition, Color.red);
        }

        public void ShootArrow()
        {
            _spriteShapeController.gameObject.SetActive(false);

            _skeletonAnimation.AnimationState.SetAnimation(1, _attackFinishAnimation, false);
            _skeletonAnimation.AnimationState.AddAnimation(1, _idleAnimation, true, 0.3f);

            ShootingArrow?.Invoke(
                -_pullingVector * _shootingForce,
                _arrowBone.GetWorldPosition(_skeletonAnimation.transform));
        }
    }
}
