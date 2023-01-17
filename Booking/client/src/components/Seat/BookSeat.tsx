import { Seat } from '@type/seat';
import { Box, Button } from '@mui/material';
import { SetStateAction, Dispatch, useEffect } from 'react';
import { useBookingMutation, CreateBooking } from '@api/useBookingMutation';
import { Dayjs } from 'dayjs';
import { DeleteBooking, useRemoveBookingMutation } from '@api/useRemoveBookingMutation';
import { User } from '@type/user';

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
      <div className='flex flex-col p-2 gap-y-4 w-[90%] mx-auto'>
        <div className='w-full bg-slate-400 bg-opacity-10 text-center flex flex-col'>
          <p className='p-3 rounded-lg text-sm truncate'>Denne pulten er allerede booket av:</p>
          <p>{data?.name}</p>
        </div>
        <Button disabled variant='contained' onClick={handleUnbook} className="w-full rounded-lg bg-[#61C577] p-2 bg-opacity-50">
          <p className='text-base m-2 text-white'>Send booking</p>
        </Button>
      </div>
    );
  };

  const SeatInfoUnbook = () => {
    return (
      <div className='flex flex-col p-2 gap-y-4 w-[90%] mx-auto'>
        <div className='w-full bg-slate-400 bg-opacity-10 text-center'>
          <p className='p-3 rounded-lg text-sm truncate'>Du har allerede booket denne pulten</p>
        </div>
        <Button variant='contained' onClick={handleUnbook} className="w-full rounded-lg bg-[#DF0D0D] p-2">
          <p className='text-base m-2'>Fjern booking</p>
        </Button>
      </div>
    );
  };

  const SeatInfoAvailable = () => {
    return (
      <div className='flex flex-col p-2 gap-y-4 w-[90%] mx-auto'>
        <div className='w-full bg-slate-400 bg-opacity-10 text-center'>
          <p className='p-3 rounded-lg text-sm'>Pulten er ledig</p>
        </div>
        <Button variant='contained' onClick={handleBook} className="w-full rounded-lg bg-[#61C577] p-2">
          <p className='text-base m-2'>Send booking</p>
        </Button>
      </div>
    );
  };

  return (
    <div>
      <Box className='p-2'>
        {seatInfo === 'bookAvailableSeat' && <SeatInfoAvailable />}
        {seatInfo === 'removeBookedSeat' && <SeatInfoUnbook />}
        {seatInfo === 'bookedSeat' && <SeatInfoOccupied />}
      </Box>
    </div>
  );
};
