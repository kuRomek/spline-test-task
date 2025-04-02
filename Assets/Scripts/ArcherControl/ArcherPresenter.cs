using StructureElements;

namespace ArcherControl
{
    public class ArcherPresenter : Presenter, IActivatable
    {
        public new Archer Model => base.Model as Archer;
        public new ArcherView View => base.View as ArcherView;

        private void Awake()
        {
            if (Model == null)
                enabled = false;
        }

        private void Start()
        {
            View.CanceledPulling();
        }

        public void Enable()
        {
            if (Model != null)
            {
                Model.PullingStarted += View.StartPullingArrow;
                Model.PullingArrow += View.PullArrowBone;
                Model.PullingCanceled += View.CanceledPulling;
            }
        }

        public void Disable()
        {
            if (Model != null)
            {
                Model.PullingStarted -= View.StartPullingArrow;
                Model.PullingArrow -= View.PullArrowBone;
                Model.PullingCanceled -= View.CanceledPulling;
            }
        }
    }
}
