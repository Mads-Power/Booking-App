import React, { FC } from "react";
import styled from "styled-components";
import { Seat } from "../../../types";

const Seats = styled.button`
  display: flex;
  justify-content: center;
  align-items: center;
  position: relative;
  gap: 1em;
  height: 5em;
  width: 5em;
  border-radius: 5px;
  margin-top: 1rem;
  margin-bottom: 1rem;
  color: white;
  background-color: #54a4d1;
  box-shadow: 2px 2px 2px 1px rgba(0, 0, 0, 0.2);

  &:hover {
    border: 2px solid blue;
  }
`;

// interface ISeatsProps {
//   getAllSeats: Seat[];
// }

const GetSeat: FC<Seat> = ({ seatId, id, isTaken }: Seat) => {
  return (
    <>
      <Seats>
        {seatId} {id} {isTaken}
      </Seats>
    </>
  );
};

export default GetSeat;
