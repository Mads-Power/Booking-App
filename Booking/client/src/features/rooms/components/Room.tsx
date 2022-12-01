import React from 'react';
import { Container } from '../../../styles/GlobalStyles';
import { GetSeats } from '../../seats/components/GetSeats';
import { Room } from '../types';

const Room = () => {
  return (
    <Container>
      <GetSeats />
    </Container>
  );
};

export default Room;
