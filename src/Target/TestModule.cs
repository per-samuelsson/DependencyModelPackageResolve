
using Ninject.Modules;
using System;

namespace Target {
  
    // Force reference to be emitted
    public class TestModule : NinjectModule {
        public override void Load() {
            throw new NotImplementedException();
        }
    }
}
