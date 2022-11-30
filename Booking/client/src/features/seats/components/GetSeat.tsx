import React, { FC, useState } from "react";
import styled from "styled-components";
import { Seat } from "../../../types";
import Dialog from "@mui/material/Dialog";
import { DialogActions, DialogContent } from "@mui/material";
import Button from "@mui/material/Button";

const Seats = styled.button`
  height: 5em;
  width: 5em;
  border-radius: 5px;
  margin-top: 1rem;
  margin-bottom: 1rem;
  color: white;
  background-color: #54a4d1;
  box-shadow: 2px 2px 2px 1px rgba(0, 0, 0, 0.2);

  &:hover {
    border: 2px solid blue;
  }
`;

const GetSeat: FC<Seat> = ({ seatId }: Seat) => {
  const [active, setActive] = useState(false);
  const [open, setOpen] = useState(false);

  const handleClick = () => {
    setActive(!active);
  };
  const handleOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  return (
    <>
      <Seats
        onClick={handleOpen}
        // style={{ backgroundColor: active ? "#df8b0d" : "#54a4d1" }}
      >
        {seatId}
      </Seats>
      <Dialog open={open} onClose={handleClose}>
        <DialogContent>
          <img src="./chair.png" alt="" />
          <p>Rom: ...</p>
          <p>Setenummer: {seatId}</p>
          <p>Navn: </p>
        </DialogContent>
        <DialogActions>
          <Button onClick={handleClose}>Book Seat</Button>
        </DialogActions>
      </Dialog>
    </>
  );
};

export default GetSeat;
