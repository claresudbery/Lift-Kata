from approvaltests import verify

from lift import Lift, LiftSystem, Call, Direction
from lift_printers import print_lifts


def test_initialises_with_two_floors_and_single_lift():
    # Arrange
    lift_a = Lift("A", 0)

    # Act
    lifts = LiftSystem(floors=[0, 1], lifts=[lift_a])

    # Assert
    verify(print_lifts(lifts))


def test_idle_lift_with_request_after_tick_moves_to_requested_floor():
    # Arrange
    lift_a = Lift("A", 0, requested_floors=[1])
    lift_system = LiftSystem(floors=[0, 1], lifts=[lift_a])
    lift_system_output = print_lifts(lift_system)

    # Act
    lift_system_output += tick_and_return_output(lift_system)

    # Assert
    verify(lift_system_output)


def test_idle_lift_that_moved_to_floor_after_tick_opens_doors_and_clears_request():
    # Arrange
    lift_a = Lift("A", 1, requested_floors=[1])
    lift_system = LiftSystem(floors=[0, 1], lifts=[lift_a])
    lift_system_output = print_lifts(lift_system)

    # Act
    lift_system_output += tick_and_return_output(lift_system)

    # Assert
    verify(lift_system_output)


def test_lift_that_finished_request_if_no_new_request_closes_doors():
    # Arrange
    lift_a = Lift("A", 1, doors_open=True)
    lift_system = LiftSystem(floors=[0, 1], lifts=[lift_a])
    lift_system_output = print_lifts(lift_system)

    # Act
    lift_system_output += tick_and_return_output(lift_system)

    # Assert
    verify(lift_system_output)


def test_empty_lift_moves_towards_a_waiting_person():
    # Arrange
    lift_a = Lift("A", 1)
    calls = [Call(0, Direction.UP)]
    lift_system = LiftSystem(floors=[0, 1], lifts=[lift_a], calls=calls)
    lift_system_output = print_lifts(lift_system)

    # Act
    lift_system_output += tick_and_return_output(lift_system)

    # Assert
    verify(lift_system_output)

def tick_and_return_output(lift_system):
    lift_system.tick()
    return "...\n" + print_lifts(lift_system)
