using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using Xunit;

namespace Lift
{
    [UseReporter(typeof(DiffReporter))]
    public class LiftSystemTest
    {
        [Fact]
        public void Initialises_WithTwoFloors_AndSingleLiftgcam _AsSpecified()
        {
            var liftA = new Lift("A", 0);
            var lifts = new LiftSystem(new List<int>(){0, 1}, new List<Lift>{liftA}, new List<Call>());
            lifts.Tick();
            Approvals.Verify(new LiftSystemPrinter().Print(lifts));
        }
    }
}