package codingdojo;

import org.junit.jupiter.api.Test;

import java.util.Arrays;
import java.util.Collections;
import java.util.List;

import static org.approvaltests.Approvals.verify;

public class LiftSystemTest {

    @Test
    public void initialises_withTwoFloors_andSingleLift_asSpecified() {
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
    public void idleLift_fulfilsRequest_inTwoTicks() {
        // Arrange
        List<Integer> requests = Arrays.asList(1);
        Lift liftA = new Lift("A", 0, requests);
        List<Lift> lifts = Collections.singletonList(liftA);
        List<Integer> floors = Arrays.asList(0, 1);
        List<Call> calls = Collections.emptyList();
        LiftSystem liftSystem = new LiftSystem(floors, lifts, calls);
        LiftSystemPrinter liftSystemPrinter = new LiftSystemPrinter();
        String liftSystemOutput = liftSystemPrinter.print(liftSystem);

        // Act
        liftSystemOutput = tickAndReturnoutput(liftSystem, liftSystemOutput, liftSystemPrinter);
        liftSystemOutput = tickAndReturnoutput(liftSystem, liftSystemOutput, liftSystemPrinter);

        // Assert
        verify(liftSystemOutput);
    }

    private static String tickAndReturnoutput(LiftSystem liftSystem, String liftSystemOutput, LiftSystemPrinter liftSystemPrinter) {
        liftSystem.tick();
        liftSystemOutput += "...\n" + liftSystemPrinter.print(liftSystem);
        return liftSystemOutput;
    }
}
