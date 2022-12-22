import { useRooms } from '@api/getRooms';
import { Box } from '@mui/system';
import { CircularProgress, Button, Container, Grid } from '@mui/material';
import { Link } from 'react-router-dom';
import { Seat } from '@type/seat';

export const Rooms = () => {
  const { isLoading, data, error } = useRooms();

  if (isLoading) {
    return <CircularProgress size={100} />;
  }

  if (error) {
    return <h4>No Rooms Found</h4>;
  }

  return (
    <>
      {data?.map(rooms => (
        <div key={rooms.id}>
          <Container>
            <h1>{rooms.name}</h1>
            <p>Capacity: {rooms.capacity}</p>
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
                  <Button variant='contained' sx={{ backgroundColor: '#54A4D1' }}>
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
