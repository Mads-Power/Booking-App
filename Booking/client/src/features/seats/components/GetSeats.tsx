import React, { FC } from "react";
import { Seat } from "../types";
import GetSeat from "./GetSeat";

interface ISeatsProps {
  getAllSeats: Seat[];
}

const GetSeats: FC<ISeatsProps> = ({ getAllSeats }) => {
  return (
    <div>
      {getAllSeats.map((seats) => (
        <GetSeat seatId={seats.id} id={seats.seatId} isTaken={seats.isTaken} />
      ))}
    </div>
  );
};

export default GetSeats;
