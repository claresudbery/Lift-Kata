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
            var floors = new List<int> { 0, 1 };
            var calls = new List<Call>();
            
            // Act
            var liftSystem = new LiftSystem(floors, lifts, calls);
            
            // Assert
            Approvals.Verify(new LiftSystemPrinter().Print(liftSystem));
        }
        [Fact]
        public void IdleLift_FulfilsRequest_InTwoTicks()
        {
            // Arrange
            var liftA = new Lift("A", 0, new List<int>(){1});
            var lifts = new List<Lift> { liftA };
            var floors = new List<int> { 0, 1 };
            var calls = new List<Call>();
            var liftSystem = new LiftSystem(floors, lifts, calls);
            var printer = new LiftSystemPrinter();
            var liftSystemOutput = printer.Print(liftSystem);
            
            // Act
            liftSystemOutput += TickAndReturnOutput(printer, liftSystem);
            liftSystemOutput += TickAndReturnOutput(printer, liftSystem);
            
            // Assert
            Approvals.Verify(liftSystemOutput);
        }

        private string TickAndReturnOutput(
                LiftSystemPrinter printer, 
                LiftSystem liftSystem)
        {
            liftSystem.Tick();
            return "...\n" + printer.Print(liftSystem);
        }
    }
}