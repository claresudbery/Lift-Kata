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
        public void Initialises_WithTwoFloors_AndSingleLift_AsSpecified()
        {
            // Arrange
            var liftA = new Lift("A", 0);
            var lifts = new List<Lift> { liftA };
            var floors = new List<int>() { 0, 1 };
            var calls = new List<Call>();
            
            // Act
            var liftSystem = new LiftSystem(floors, lifts, calls);
            
            // Assert
            Approvals.Verify(new LiftSystemPrinter().Print(liftSystem));
        }
    }
}