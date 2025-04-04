using Spine;
using Spine.Unity;
using StructureElements;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.U2D;

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
        private Vector2 _pullPointOffset = new Vector2(0.5f, 1.5f);
        private Coroutine _waitingCoroutine;

        public Action<Vector2, Vector2> ThrowingArrow;

        private void Awake()
        {
            _bowTiltBone = _skeletonAnimation.Skeleton.FindBone(_bowTiltBoneName);
            _arrowBone = _skeletonAnimation.Skeleton.FindBone(_arrowBoneName);
            _arrowTrajectory = new ArrowTrajectory(_spriteShapeController);
        }

        public void StartPullingArrow()
        {
            if (_waitingCoroutine != null)
                StopCoroutine(_waitingCoroutine);

            _skeletonAnimation.AnimationState.SetAnimation(1, _aimingAnimation, false);
        }

        public void PullArrowBone(Vector2 position)
        {
            _pullingVector = _skeletonAnimation.transform.InverseTransformPoint(position);
            _pullingVector -= _pullPointOffset;

            if (_pullingVector.x < -0.5f)
            {
                _bowTiltBone.Rotation = Vector2.SignedAngle(Vector2.left, _pullingVector);
                _spriteShapeController.transform.position = _arrowBone.GetWorldPosition(_skeletonAnimation.transform);
                _arrowTrajectory.Update(-_pullingVector * 3f);
            }
        }

        public void CanceledPulling()
        {
            _bowTiltBone.Rotation = 0f;
            _skeletonAnimation.AnimationState.SetAnimation(1, _attackFinishAnimation, false);
            _skeletonAnimation.AnimationState.AddAnimation(1, _idleAnimation, true, 1f);

            ThrowingArrow?.Invoke(-_pullingVector * 3f, _arrowBone.GetWorldPosition(_skeletonAnimation.transform));
            _spriteShapeController.gameObject.SetActive(false);
            _waitingCoroutine = StartCoroutine(WaitForThrowingAnimation());
        }

        private IEnumerator WaitForThrowingAnimation()
        {
            float waitingTime = 1f;
            float accumTime = 0f;

            WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

            while (accumTime < waitingTime)
            {
                accumTime += Time.fixedDeltaTime;
                _bowTiltBone.Rotation = Vector2.SignedAngle(Vector2.left, _pullingVector);

                yield return waitForFixedUpdate;
            }
        }
    }
}
