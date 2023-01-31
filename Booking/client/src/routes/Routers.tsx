import { Routes, Route } from "react-router-dom";
import { Rooms } from "@components/Rooms";
import { Office } from "@components/Office";
import { Seat } from "@components/Seat";
import { Bookings } from "@components/Bookings/Bookings";
import { LoginPage, LoggedIn } from "@components/Login/LoginPage";
import { IsAuthenticated } from "@components/Login/IsAuthenticated";
import { useAtom } from "jotai";
import { userAtom } from "@components/Provider/app";
import { ProtectedRoute } from "./ProtectedRoute";

const Routers = () => {

  const [ user, setUser ] = useAtom(userAtom)
  return (
  <Routes>
    <Route path="/" element={<IsAuthenticated />}></Route>
    <Route path="/login" element={<LoginPage />}></Route>
    <Route path="/loggedIn" element={<LoggedIn />}></Route>

    <Route path="/rooms" element={<ProtectedRoute user={user} outlet={<Rooms />} />} />
    <Route path="/office" element={<ProtectedRoute user={user} outlet={<Office />} />} />
    <Route path="/seat/:seatId" element={<ProtectedRoute user={user} outlet={<Seat />} />} />
    <Route path="/bookings" element={<ProtectedRoute user={user} outlet={<Bookings />} />} />
  </Routes>
  )
};

export default Routers;
