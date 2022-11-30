import React from "react";
import { Container } from "../../../styles/GlobalStyles";
import { GetSeats } from "../../seats/components/GetSeats";
import { Room } from "../types";

// interface IRoom {
//   id: Room;
//   seats: Seat[];
// }

const Room = () => {
  return (
    <Container>
      <GetSeats />
    </Container>
  );
};

export default Room;
