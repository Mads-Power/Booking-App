import { AppProvider } from './components/Provider/app';
import OfficeLayout from './components/Layout/OfficeLayout';
import MainLayout from './components/Layout/MainLayout';
import SeatLayout from './components/Layout/SeatLayout';
import GetSeat from './features/seats/components/GetSeat';
import Seat from './data/seats.json';
import Room from './features/rooms/components/Room';

function App() {
  return <AppProvider children={<Room />} />;
}

export default App;
