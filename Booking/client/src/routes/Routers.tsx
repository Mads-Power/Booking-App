import { Routes, Route } from "react-router-dom";
import MainLayout from "../components/Layout/MainLayout";
import OfficeLayout from "../components/Layout/OfficeLayout";

const Routers = () => {
  return (
    <Routes>
      <Route path="/" element={<OfficeLayout />}></Route>
      <Route path="/mainLayout" element={<MainLayout />}></Route>
      <Route path="/" element={<OfficeLayout />}></Route>
    </Routes>
  );
};

export default Routers;
