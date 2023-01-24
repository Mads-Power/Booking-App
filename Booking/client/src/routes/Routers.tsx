import { Routes, Route } from 'react-router-dom';
import { Rooms } from '@components/Rooms';
import { Office } from '@components/Office';
import { Seat } from '@components/Seat';
import { Bookings } from '@components/Bookings/Bookings';

const Routers = () => {
  return (
    <Routes>
      <Route path='/' element={<Rooms />}></Route>
      <Route path='/office' element={<Office />}></Route>
      <Route path='/seat/:seatId' element={<Seat />}></Route>
      <Route path='/bookings/:userId' element={<Bookings />}></Route>
    </Routes>
  );
};

export default Routers;
