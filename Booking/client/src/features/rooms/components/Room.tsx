import React from "react";
import { Container } from "../../../styles/GlobalStyles";
import GetSeat from "../../seats/components/GetSeat";
import GetSeats from "../../seats/components/GetSeats";
import { Seat } from "../../seats/types";
import { Room } from "../types";

interface IRoom {
  id: Room;
  seats: Seat[];
}

const Room = ({ id, seats }: Room) => {
  return <Container></Container>;
};

export default Room;
