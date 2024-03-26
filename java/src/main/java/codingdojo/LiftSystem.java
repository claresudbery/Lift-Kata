package codingdojo;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.stream.Collectors;

public class LiftSystem {
    private final List<Integer> floors;
    private final List<Call> calls;
    private final List<Lift> lifts;

    public LiftSystem(List<Integer> floors, List<Lift> lifts, List<Call> calls) {
        this.floors = floors;
        this.calls = calls;
        this.lifts = lifts;
    }

    public List<Integer> getFloorsInDescendingOrder() {
        List<Integer> shallowCopy = new ArrayList<>(floors);
        Collections.reverse(shallowCopy);
        return shallowCopy;
    }

    public List<Call> getCallsForFloor(int floor) {
        return calls.stream().filter(c -> c.getFloor() == floor).collect(Collectors.toList());
    }

    public List<Lift> getLifts() {
        return lifts;
    }

    public void tick() {
        int test = lifts.size();
        for (Lift lift : lifts) {
            lift.fulFilRequestsOrCloseDoors();
            respondToCalls(lift);
        }
    }

    private void respondToCalls(Lift lift) {
        if (calls.size() > 0) {
            if (lift.getFloor() != calls.get(0).getFloor()) {
                lift.moveTo(calls.get(0).getFloor());
            }
        }
    }

    public void tickFulfilRequestsAndCalls() {
        int test = lifts.size();
        for (Lift lift : lifts) {
            lift.fulFilRequestsOrCloseDoors();
            if (calls.size() > 0) {
                if (lift.getFloor() != calls.get(0).getFloor()) {
                    lift.moveTo(calls.get(0).getFloor());
                }
            }
        }
    }

    public void tickOpenDoorsAndClearRequestAndCloseDoors() {
        int test = lifts.size();
        for (Lift lift : lifts) {
            if (lift.requests.size() > 0) {
                lift.fulfilRequests();
            } else {
                lift.closeDoors();
            }
        }
    }

    public void tickOpenDoorsAndClearRequest() {
        int test = lifts.size();
        for (Lift lift : lifts) {
            if (lift.requests.size() > 0) {
                var request = lift.requests.get(0);
                if (lift.getFloor() != request) {
                    lift.moveTo(request);
                } else {
                    lift.requests.remove(0);
                    lift.openDoors();
                }
            }
        }
    }

    public void tickManyLiftsMoveToFloor() {
        int test = lifts.size();
        for (Lift lift : lifts) {
            if (lift.requests.size() > 0) {
                var request = lift.requests.get(0);
                lift.moveTo(request);
            }
        }
    }

    public void tickMoveToFloor() {
        int test = lifts.size();
        Lift lift = lifts.get(0);
        if (lift.requests.size() > 0) {
            var request = lift.requests.get(0);
            lift.moveTo(request);
        }
    }

    public void tickUnimplemented() {
        // TODO: implement this method
    }
}
