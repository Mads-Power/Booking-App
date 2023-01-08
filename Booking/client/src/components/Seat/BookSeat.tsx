import { Seat } from '@type/seat';
import Chair from '@assets/chair.png';
import { Box, Button, CircularProgress, Modal, Typography } from '@mui/material';
import { useEffect, useState } from 'react';
import { useBook } from '@api/putBookingBook';
import { Dayjs } from 'dayjs';
import { Booking } from '@type/booking';
import { DeleteBooking, useUnbook } from '@api/putBookingUnbook';
import { useUser } from '@api/getUser';
import { useUserContext } from '@components/Provider/UserContextProvider';
import { isSameDay } from 'date-fns';
import { User } from '@type/user';
import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { Container } from '@mui/system';

export type CreateBooking = {
  seatId: number;
  userId: number;
  date: string;
};

// const url = 'http://localhost:51249/api/Booking/Book';
// export const putBookingBook = async ({ seatId, userId, date }: CreateBooking) => {
//   const requestOptions = {
//     method: 'PUT',
//     headers: {
//       Accept: 'application/json',
//       'Content-Type': 'application/json',
//     },
//     body: JSON.stringify({
//       seatId,
//       userId,
//       date,
//     }),
//   };
//   const res = await fetch(url, requestOptions);

//   // return as json if error, so we can log error message
//   // if (res.status == 400) {
//   //   return res.json();
//   // } else {
//   //   return res.json();
//   // }
//   return res.json();
// };

// export const useBook = () => {
//   return useMutation({
//     mutationFn: putBookingBook,
//     onError: error => {
//       console.log(error);
//     },
//     onMutate: m => {
//       console.log(m);
//     },
//     onSuccess: s => {
//       queryClient.setQueryData(['seat', s.seatId], s);
//       console.log(s);
//     },
//   });
// };

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
  booking: Booking;
};

