using System.Collections;

namespace Mistix{
    public abstract class AIA_Action{
        public AIA_Action(AIActor actor){ _actor = actor; }
        protected AIActor _actor;
        public abstract void StartActionRoutine();
        public abstract IEnumerator ActionRoutine();
    }
}