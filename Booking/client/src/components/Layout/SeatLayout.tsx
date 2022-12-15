import React, { useState } from "react";
import { useSeat } from "../../features/seats/api/getSeat";
import {
  CreateBooking,
  useBook,
} from "../../features/bookings/api/putBookingBook";
import { CircularProgress, Button, Container, colors, TextField } from "@mui/material";
import { useParams } from "react-router-dom";
import Chari from "../../features/seats/components/chair.png";
import { Booking } from "../../features/bookings/types";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import { DatePicker, LocalizationProvider } from "@mui/x-date-pickers";
import dayjs, { Dayjs } from "dayjs";

const SeatLayout = () => {
  const mutation = useBook();
  let { seatId } = useParams();
  const { isLoading, data, error } = useSeat(seatId!);
  const [date, setDate] = React.useState<Dayjs | null>(dayjs(Date.now()))

  if (isLoading) {
    return <CircularProgress size={100} />;
  }

  if (error) {
    return <h4>No Rooms Found</h4>;
  }



  const handleBook = (e: React.MouseEvent<HTMLButtonElement>) => {
    e.preventDefault();
    const bookingData = {
      seatId: data?.id,
      userId: 1,
      date: date?.toISOString()//"2023-01-06T12:00:00.000+01",
    } as CreateBooking;
    mutation.mutate(bookingData);
  };

  return (
    <>
      <img src={Chari} />
      <h1>Seat name: {data?.name}</h1>
      <div>Room number {data?.roomId}</div>
      <LocalizationProvider dateAdapter={AdapterDayjs}>
        <DatePicker
          value={date}
          onChange={(newValue) => {
            setDate(newValue);
            console.log(newValue?.toISOString())
          }}
          renderInput={(params) => <TextField {...params} />}
        />
      </LocalizationProvider>
      <Button onClick={handleBook}>Book seat</Button>
      {data!.bookings.map((b) => (
        <div key={b.id}>Date: {b.date}</div>
      ))}
    </>
  );
};

export default SeatLayout;
