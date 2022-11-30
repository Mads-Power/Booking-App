import { useEffect, useState } from "react";
import styled from "styled-components";
import GetSeat from "../features/seats/components/GetSeat";
import GetSeats from "../features/seats/components/GetSeats";
import { Seat } from "../features/seats/types";
import { Container } from "../styles/GlobalStyles";

const Seats = styled.button`
  display: flex;
  justify-content: center;
  align-items: center;
  position: relative;
  gap: 1em;
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

const RoomOne = () => {
  const [active, setActive] = useState(false);

  const handleClick = () => {
    setActive(!active);
  };

  return (
    <>
      <h1>Room one</h1>
      <Container>
        <GetSeats />
        {/* <Seats
          onClick={handleClick}
          style={{ backgroundColor: active ? " #54a4d1" : "#df8b0d" }}
        >
          background color change
        </Seats> */}
      </Container>
    </>
  );
};

const RoomTwo = () => {
  return (
    <>
      <h1>Room Two</h1>
      <Container>
        <Seats>6</Seats>
        <Seats>7</Seats>
        <Seats>8</Seats>
        <Seats>9</Seats>
        <Seats>10</Seats>
        <Seats>11</Seats>
        <Seats>12</Seats>
        <Seats>13</Seats>
        <Seats>14</Seats>
        <Seats>15</Seats>
      </Container>
    </>
  );
};

const OfficeOslo = () => {
  return (
    <div>
      <RoomOne />
      <hr />
      <RoomTwo />
    </div>
  );
};

export default OfficeOslo;
