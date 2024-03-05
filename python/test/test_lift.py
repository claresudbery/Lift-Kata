from approvaltests import verify

from lift import Lift, LiftSystem
from lift_printers import print_lifts


def test_initialises_with_two_floors_and_single_lift():
    #Arrange
    liftA = Lift("A", 0)

    #Act
    lifts = LiftSystem(floors=[0, 1], lifts=[liftA])

    #Assert
    verify(print_lifts(lifts))

def test_idle_lift_with_no_request_after_tick_stays_still():
    #Arrange
    liftA = Lift("A", 0)
    lift_system = LiftSystem(floors=[0, 1], lifts=[liftA])
    lift_system_output = print_lifts(lift_system)

    #Act
    lift_system_output += tick_and_return_output(lift_system)

    #Assert
    verify(lift_system_output)


def tick_and_return_output(lift_system):
    lift_system.tick()
    return "...\n" + print_lifts(lift_system)
