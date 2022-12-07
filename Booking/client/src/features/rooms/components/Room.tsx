import { CircularProgress } from '@mui/material';
import React, { useState } from 'react';
import { Container } from '../../../styles/GlobalStyles';
import { GetSeats } from '../../seats/components/GetSeats';
import { useRooms } from '../api/getRooms';

const Room = () => {
  const rooms = useRooms();

  if (rooms.isLoading) {
    return <CircularProgress size={100} />;
  }

  if (!rooms?.data?.length) {
    return <h4>No Rooms Found</h4>;
  }
  return <>{console.log(rooms.data)}</>;
};

export default Room;
