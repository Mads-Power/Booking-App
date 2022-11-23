import styled from "styled-components";
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
  box-shadow: 2px 2px 2px 1px rgba(0, 0, 0, 0.2);
`;

const RoomOne = () => {
  return (
    <>
      <h1>Room one</h1>
      <Container>
        <Seats>1</Seats>
        <Seats>2</Seats>
        <Seats>3</Seats>
        <Seats>4</Seats>
        <Seats>5</Seats>
        <Seats>6</Seats>
        <Seats>7</Seats>
        <Seats>8</Seats>
        <Seats>9</Seats>
        <Seats>10</Seats>
      </Container>
    </>
  );
};

const RoomTwo = () => {
  return (
    <>
      <h1>Room Two</h1>
      <Container>
        <Seats>1</Seats>
        <Seats>2</Seats>
        <Seats>3</Seats>
        <Seats>4</Seats>
        <Seats>5</Seats>
        <Seats>6</Seats>
        <Seats>7</Seats>
        <Seats>8</Seats>
        <Seats>9</Seats>
        <Seats>10</Seats>
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
