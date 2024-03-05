from approvaltests import verify

from lift import Lift, LiftSystem
from lift_printers import print_lifts


def test_initialises_with_two_floors_and_single_lift():
    # Arrange
    lift_a = Lift("A", 0)

    # Act
    lifts = LiftSystem(floors=[0, 1], lifts=[lift_a])

    # Assert
    verify(print_lifts(lifts))
