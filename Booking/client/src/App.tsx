import { AppProvider } from './components/Provider/app';
import OfficeLayout from './components/Layout/OfficeLayout';
import MainLayout from './components/Layout/MainLayout';
import SeatLayout from './components/Layout/SeatLayout';
import GetSeat from './features/seats/components/GetSeat';
import Seat from './data/seats.json';

function App() {
  const { id, isTaken, seatId } = Seat.seat;
  return <AppProvider children={<GetSeat id={id} isTaken={isTaken} seatId={seatId} />} />;
}

export default App;
