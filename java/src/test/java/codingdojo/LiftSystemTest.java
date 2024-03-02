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
        liftSystem.tick();

        // Assert
        verify(new LiftSystemPrinter().print(liftSystem));
    }
}
