import { CircularProgress, Button, Container } from "@mui/material";
import React, { useState } from "react";
import { GetSeats } from "../../seats/components/GetSeats";
import { useRooms } from "../api/getRooms";
import { Rooms } from "../types";

const Room = () => {
  const { isLoading, data, error } = useRooms();

  const capacityHandler = () => {
    // total kapasitet
    // skal minimere hvis noen har booket den dagen
  };

  if (isLoading) {
    return <CircularProgress size={100} />;
  }

  if (error) {
    return <h4>No Rooms Found</h4>;
  }

  return (
    <>
      {data?.map((rooms) => (
        <Container>
          <h1>{rooms.name}</h1>
          <p>Kapasitet: {rooms.capacity}</p>
        </Container>
      ))}{" "}
    </>
  );
};

export default Room;
