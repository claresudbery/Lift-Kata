package codingdojo;

import org.junit.jupiter.api.Test;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.List;

import static org.approvaltests.Approvals.verify;

public class LiftSystemTest {

    @Test
    public void initialises_withTwoFloors_andSingleLift() {
        // Arrange
        Lift liftA = new Lift("A", 0);
        List<Lift> lifts = Collections.singletonList(liftA);
        List<Integer> floors = Arrays.asList(0, 1);
        List<Call> calls = Collections.emptyList();

        // Act
        LiftSystem liftSystem = new LiftSystem(floors, lifts, calls);

        // Assert
        verify(new LiftSystemPrinter().print(liftSystem));
    }

    @Test
    public void idleLiftWithNoRequest_afterTick_staysStill() {
        // Arrange
        Lift liftA = new Lift("A", 0);
        List<Lift> lifts = Collections.singletonList(liftA);
        List<Integer> floors = Arrays.asList(0, 1);
        List<Call> calls = Collections.emptyList();
        LiftSystem liftSystem = new LiftSystem(floors, lifts, calls);
        LiftSystemPrinter printer = new LiftSystemPrinter();
        String liftSystemOutput = printer.print(liftSystem);

        // Act
        liftSystem.tickUnimplemented();
        liftSystemOutput += "...\n" + printer.print(liftSystem);

        // Assert
        verify(liftSystemOutput);
    }

    @Test
    public void idleLiftWithRequest_afterTick_movesToRequestedFloor() {
        // Arrange
        Lift liftA = new Lift("A", 0, new ArrayList<>(Arrays.asList(1)));
        List<Lift> lifts = Collections.singletonList(liftA);
        List<Integer> floors = Arrays.asList(0, 1);
        List<Call> calls = Collections.emptyList();
        LiftSystem liftSystem = new LiftSystem(floors, lifts, calls);
        LiftSystemPrinter printer = new LiftSystemPrinter();
        String liftSystemOutput = printer.print(liftSystem);

        // Act
        liftSystem.tickMoveToFloor();
        liftSystemOutput += "...\n" + printer.print(liftSystem);

        // Assert
        verify(liftSystemOutput);
    }

    @Test
    public void manyLiftsWithRequests_afterTick_moveToRequestedFloors() {
        // Arrange
        Lift liftA = new Lift("A", 0, new ArrayList<>(Arrays.asList(1)));
        Lift liftB = new Lift("B", 0, new ArrayList<>(Arrays.asList(2)));
        Lift liftC = new Lift("C", 1, new ArrayList<>(Arrays.asList(3)));
        List<Lift> lifts = Arrays.asList(liftA, liftB, liftC);
        List<Integer> floors = Arrays.asList(0, 1, 2, 3);
        List<Call> calls = Collections.emptyList();
        LiftSystem liftSystem = new LiftSystem(floors, lifts, calls);
        LiftSystemPrinter printer = new LiftSystemPrinter();
        String liftSystemOutput = printer.print(liftSystem);

        // Act
        liftSystem.tickManyLiftsMoveToFloor();
        liftSystemOutput += "...\n" + printer.print(liftSystem);

        // Assert
        verify(liftSystemOutput);
    }

    @Test
    public void idleLift_thatMovedToFloorAfterTick_opensDoorsAndClearsRequest() {
        // Arrange
        int requestFloor = 1;
        Lift liftA = new Lift("A", requestFloor, new ArrayList<>(Arrays.asList(requestFloor)));
        List<Lift> lifts = Collections.singletonList(liftA);
        List<Integer> floors = Arrays.asList(0, 1);
        List<Call> calls = Collections.emptyList();
        LiftSystem liftSystem = new LiftSystem(floors, lifts, calls);
        LiftSystemPrinter printer = new LiftSystemPrinter();
        String liftSystemOutput = printer.print(liftSystem);

        // Act
        liftSystem.tickOpenDoorsAndClearRequest();
        liftSystemOutput += "...\n" + printer.print(liftSystem);

        // Assert
        verify(liftSystemOutput);
    }

    @Test
    public void liftWithRequest_afterTwoTicks_movesToFloor_clearsRequest_andOpensDoors() {
        // Arrange
        Lift liftA = new Lift("A", 0, new ArrayList<>(Arrays.asList(1)));
        List<Lift> lifts = Collections.singletonList(liftA);
        List<Integer> floors = Arrays.asList(0, 1);
        List<Call> calls = Collections.emptyList();
        LiftSystem liftSystem = new LiftSystem(floors, lifts, calls);
        LiftSystemPrinter printer = new LiftSystemPrinter();
        String liftSystemOutput = printer.print(liftSystem);

        // Act
        liftSystem.tickOpenDoorsAndClearRequest();
        liftSystemOutput += "...\n" + printer.print(liftSystem);
        liftSystem.tickOpenDoorsAndClearRequest();
        liftSystemOutput += "...\n" + printer.print(liftSystem);

        // Assert
        verify(liftSystemOutput);
    }

