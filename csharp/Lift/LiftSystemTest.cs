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
        public void Initialises_WithTwoFloors_AndSingleLift()
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
        public void IdleLiftWithNoRequest_AfterTick_StaysStill()
        {
            // Arrange
            var liftA = new Lift("A", 0);
            var lifts = new List<Lift> { liftA };
            var floors = new List<int> { 0, 1 };
            var calls = new List<Call>();
            var liftSystem = new LiftSystem(floors, lifts, calls);
            var printer = new LiftSystemPrinter();
            var liftSystemOutput = printer.Print(liftSystem);
            
            // Act
            liftSystemOutput += TickAndReturnOutput(printer, liftSystem);
            
            // Assert
            Approvals.Verify(liftSystemOutput);
        }
        
        [Fact]
        public void IdleLiftWithRequest_AfterTick_MovesToRequestedFloor()
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
            
            // Assert
            Approvals.Verify(liftSystemOutput);
        }
        
        [Fact]
        public void ManyLiftsWithRequests_AfterTick_MoveToRequestedFloors()
        {
            // Arrange
            var liftA = new Lift("A", 0, new List<int>(){1});
            var liftB = new Lift("B", 0, new List<int>(){2});
            var liftC = new Lift("C", 1, new List<int>(){3});
            var lifts = new List<Lift> { liftA, liftB, liftC };
            var floors = new List<int> { 0, 1, 2, 3 };
            var calls = new List<Call>();
            var liftSystem = new LiftSystem(floors, lifts, calls);
            var printer = new LiftSystemPrinter();
            var liftSystemOutput = printer.Print(liftSystem);
            
            // Act
            liftSystemOutput += TickAndReturnOutput(printer, liftSystem);
            
            // Assert
            Approvals.Verify(liftSystemOutput);
        }
        
        [Fact]
        public void IdleLift_ThatMovedToFloorAfterTick_OpensDoorsAndClearsRequest()
        {
            // Arrange
            int requestFloor = 1;
            var liftA = new Lift("A", requestFloor, new List<int>(){requestFloor});
            var lifts = new List<Lift> { liftA };
            var floors = new List<int> { 0, 1 };
            var calls = new List<Call>();
            var liftSystem = new LiftSystem(floors, lifts, calls);
            var printer = new LiftSystemPrinter();
            var liftSystemOutput = printer.Print(liftSystem);
            
            // Act
            liftSystemOutput += TickAndReturnOutput(printer, liftSystem);
            
            // Assert
            Approvals.Verify(liftSystemOutput);
        }
        
        [Fact]
        public void LiftWithRequest_AfterTwoTicks_MovesToFloor_ClearsRequest_AndOpensDoors()
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
        
        [Fact]
        public void LiftThatFinishedRequest_IfNoNewRequest_ClosesDoors()
        {
            // Arrange
            var liftA = new Lift("A", 1, new List<int>(), doorsOpen: true);
            var lifts = new List<Lift> { liftA };
            var floors = new List<int> { 0, 1 };
            var calls = new List<Call>();
            var liftSystem = new LiftSystem(floors, lifts, calls);
            var printer = new LiftSystemPrinter();
            var liftSystemOutput = printer.Print(liftSystem);
            
            // Act
            liftSystemOutput += TickAndReturnOutput(printer, liftSystem);
            
            // Assert
            Approvals.Verify(liftSystemOutput);
        }
        
        [Fact]
        public void Lift_FulfilsRequest_OpensDoors_ClearsRequest_AndClosesDoors()
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
            liftSystemOutput += TickAndReturnOutput(printer, liftSystem);
            
            // Assert
            Approvals.Verify(liftSystemOutput);
        }
        
        [Fact]
        public void ManyLifts_FulfilRequests_OpenDoors_ClearRequests_AndCloseDoors()
        {
            // Arrange
            var liftA = new Lift("A", 0, new List<int>(){3});
            var liftB = new Lift("B", 0, new List<int>(){1});
            var liftC = new Lift("C", 0, new List<int>(){4});
            var lifts = new List<Lift> { liftA, liftB, liftC };
            var floors = new List<int> { 0, 1, 2, 3, 4 };
            var calls = new List<Call>();
            var liftSystem = new LiftSystem(floors, lifts, calls);
            var printer = new LiftSystemPrinter();
            var liftSystemOutput = printer.Print(liftSystem);
            
            // Act
            liftSystemOutput += TickAndReturnOutput(printer, liftSystem);
            liftSystemOutput += TickAndReturnOutput(printer, liftSystem);
            liftSystemOutput += TickAndReturnOutput(printer, liftSystem);
            
            // Assert
            Approvals.Verify(liftSystemOutput);
        }
        
        [Fact]
        public void EmptyLift_MovesTowardsAWaitingPerson()
        {
            // Arrange
            var liftA = new Lift("A", 1);
            var lifts = new List<Lift> { liftA };
            var floors = new List<int> { 0, 1 };
            var calls = new List<Call>{new Call(0, Direction.Up)};
            var liftSystem = new LiftSystem(floors, lifts, calls);
            var printer = new LiftSystemPrinter();
            var liftSystemOutput = printer.Print(liftSystem);
            
            // Act
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