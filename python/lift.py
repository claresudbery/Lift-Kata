from enum import Enum

from attr import dataclass


class Direction(Enum):
    UP = 0
    DOWN = 1


@dataclass
class Call:
    floor: int
    direction: Direction


class Lift:

    def __init__(self, id, floor, doors_open=False, requested_floors=None):
        self.id = id
        self.floor = floor
        self.doors_open = doors_open
        self.requested_floors = list(requested_floors) if requested_floors else []

    def move_to(self, new_floor):
        self.floor = new_floor

    def fulfil_requests_or_close_doors(self):
        if self.requested_floors:
            self.fulfil_requests()
        else:
            self.doors_open = False

    def fulfil_requests(self):
        request = self.requested_floors[0]
        if self.floor != request:
            self.move_to(request)
        else:
            self.doors_open = True
            self.requested_floors = []


class LiftSystem:
    def __init__(self, floors=None, lifts=None, calls=None):
        self.floors = list(floors) if floors else []
        self.lifts = list(lifts) if lifts else []
        self.calls = list(calls) if calls else []

    def calls_for(self, floor):
        return [c for c in self.calls if c.floor == floor]

    def tick(self):
        for lift in self.lifts:
            lift.fulfil_requests_or_close_doors()
            self.respond_to_calls(lift)

    def respond_to_calls(self, lift):
        if len(self.calls) > 0:
            if lift.floor != self.calls[0].floor:
                lift.move_to(self.calls[0].floor)

    def tick_fulfil_requests_and_calls(self):
        for lift in self.lifts:
            lift.fulfil_requests_or_close_doors()
            if len(self.calls) > 0:
                if lift.floor != self.calls[0].floor:
                    lift.move_to(self.calls[0].floor)

    def tick_open_doors_and_clear_request_and_close_doors(self):
        for lift in self.lifts:
            if lift.requested_floors:
                lift.fulfil_requests()
            else:
                lift.doors_open = False

    def tick_open_doors_and_clear_request(self):
        for lift in self.lifts:
            if lift.requested_floors:
                request = lift.requested_floors[0]
                if lift.floor != request:
                    lift.move_to(request)
                else:
                    lift.doors_open = True
                    lift.requested_floors = []

    def tick_many_lifts_move_to_floor(self):
        for lift in self.lifts:
            if lift.requested_floors:
                request = lift.requested_floors[0]
                lift.move_to(request)

    def tick_move_to_floor(self):
        lift = self.lifts[0]
        if lift.requested_floors:
            request = lift.requested_floors[0]
            lift.move_to(request)

    def tick_unimplemented(self):
        # TODO: implement this method
        pass