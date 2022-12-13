import React from 'react';
import { useSeat } from '../../features/seats/api/getSeat';
import { CreateBooking, useBook } from '../../features/bookings/api/putBookingBook';
import { CircularProgress, Button, Container, colors } from '@mui/material';
import { useParams } from 'react-router-dom';
import Chari from '../../features/seats/components/chair.png';
import { Booking } from '../../features/bookings/types';

const SeatLayout = () => {
  const mutation = useBook();
  let { seatId } = useParams();
  const { isLoading, data, error } = useSeat(seatId!);
  

  if (isLoading) {
    return <CircularProgress size={100} />;
  }

  if (error) {
    return <h4>No Rooms Found</h4>;
  }
  const bookingData = {
    data: {
      seatId: data?.id,
      userId: 1,
      date: '2022-12-26T12:00:00.000+01',
    },
  } as CreateBooking;

  const handleBook = (e: React.MouseEvent<HTMLButtonElement>) => {
    e.preventDefault();
    mutation.mutate(bookingData);
    
  };

  return (
    <>
      <img src={Chari} />
      <h1>Seat name: {data?.name}</h1>
      <div>Room number {data?.roomId}</div>
      <Button onClick={handleBook}>Book seat</Button>
      {data!.bookings.map(b => (
        <div key={b.id}>Date: {b.date}</div>
      ))}
    </>
  );
};

export default SeatLayout;
