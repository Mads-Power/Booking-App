import { useRoomsQuery } from '@api/useRoomsQuery';
import { Box } from '@mui/system';
import { CircularProgress, Button, Container } from '@mui/material';
import { Link } from 'react-router-dom';
import { Seat } from '@type/seat';
import { WeekViewDatePicker } from '@components/WeekViewDatePicker';
import './Rooms.module.css';
import { useEffect, useState } from 'react';
import { useBookingsByRoomQuery } from '@api/useBookingsByRoomQuery';
import { useAtom } from 'jotai';
import { dateAtom } from '../../jotaiProvider';
import { useUserQuery } from '@api/useUserQuery';

export const Rooms = () => {
  const { isLoading, data, error } = useRoomsQuery();
  const [date, setDate] = useAtom(dateAtom);
  const occupiedSeats = useBookingsByRoomQuery(1, date);
  const { data: userData } = useUserQuery('5');

  if (isLoading) {
    return <CircularProgress size={100} />;
  }

  if (error) {
    return <h4>No Rooms Found</h4>;
  }

  //removed !== undefined
  const isOccupied = (seatId: number) => {
    return occupiedSeats.data?.find(occupiedSeat => occupiedSeat.seatId == seatId);
  };

  //removed !== undefined
  const occupiedByLoggedInUser = (seatId: number) => {
    return occupiedSeats.data?.find(
      occupiedSeat =>
        occupiedSeat.seatId === seatId && occupiedSeat.userId === userData?.id
    );
  };

  return (
    <>
      <Container style={{ width: '90vw' }}>
        <WeekViewDatePicker />
      </Container>
      {data?.map(rooms => (
        <div key={rooms.id}>
          <Container>
            <h1>{rooms.name}</h1>
          </Container>
          <Container
            sx={{
              display: 'flex',
              flexWrap: 'wrap',
              backgroundColor: '#CECECE',
              borderRadius: '25px',
              flexDirection: 'column-reverse',
              p: 1,
              border: '1px solid',
            }}>
            <Box
              sx={{
                display: 'flex',
                flexWrap: 'wrap',
                m: 2,
                gap: 2,
              }}>
              {rooms.seats.map((seat: Seat) => (
                <Link to={`/seat/${seat.id}`} key={seat.id} relative='path'>
                  <Button
                    variant='contained'
                    sx={
                      occupiedByLoggedInUser(seat.id)
                        ? { backgroundColor: '#61C577' }
                        : isOccupied(seat.id)
                        ? { backgroundColor: '#DF8B0D' }
                        : { backgroundColor: '#54A4D1' }
                    }>
                    {seat.id}
                  </Button>
                </Link>
              ))}
            </Box>
          </Container>
        </div>
      ))}
    </>
  );
};
