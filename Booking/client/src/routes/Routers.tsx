import { Routes, Route } from "react-router-dom";
import MainLayout from "../components/Layout/MainLayout";
import OfficeLayout from "../components/Layout/OfficeLayout";

type RoutingProviderProps = {
  children: React.ReactNode;
};

const Routers = ({ children }: RoutingProviderProps) => {
  return (
    <>
      <Routes>
        <Route path="/officeLayout" element={<OfficeLayout />}></Route>
        <Route path="/mainLayout" element={<MainLayout />}></Route>
      </Routes>
      {children}
    </>
  );
};

export default Routers;
