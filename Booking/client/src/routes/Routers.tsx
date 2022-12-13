import { Routes, Route } from "react-router-dom";
import MainLayout from "../components/Layout/MainLayout";
import OfficeLayout from "../components/Layout/OfficeLayout";
import SeatLayout from "../components/Layout/SeatLayout";
import { BrowserRouter } from "react-router-dom";

type RoutingProviderProps = {
  children: React.ReactNode;
};

const Routers = ({ children }: RoutingProviderProps) => {
  return (
    <>
      <BrowserRouter>
        <Routes>
          <Route path="/officeLayout" element={<OfficeLayout />}></Route>
          <Route path="/" element={<MainLayout />}></Route>
          <Route path="/seatLayout" element={<SeatLayout />}></Route>
          <Route path="/seatLayout/:seatId" element={<SeatLayout />}></Route>
        </Routes>
      </BrowserRouter>
    </>
  );
};

export default Routers;
