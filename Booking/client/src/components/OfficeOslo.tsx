import GetSeats from "../features/seats/components/GetSeats";
import { Container } from "../styles/GlobalStyles";

const RoomOne = () => {
  return (
    <>
      <h1>Room one</h1>
      <Container>
        <GetSeats />
      </Container>
    </>
  );
};

const RoomTwo = () => {
  return (
    <>
      <h1>Room Two</h1>
      <Container></Container>
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
