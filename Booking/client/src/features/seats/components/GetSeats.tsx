import React, { FC, useEffect, useState } from "react";
import { Room } from "../../../types";
import GetSeat from "./GetSeat";
import { roomOneOslo } from "../../../data/rooms";
import { useQuery } from "@tanstack/react-query";
import { fetchRoom } from "../api/getRooms.api";

const GetSeats = () => {
  const { isLoading, isError, data, error } = useQuery({
    queryKey: ["room"],
    queryFn: fetchRoom,
  });

  // const [activeCountries, setActiveCountries] = useState<Country[]>([]);

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
