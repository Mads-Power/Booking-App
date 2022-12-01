import React, { useState } from 'react';
import { Seat } from '../types';
import Dialog from '@mui/material/Dialog';
import { DialogActions, DialogContent } from '@mui/material';
import Button from '@mui/material/Button';

const GetSeat = ({ seatId }: Seat) => {
  return (
    <>
      {/* <Dialog open={open} onClose={handleClose}>
        <DialogContent>
          <img src='./chair.png' alt='' />
          <p>Rom: ...</p>
          <p>Setenummer: {seatId}</p>
          <p>Navn: </p>
        </DialogContent>
        <DialogActions>
          <Button onClick={handleClose}>Book Seat</Button>
        </DialogActions>
      </Dialog> */}
      <p>hehelwofrhle</p>
    </>
  );
};

export default GetSeat;
