import { Seat } from '@type/seat';
import { Box, Button } from '@mui/material';
import { SetStateAction, Dispatch } from 'react';
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

  const renderBookingButton = (label: string, onclickHandler: (e: React.MouseEvent<HTMLButtonElement>) => void) => {

    // Green background + 0.5 backgorund opacity
    const occupiedStyle = {
      // Need !important in order to overwrite the styles applied by MUI to button elements
      background: "rgba(97, 197, 119, 0.5) !important"
    } as const

    // Green background
    const availableStyle = {
      background: "rgb(97, 197, 119)",
      '&:hover': {
        background: "rgba(97, 197, 119, 0.50)"
      }
    } as const

    // Red background
    const unbookStyle = {
      background: "rgb(223, 13, 13)",
      '&:hover': {
        background: "rgba(223, 13, 13, 0.50)"
      }
    } as const

    return <Button
      variant='contained'
      onClick={onclickHandler}
      className="w-full rounded-lg p-2"
      disabled={seatInfo === "bookedSeat"}
      sx={{
        ...(seatInfo === "bookedSeat" && occupiedStyle),
        ...(seatInfo === "bookAvailableSeat" && availableStyle),
        ...(seatInfo === "removeBookedSeat" && unbookStyle)
      }}>
      <p className='text-base m-2 text-white'>{label}</p>
    </Button>
  }

  const SeatInfoOccupied = () => {
    return (
      <>
        <div className='w-full bg-slate-400 bg-opacity-10 text-center flex flex-col p-2 md:grow'>
          <p className='p-3 rounded-lg text-sm truncate'>Denne pulten er allerede booket av:</p>
          <p>{data?.name}</p>
        </div>
        {renderBookingButton("Send booking", handleBook)}
      </>
    );
  };

  const SeatInfoUnbook = () => {
    return (
      <>
        <div className='w-full bg-slate-400 bg-opacity-10 text-center md:grow'>
          <p className='p-3 rounded-lg text-sm truncate md:text-lg'>Du har allerede booket denne pulten</p>
        </div>
        {renderBookingButton("Fjern booking", handleUnbook)}
      </>
    );
  };

  const SeatInfoAvailable = () => {
    return (
      <>
        <div className='w-full bg-slate-400 bg-opacity-10 text-center md:grow'>
          <p className='p-3 rounded-lg text-sm truncate md:text-lg'>Pulten er ledig</p>
        </div>
        {renderBookingButton("Send booking", handleBook)}
      </>
    );
  };

  return (
    <div className='w-full h-full'>
      <Box className='p-2 flex flex-col w-full h-full'>
        <div className='flex flex-col p-2 gap-y-4 w-[90%] mx-auto h-full'>
          {seatInfo === 'bookAvailableSeat' && <SeatInfoAvailable />}
          {seatInfo === 'removeBookedSeat' && <SeatInfoUnbook />}
          {seatInfo === 'bookedSeat' && <SeatInfoOccupied />}
        </div>
      </Box>
    </div>
  );
};
