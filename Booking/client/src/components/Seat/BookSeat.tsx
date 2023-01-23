import { Seat } from '@type/seat';
import { Box, Button } from '@mui/material';
import React, { SetStateAction, Dispatch, useEffect } from 'react';
import { useBookingMutation, CreateBooking } from '@api/useBookingMutation';
import { Dayjs } from 'dayjs';
import { DeleteBooking, useRemoveBookingMutation } from '@api/useRemoveBookingMutation';
import { User } from '@type/user';
import Snackbar from '@mui/material/Snackbar';
import MuiAlert, { AlertProps } from '@mui/material/Alert';

type TBookSeat = {
  seat: Seat;
  date: Dayjs;
  data: User;
  seatInfo: string;
  onSeatInfoChange: Dispatch<SetStateAction<string>>;
};

export const BookSeat = ({ seat, date, data, seatInfo, onSeatInfoChange }: TBookSeat) => {
  const [state, setState] = React.useState({
    open: false,
    message: ''
  });
  const { open, message } = state;
  const bookingMutation = useBookingMutation();
  const removeBookingMutation = useRemoveBookingMutation();

  useEffect(() => {
  }, [data, seatInfo]);

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
        setState({
          message: 'Bookingen er nå registrert',
          open: true,
        });
      },
      onError() {
        setState({
          message: 'Kunne ikke reservere bookingen',
          open: true,
        });
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
        setState({
          message: 'Bookingen er nå fjernet',
          open: true,
        });
      },
      onError() {
        setState({
          message: 'Kunne ikke fjerne bookingen',
          open: true,
        });
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

  const Alert = React.forwardRef<HTMLDivElement, AlertProps>(function Alert(
    props,
    ref,
  ) {
    return <MuiAlert elevation={6} ref={ref} variant="filled" {...props} />;
  });

  const handleClose = (event?: React.SyntheticEvent | Event) => {
    setState({
      ...state,
      open: false,
    });
  }

  return (
    <div className='w-full h-full'>
      <Box className='p-2 flex flex-col w-full h-full'>
        <div className='flex flex-col p-2 gap-y-4 w-[90%] mx-auto h-full'>
          {seatInfo === 'bookAvailableSeat' && <SeatInfoAvailable />}
          {seatInfo === 'removeBookedSeat' && <SeatInfoUnbook />}
          {seatInfo === 'bookedSeat' && <SeatInfoOccupied />}
        </div>
      </Box>
      <Snackbar open={open} autoHideDuration={6000} onClose={handleClose} anchorOrigin={{ vertical: 'bottom', horizontal: 'center' }}>
        <Alert onClose={handleClose} severity="success" sx={{ width: '100%' }}>
          {message}
        </Alert>
      </Snackbar>
    </div>
  );
};
