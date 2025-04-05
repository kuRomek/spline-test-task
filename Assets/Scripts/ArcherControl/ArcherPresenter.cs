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

        public void Enable()
        {
            if (Model != null)
            {
                Model.PullingStarted += View.StartPullingArrow;
                Model.PullingArrow += View.PullArrow;
                Model.PullingCanceled += View.ShootArrow;
                View.ShootingArrow += Model.ShootArrow;
            }
        }

        public void Disable()
        {
            if (Model != null)
            {
                Model.PullingStarted -= View.StartPullingArrow;
                Model.PullingArrow -= View.PullArrow;
                Model.PullingCanceled -= View.ShootArrow;
                View.ShootingArrow -= Model.ShootArrow;
            }
        }
    }
}
