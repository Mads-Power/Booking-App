import { Seat } from '@type/seat';
import Chair from '@assets/chair.png';
import { Box, Button, CircularProgress, Modal, Typography } from '@mui/material';
import { SetStateAction, Dispatch, useState } from 'react';
import { useBookingMutation, CreateBooking } from '@api/useBookingMutation';
import { Dayjs } from 'dayjs';
import { Booking } from '@type/booking';
import { DeleteBooking, useRemoveBookingMutation } from '@api/useRemoveBookingMutation';
import { useUserQuery } from '@api/useUserQuery';
import { isSameDay } from 'date-fns';
import { User } from '@type/user';
import { Container } from '@mui/system';

const modalStyle = {
  position: 'absolute' as 'absolute',
  top: '50%',
  left: '50%',
  transform: 'translate(-50%, -50%)',
  width: '60vw',
  bgcolor: 'background.paper',
  border: '2px solid #000',
  borderRadius: 5,
  boxShadow: 24,
  p: 4,
};

const boxStyle = {
  border: 'solid',
  borderRadius: '5px',
  padding: '30px 20px',
};

type TBookSeat = {
  seat: Seat;
  date: Dayjs;
  data: User;
  seatInfo: string;
  onSeatInfoChange: Dispatch<SetStateAction<string>>;
};

export const BookSeat = ({ seat, date, data, seatInfo, onSeatInfoChange }: TBookSeat) => {
  // user occupying seat

  const bookingMutation = useBookingMutation();
  const removeBookingMutation = useRemoveBookingMutation();

  const handleBook = (e: React.MouseEvent<HTMLButtonElement>) => {
    e.preventDefault();

    const bookingData = {
      seatId: seat?.id,
      userId: data?.id,
      date: date?.toISOString(), //"2023-01-06T12:00:00.000+01",
    } as CreateBooking;

    bookingMutation.mutate(bookingData, {
      onSuccess: () => {
        onSeatInfoChange('removeBookedSeat');
      },
    });
  };

  const handleUnbook = (e: React.MouseEvent<HTMLButtonElement>) => {
    e.preventDefault();
    const unbookingData = {
      userId: data?.id,
      date: date?.toISOString(), //"2023-01-06T12:00:00.000+01",
    } as DeleteBooking;
    removeBookingMutation.mutate(unbookingData, {
      onSuccess() {
        onSeatInfoChange('bookAvailableSeat');
      },
    });
  };

  const SeatInfoOccupied = () => {
    return (
      <div>
        <h3>Booket av: </h3>
        <div style={{ textAlign: 'left' }}>
          <h3>Bruker: {data?.name}</h3>
          <h3>Telefon: {data?.phoneNumber}</h3>
          <h3>Email: {data?.email}</h3>
        </div>
      </div>
    );
  };

  const SeatInfoUnbook = () => {
    return (
      <Button
        variant='contained'
        style={{ backgroundColor: '#DF8B0D' }}
        onClick={handleUnbook}>
        Fjern booking
      </Button>
    );
  };

  const SeatInfoAvailable = () => {
    return (
      <Button variant='contained' onClick={handleBook}>
        Book sete
      </Button>
    );
  };

  return (
    <div style={{ margin: '10px', textAlign: 'center' }}>
      <Box style={boxStyle}>
        <h3>Setenummer: {seat?.name}</h3>
        <h3>Romnummer: {seat?.roomId}</h3>
        <h3>Dato: {date?.toDate().toLocaleDateString()}</h3>
        {seatInfo === 'bookAvailableSeat' && <SeatInfoAvailable />}
        {seatInfo === 'removeBookedSeat' && <SeatInfoUnbook />}
        {seatInfo === 'bookedSeat' && <SeatInfoOccupied />}
      </Box>
    </div>
  );
};
