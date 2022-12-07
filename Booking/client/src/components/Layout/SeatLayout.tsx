import React from "react";
import { useSeat } from "../../features/seats/api/getSeat";
import { CircularProgress, Button, Container } from "@mui/material";

const SeatLayout = () => {
  const { isLoading, data, error } = useSeat();

  if (isLoading) {
    return <CircularProgress size={100} />;
  }

  if (error) {
    return <h4>No Rooms Found</h4>;
  }

  return (
    <div>
      <p>Seat img</p>
      <p>favorite</p>
      <p>meta data</p>
      <p>date</p>
    </div>
  );
};

export default SeatLayout;
