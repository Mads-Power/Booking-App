import React, { FC, useEffect, useState } from "react";
import GetSeat from "./GetSeat";
import { useQuery } from "@tanstack/react-query";
import { fetchRoomOne } from "../api/getRooms.api";

const GetSeats = () => {
  const { isLoading, isError, data, error } = useQuery({
    queryKey: ["room"],
    queryFn: fetchRoomOne,
  });

  if (isLoading) return <div>Laster inn rooms</div>;

  if (isError) return <div>Noe gikk galt</div>;

  return (
    <div>
      {data.seats.map((item) => (
        <GetSeat
          key={item.id}
          id={item.id}
          seatId={item.seatId}
          isTaken={item.isTaken}
        />
      ))}
    </div>
  );
};

export default GetSeats;
