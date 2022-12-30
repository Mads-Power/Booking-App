import { useRooms } from '@api/getRooms';
import { Box } from '@mui/system';
import { CircularProgress, Button, Container, Grid } from '@mui/material';
import { Link, useLocation } from 'react-router-dom';
import { Seat } from '@type/seat';
import { WeekViewDatePicker } from '@components/WeekViewDatePicker';
import './Rooms.module.css';
import { useEffect, useState } from 'react';
import { useBookingsByRoom } from '@api/getBookingsByRoom';

const handleInitialDate = (date: Date) => {
  if (date) {
    console.log("got date")
    console.log(date)
    return date;
  }
  else {
    return new Date();
  }
}

export const Rooms = () => {
  const { isLoading, data, error } = useRooms();
  const childDate = useLocation().state?.date;
  const [date, setDate] = useState(handleInitialDate(childDate)); //'2023-01-02T13:42:16.115Z'
  const occupiedSeats = useBookingsByRoom(1, date);
  
  useEffect(() => {
  }, [date, occupiedSeats]);

  if (isLoading) {
    return <CircularProgress size={100} />;
  }

  if (error) {
    return <h4>No Rooms Found</h4>;
  }

  const handleSelectDate = (date: Date) => {
    console.log(date);
    setDate(date);
  }

  const renderIsOccupied = (seatId: number) => {
    const predicate =  occupiedSeats.data?.find((o) => o.seatId == seatId) != undefined;
    // console.log(predicate)
    return predicate
  }

  return (
    <>
      <Container style={{width: "90vw"}}>
        <WeekViewDatePicker handleSelectDate={handleSelectDate} date={date}/>
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
                <Link to={`/seat/${seat.id}`} key={seat.id} relative='path' state={{ date: date }}>
                  <Button variant='contained' sx={ renderIsOccupied(seat.id) ? { backgroundColor: '#DF8B0D' } : { backgroundColor: '#54A4D1' }}>
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
