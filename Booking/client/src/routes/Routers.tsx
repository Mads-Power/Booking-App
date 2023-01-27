import { Routes, Route } from "react-router-dom";
import { Rooms } from "@components/Rooms";
import { Office } from "@components/Office";
import { Seat } from "@components/Seat";
import { Bookings } from "@components/Bookings/Bookings";
import { LoginPage, LoggedIn } from "@components/Login/LoginPage";
import { useAtom } from "jotai";
// import { isAuthenticatedAtom } from "@components/Provider/jotaiProvider";

const Routers = () => {
  // const [isAuthenticated, setIsAuthenticated] = useAtom(isAuthenticatedAtom);

  // if (isAuthenticated) {
  //   return (
  //     <Routes>
  //       <Route path="/" element={<Rooms />}></Route>
  //       <Route path="/office" element={<Office />}></Route>
  //       <Route path="/seat/:seatId" element={<Seat />}></Route>
  //       <Route path="/bookings/:userId" element={<Bookings />}></Route>
  //       <Route path="/loggedIn" element={<LoggedIn />}></Route>
  //     </Routes>
  //   );
  // } else {
  //   return (
  //     <Routes>
  //       <Route path="/" element={<LoginPage />}></Route>
  //       <Route path="/login" element={<LoginPage />}></Route>
  //     </Routes>
  //   );
  // }

  return (
  <Routes>
    <Route path="/" element={<Rooms />}></Route>
    <Route path="/login" element={<LoginPage />}></Route>
    <Route path="/office" element={<Office />}></Route>
    <Route path="/seat/:seatId" element={<Seat />}></Route>
    <Route path="/bookings/:userId" element={<Bookings />}></Route>
    <Route path="/loggedIn" element={<LoggedIn />}></Route>
  </Routes>
  )
};

export default Routers;