export const BookSeat = ({ seat, date, booking }: TBookSeat) => {
  // logged in user
  const loggedInUser = useUserContext().user;
  // const loggedInUserAddBooking = (b: any) => useUserContext().addBooking(b);
  // const loggedInUserRemoveBooking = (b: any) => useUserContext().removeBooking(b);

  // user occupying seat
  const [seatInfo, setSeatInfo] = useState('bookAvailableSeat');
  const [userId, setUserId] = useState('');
  const { isLoading, data, error } = useUser(userId);
  const [open, setOpen] = useState(false);
  const bookMutation = useBook();
  const unbookMutation = useUnbook();

  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);

  useEffect(() => {
    // console.log(`BOOKING USER ID: ${booking?.userId}`)
    // console.log(`USER ID: ${loggedInUser.id}`)
    console.log(`USER BOOKINGS FOR USER: ${loggedInUser.id}`);
    // loggedInUser.bookings.forEach(b => {
    //   console.log(b)
    // })
    if (booking) {
      setUserId(booking?.userId.toString());
    }
  }, [date, data, booking, loggedInUser.bookings]);

  if (isLoading) {
    return (
      <div style={{ display: 'flex' }}>
        <CircularProgress size={100} style={{ margin: '10vh auto' }} />
      </div>
    );
  }

  if (error) {
    return <h4>Kan ikke finne seteinformasjon.</h4>;
  }

  const handleBook = (e: React.MouseEvent<HTMLButtonElement>) => {
    e.preventDefault();
    const bookingData = {
      seatId: seat?.id,
      userId: loggedInUser.id,
      date: date?.toISOString(), //"2023-01-06T12:00:00.000+01",
    } as CreateBooking;
    bookMutation.mutate(bookingData);

    // if (bookMutation.isSuccess) {
    //   console.log("BOOKING SUCCESFUL")
    //   console.log(bookMutation.data)
    //   // loggedInUserAddBooking(bookMutation.data)
    // }
    // loggedInUser.bookings.push(bookingData)
    // TODO: fiks så den bare kjører når respons har kode 204
    // if (mutation.isSuccess) handleOpen();
    handleOpen();
    setSeatInfo('removeBookedSeat');
  };

  const handleUnbook = (e: React.MouseEvent<HTMLButtonElement>) => {
    e.preventDefault();
    const unbookingData = {
      userId: loggedInUser.id,
      date: date?.toISOString(), //"2023-01-06T12:00:00.000+01",
    } as DeleteBooking;
    unbookMutation.mutate(unbookingData);

    // TODO: fiks så den bare kjører når respons har kode 204
    // if (mutation.isSuccess) handleOpen();
    handleOpen();
    setSeatInfo('bookAvailableSeat');
  };

  const SeatInfoOccupied = () => {
    return (
      <Container>
        <h3>Booket av: </h3>
        <div style={{ textAlign: 'left' }}>
          <h3>Bruker: {data?.name}</h3>
          <h3>Telefon: {data?.phoneNumber}</h3>
          <h3>Email: {data?.email}</h3>
        </div>
      </Container>
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

  // const SeatInfoBook = () => {
  //   // BUG: need to make sure loggedInUser.bookings got updated when booking/unbooking another seat that same day
  //   const userAlreadyBooked = loggedInUser.bookings.find(b =>
  //     isSameDay(new Date(b.date), new Date(date!.toISOString()))
  //   );
  //   if (userAlreadyBooked) {
  //     return renderSeatInfoDisabled();
  //   } else {
  //     return renderSeatInfoAvailable();
  //   }
  // };

  const SeatInfoAvailable = () => {
    return (
      <Button variant='contained' onClick={handleBook}>
        Book sete
      </Button>
    );
  };
  // {
  //             <Modal
  //               open={open}
  //               onClose={handleClose}
  //               aria-labelledby='modal-modal-title'
  //               aria-describedby='modal-modal-description'>
  //               <Box sx={modalStyle}>
  //                 <Typography id='modal-modal-title' variant='h6' component='h2'>
  //                   Setet er booket!
  //                 </Typography>
  //               </Box>
  //             </Modal>
  //           }
  // const renderSeatInfoDisabled = () => {
  //   return (
  //     <div style={{ margin: '10px', textAlign: 'center' }}>
  //       <Box style={boxStyle}>
  //         <img src={Chair} height='120' style={{ margin: 'auto auto 20px' }} />
  //         <h3>Setenummer: {seat?.name}</h3>
  //         <h3>Romnummer: {seat?.roomId}</h3>
  //         <h3>Dato: {date?.toDate().toLocaleDateString()}</h3>
  //         <Button variant='contained' disabled={true}>
  //           Book sete
  //         </Button>
  //         <h5>Du har allerede booket for denne dagen.</h5>
  //       </Box>
  //     </div>
  //   );
  // };

  // if (booking && booking.userId !== loggedInUser.id) {
  //   return <SeatInfoOccupied />;
  // } else if (booking && booking.userId == loggedInUser.id) {
  //   return <SeatInfoUnbook />;
  // } else {
  //   return <SeatInfoBook />;
  // }

  const populateSeatInfo = (seatInfo: string) => {
    if (seatInfo === 'bookAvailableSeat') {
      return <SeatInfoAvailable />;
    }
    if (seatInfo === 'removeBookedSeat') {
      return <SeatInfoUnbook />;
    }
    if (seatInfo === 'bookedSeat') {
      return <SeatInfoOccupied />;
    }
  };

  return (
    <div style={{ margin: '10px', textAlign: 'center' }}>
      <Box style={boxStyle}>
        <img src={Chair} height='120' style={{ margin: 'auto auto 20px' }} />
        <h3>Setenummer: {seat?.name}</h3>
        <h3>Romnummer: {seat?.roomId}</h3>
        <h3>Dato: {date?.toDate().toLocaleDateString()}</h3>
        {populateSeatInfo(seatInfo)}
      </Box>
    </div>
  );
};
