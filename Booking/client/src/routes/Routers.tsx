import { Routes, Route } from 'react-router-dom';
import { Rooms } from '@components/Rooms';
import { Office } from '@components/Office';
import { Seat } from '@components/Seat';
import { BrowserRouter } from 'react-router-dom';

const Routers = () => {
  return (
    <Routes>
      <Route path='/' element={<Rooms />}></Route>
      <Route path='/office' element={<Office />}></Route>
      <Route path='/seat/:seatId' element={<Seat />}></Route>
    </Routes>
  );
};

export default Routers;