    @Test
    public void liftThatFinishedRequest_ifNoNewRequest_closesDoors() {
        // Arrange
        Lift liftA = new Lift("A", 1, new ArrayList<>(Arrays.asList()), true);
        List<Lift> lifts = Collections.singletonList(liftA);
        List<Integer> floors = Arrays.asList(0, 1);
        List<Call> calls = Collections.emptyList();
        LiftSystem liftSystem = new LiftSystem(floors, lifts, calls);
        LiftSystemPrinter printer = new LiftSystemPrinter();
        String liftSystemOutput = printer.print(liftSystem);

        // Act
        liftSystem.tickOpenDoorsAndClearRequestAndCloseDoors();
        liftSystemOutput += "...\n" + printer.print(liftSystem);

        // Assert
        verify(liftSystemOutput);
    }

    @Test
    public void lift_fulfilsRequest_opensDoors_clearsRequest_andClosesDoors() {
        // Arrange
        Lift liftA = new Lift("A", 0, new ArrayList<>(Arrays.asList(1)));
        List<Lift> lifts = Collections.singletonList(liftA);
        List<Integer> floors = Arrays.asList(0, 1);
        List<Call> calls = Collections.emptyList();
        LiftSystem liftSystem = new LiftSystem(floors, lifts, calls);
        LiftSystemPrinter printer = new LiftSystemPrinter();
        String liftSystemOutput = printer.print(liftSystem);

        // Act
        liftSystem.tickOpenDoorsAndClearRequestAndCloseDoors();
        liftSystemOutput += "...\n" + printer.print(liftSystem);
        liftSystem.tickOpenDoorsAndClearRequestAndCloseDoors();
        liftSystemOutput += "...\n" + printer.print(liftSystem);
        liftSystem.tickOpenDoorsAndClearRequestAndCloseDoors();
        liftSystemOutput += "...\n" + printer.print(liftSystem);

        // Assert
        verify(liftSystemOutput);
    }

    @Test
    public void manyLifts_fulfilRequests_openDoors_clearRequests_andCloseDoors() {
        // Arrange
        Lift liftA = new Lift("A", 0, new ArrayList<>(Arrays.asList(3)));
        Lift liftB = new Lift("B", 0, new ArrayList<>(Arrays.asList(1)));
        Lift liftC = new Lift("C", 0, new ArrayList<>(Arrays.asList(4)));
        List<Lift> lifts = Arrays.asList(liftA, liftB, liftC);
        List<Integer> floors = Arrays.asList(0, 1, 2, 3, 4);
        List<Call> calls = Collections.emptyList();
        LiftSystem liftSystem = new LiftSystem(floors, lifts, calls);
        LiftSystemPrinter printer = new LiftSystemPrinter();
        String liftSystemOutput = printer.print(liftSystem);

        // Act
        liftSystem.tickOpenDoorsAndClearRequestAndCloseDoors();
        liftSystemOutput += "...\n" + printer.print(liftSystem);
        liftSystem.tickOpenDoorsAndClearRequestAndCloseDoors();
        liftSystemOutput += "...\n" + printer.print(liftSystem);
        liftSystem.tickOpenDoorsAndClearRequestAndCloseDoors();
        liftSystemOutput += "...\n" + printer.print(liftSystem);

        // Assert
        verify(liftSystemOutput);
    }

    @Test
    public void emptyLift_movesTowardsAWaitingPerson() {
        // Arrange
        Lift liftA = new Lift("A", 1);
        List<Lift> lifts = Collections.singletonList(liftA);
        List<Integer> floors = Arrays.asList(0, 1);
        List<Call> calls = Arrays.asList(new Call(0, Direction.UP));
        LiftSystem liftSystem = new LiftSystem(floors, lifts, calls);
        LiftSystemPrinter printer = new LiftSystemPrinter();
        String liftSystemOutput = printer.print(liftSystem);

        // Act
        liftSystem.tickFulfilRequestsAndCalls();
        liftSystemOutput += "...\n" + printer.print(liftSystem);

        // Assert
        verify(liftSystemOutput);
    }

    private String tickAndReturnOutput(
            LiftSystemPrinter printer,
            LiftSystem liftSystem)
    {
        liftSystem.tick();
        return "...\n" + printer.print(liftSystem);
    }
}
