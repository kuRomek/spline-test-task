using Spine;
using Spine.Unity;
using StructureElements;
using UnityEngine;

namespace ArcherControl
{
    public class ArcherView : View
    {
        [SpineBone(dataField: "skeletonAnimation")]
        [SerializeField] private string _bowTiltBoneName;
        [SpineAnimation, SerializeField] private string _aimingAnimation;
        [SpineAnimation, SerializeField] private string _idleAnimation;
        [SerializeField] private SkeletonAnimation _skeletonAnimation;
        
        private Bone _bowTiltBone;
        private Vector2 _pullPointOffset = new Vector2(0.5f, 1.5f);

        private void Awake()
        {
            _bowTiltBone = _skeletonAnimation.Skeleton.FindBone(_bowTiltBoneName);
        }

        public void StartPullingArrow()
        {
            _skeletonAnimation.AnimationState.SetAnimation(1, _aimingAnimation, false);
        }

        public void PullArrowBone(Vector2 position)
        {
            Vector2 skeletonSpacePoint = _skeletonAnimation.transform.InverseTransformPoint(position);
            skeletonSpacePoint -= _pullPointOffset;

            if (skeletonSpacePoint.x < -0.5f)
                _bowTiltBone.Rotation = Vector2.SignedAngle(Vector2.left, skeletonSpacePoint);
        }

        public void CanceledPulling()
        {
            _bowTiltBone.Rotation = 0f;
            _skeletonAnimation.AnimationState.SetAnimation(1, _idleAnimation, true);
        }
    }
}
